using Entities;
using Microsoft.Extensions.Logging;

namespace Entity.Services
{
    public class AnnouncementPreferenceService : EntityService<AnnouncementPreference>
    {
        public AnnouncementPreferenceService(
            PocDbContext context, 
            ILogger<EntityService<AnnouncementPreference>> logger) : base(context, logger)
        { }
    }
}
