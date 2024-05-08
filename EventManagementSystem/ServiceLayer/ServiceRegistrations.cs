using DAL.DataAccess;
using DAL.Models;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceLayer
{
    //This class binding model's services into single reference
    public static class ServiceRegistrations
    {
        //For binding services
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<ISpeakerDetailsInfoRepo<SpeakersDetails>, SpeakerDetailsInfoRepo>();
            services.AddScoped<IParticipantEventDetailsRepo<ParticipantEventDetails>, ParticipantEventDetailsRepo>();
            services.AddScoped<IEventDetailsRepo<EventDetails>, EventDetailsRepo>();
            services.AddScoped<ISessionInfoRepo<SessionInfo>, SessionInfoRepo>();
            services.AddScoped<IUserInfoRepo<UserInfo>, UserInfoRepo>();

        }
    }
}
