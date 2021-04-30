using System;

namespace Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
        DateTime LastModified { get; set; }
        DateTime Created { get; set; }
    }
}
