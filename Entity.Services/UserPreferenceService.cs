using Entities;
using Microsoft.Extensions.Logging;

namespace Entity.Services
{
    public class UserPreferenceService : EntityService<UserPreference>
    {
        public UserPreferenceService(
            PocDbContext context, 
            ILogger<EntityService<UserPreference>> logger) : base(context, logger)
        { }
    }
}
