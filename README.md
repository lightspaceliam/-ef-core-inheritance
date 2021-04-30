# Entity Framework Core Inheritance

## Summary
Entity Framework has an excellent strategy for transforming derived types implementing commonality via inheritance to the database. Microsoft explains this as: 

> EF can map a .NET type hierarchy to a database. This allows you to write your .NET entities in code as usual, using base and derived types, and have EF seamlessly create the appropriate database schema, issue queries, etc. The actual details of how a type hierarchy is mapped are provider-dependent; this page describes inheritance support in the context of a relational database.

Reference: [Inheritance](https://docs.microsoft.com/en-us/ef/core/modeling/inheritance)

How I interpret this:

Entity Framework support for table/s designed to Inherit common properties, reducing the need for duplication, provide extensibility and facilitates a very expressive pattern that can be consumed by multiple subscribers.
 
## Technology Stack
- MS SQL relational database 2019^
- Entity Framework 5.0.5
- Code First Migrations 5.0.5

 ## Use Case
I need to persist user preferences. At the moment, there is a requirement to have user preferences for:
- Announcements
- Preferences specific to the user: date / time formats, ...
- Future, unforeseen requirements ... 

### Commonality 
Commonality in these two separate concerns is the User. I require a foreign key to reference relational User data.
### Separate Concerns
- Foreign key to Announcement
- Requirement: a location to persist Date / Time format and other ... (purpose of illustrating a point of difference)

## Implementation
I have an abstract class named Preference with two concrete classes that inherit common properties (AnnouncementPreference & UserPreference). At this point I don't want to allow direct access via EF to the entire table, choosing to restrict access via the two domains (you don’t have to do this): 
- AnnouncementPreference
- UserPreference

## Pros & Cons
The primary benefit to this pattern so far is now I have, conceptually two tables and both can scale independently whilst expressively communicating purpose.

| Pros | Cons
| --- | ---
| Table / Domain properties can scale as requirements change independently | Extra configuration in the Fluent Api  
| Facilitates expressive purpose of each segregated range via a discriminator to multiple subscribers | Properties unique to concrete entities are nullable on the database
| Simplified querying by concrete class, you only get that data by that Domain / Discriminator. |
| Unit testable |
| Cannot exceed segregated range - no magic numbers
| Column name "Discriminator" can be configured, including string length constraint and named to whatever you specify 
| Discriminator values can be configured and named to whatever you specify


## References
- [Inheritance](https://docs.microsoft.com/en-us/ef/core/modeling/inheritance)