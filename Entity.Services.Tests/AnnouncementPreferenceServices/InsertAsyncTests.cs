using Entities;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Entity.Services.Tests.AnnouncementPreferenceServices
{
    public class InsertAsyncTests : AnnouncementPreferenceServiceTestBase
    {
        [Fact(DisplayName = "Can insert valid Annoucement Preference")]
        public async Task CanInsertValidEntity_Success()
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

            //  You don't have to set the Descriminator. It is configured in the Fluent Api.
            var announcementPreference = new AnnouncementPreference
            {
                UserId = userId,
                AnnouncementId = announcementId
            };

            var response = await Service.InsertAsync(announcementPreference);

            Assert.Equal("AnnouncementPreference", response.Discriminator);
        }
    }
}
