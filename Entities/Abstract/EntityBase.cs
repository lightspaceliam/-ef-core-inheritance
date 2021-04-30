using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Entities
{
    [DataContract]
    public abstract class EntityBase : IEntity
    {
        [DataMember]
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [DataMember]
        [Display(Name = "Last Modified")]
        [ConcurrencyCheck]
        public DateTime LastModified { get; set; }

        [DataMember]
        [Display(Name = "Created")]
        public DateTime Created { get; set; }
    }
}
