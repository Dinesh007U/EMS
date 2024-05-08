using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class SessionInfoRepo : ISessionInfoRepo<SessionInfo>
    {

        //This method is used for Adding session Details 
        public bool SaveSessionDetails(SessionInfo sessionDetails)
        {
            using (EventManagementDbContext dbContext = new EventManagementDbContext())
            {
                dbContext.SessionInfos.Add(sessionDetails);

                dbContext.SaveChanges();

                return true;
            }
        }

        //This method is used to delete the Session Details
        public bool DeleteSessionDetails(Guid sessionId)
        {
            using (EventManagementDbContext dbContext = new EventManagementDbContext())
            {
                var allSessionDetail = dbContext.SessionInfos.Where(d => d.IsDeleted != true).ToList();

                var sessionDetail = allSessionDetail.Where(d => d.SessionId == sessionId).FirstOrDefault();

                if (sessionDetail != null)
                {
                    dbContext.SessionInfos.Remove(sessionDetail);
                    dbContext.SaveChanges();

                    return true;
                }
            }
            return false;
        }

        //This method is used for Updating the Existing session Details
        public bool UpdateSessionDetails(SessionInfo sessionDetails)
        {
            using (EventManagementDbContext dbContext = new EventManagementDbContext())
            {
                var allSessionDetails = dbContext.SessionInfos.Where(d => d.IsDeleted != true).ToList();

                var oldDetails = allSessionDetails.Where(d => d.SessionId == sessionDetails.SessionId).FirstOrDefault();

                if(oldDetails != null)
                {
                    oldDetails.SessionId = sessionDetails.SessionId;
                    oldDetails.EventId = sessionDetails.EventId;
                    oldDetails.SessionTitle = sessionDetails.SessionTitle;
                    oldDetails.SpeakerId = sessionDetails.SpeakerId;
                    oldDetails.Description = sessionDetails.Description;
                    oldDetails.SessionStart = sessionDetails.SessionStart;
                    oldDetails.SessionEnd = sessionDetails.SessionEnd;
                    oldDetails.SessionUrl = sessionDetails.SessionUrl;
                    oldDetails.IsDeleted = sessionDetails.IsDeleted;

                    dbContext.SaveChanges();

                    return true;
                }

                else
                {
                    return false;
                }
            }
        }

        //This method is used for Get Session details By Session Id
        public SessionInfo GetSessionById(Guid sessionId)
        {
            using (EventManagementDbContext dbContext = new EventManagementDbContext())
            {
                var allSessionDetails = dbContext.SessionInfos.Where(d => d.IsDeleted != true).ToList();

                return allSessionDetails.Where(d => d.SessionId == sessionId).FirstOrDefault();

            }
        }

        //This Method is used for returning all the Session Details
        public ICollection<SessionInfo> GetAllSessionDetails()
        {
            using (EventManagementDbContext dbContext = new EventManagementDbContext())
            {
                return dbContext.SessionInfos.Where(d => d.IsDeleted != true).ToList();
            }
        }

        public List<SessionInfo> GetEventWithSessions(Guid eventId)
        {
            using (EventManagementDbContext dbContext = new EventManagementDbContext())
            {
                var allSessionDetails = dbContext.SessionInfos.Where(d => d.IsDeleted != true).ToList();

                return allSessionDetails.Where(d => d.EventId == eventId).ToList();
            }
        }

        public Dictionary<Guid, List<SessionInfo>> GetAllEventWithSessions()
        {
            using (EventManagementDbContext dbContext = new EventManagementDbContext())
            {
                Dictionary<Guid, List<SessionInfo>> eventWithSession = new Dictionary<Guid, List<SessionInfo>>();

                var allEvents = dbContext.EventDetails.Where(d => d.IsDeleted != true).ToList();

                List<SessionInfo> allSessions = new List<SessionInfo>();

                foreach (var eventDetail in allEvents)
                {
                    var sessionDetails = dbContext.SessionInfos.Where(x => x.EventId == eventDetail.EventId && x.IsDeleted != true).ToList();
                    eventWithSession[eventDetail.EventId] = sessionDetails;
                }

                return eventWithSession;
            }
        }
    }
}
