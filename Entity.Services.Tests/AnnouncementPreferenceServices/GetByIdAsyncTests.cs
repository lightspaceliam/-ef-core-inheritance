using Entities;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Entity.Services.Tests.AnnouncementPreferenceServices
{
    public class GetByIdAsyncTests : AnnouncementPreferenceServiceTestBase
    {
        [Fact(DisplayName = "Can find entity when within discriminator range")]
        public async Task CanFindExistingEntity_Success()
        {
            var userId = Guid.NewGuid();
            Context.Users.Add(new User
            {
                Id = userId,
                FirstName = "John",
                LastName = "Smith",
                Email = "js@xyz.com.au",
            });

            var announcementId = Guid.NewGuid();
            Context.Announcements.Add(new Announcement
            {
                Id = announcementId,
                Type = (AnnouncementTypes.TaskYouAreAssignedTo).ToString(),
                UserId = userId
            });

            var announcementPreferenceId = Guid.NewGuid();
            Context.AnnouncementPreferences.Add(new AnnouncementPreference
            {
                Id = announcementPreferenceId,
                UserId = userId,
                AnnouncementId = announcementId
            });

            Context.UserPreferences.Add(new UserPreference
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Type = (UserPreferenceTypes.DateFormat).ToString(),
                Requirement = "AU - not going to look it up!"
            });

            Context.SaveChanges();

            var response = await Service.GetByIdAsync(announcementPreferenceId);

            Assert.Equal(response.Id, announcementPreferenceId);
        }

        [Fact(DisplayName = "Searching for an existing Id that exists outside the discriminator range of AnnouncementPreference returns null")]
        public async Task CanNotFindExistingEntityIfOutsideDescriminatorRange_Null()
        {
            var userId = Guid.NewGuid();
            Context.Users.Add(new User
            {
                Id = userId,
                FirstName = "John",
                LastName = "Smith",
                Email = "js@xyz.com.au",
            });

            var announcementId = Guid.NewGuid();
            Context.Announcements.Add(new Announcement
            {
                Id = announcementId,
                Type = (AnnouncementTypes.TaskYouAreAssignedTo).ToString(),
                UserId = userId
            });

            Context.AnnouncementPreferences.Add(new AnnouncementPreference
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                AnnouncementId = announcementId
            });

            var userPreferenceId = Guid.NewGuid();
            Context.UserPreferences.Add(new UserPreference
            {
                Id = userPreferenceId,
                UserId = userId,
                Type = (UserPreferenceTypes.DateFormat).ToString(),
                Requirement = "AU - I'm not going to look it up! ;-D"
            });

            Context.SaveChanges();

            var response = await Service.GetByIdAsync(userPreferenceId);

            Assert.Null(response);
        }
    }
}
