using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public interface ISessionInfoRepo<T>
    {
        //initializing all the methods
        public bool SaveSessionDetails(T sessionDetails);

        public bool DeleteSessionDetails(Guid sessionId);

        public bool UpdateSessionDetails(T sessionDetails);

        public T GetSessionById(Guid sessionId);

        public ICollection<T> GetAllSessionDetails();

        public List<SessionInfo> GetEventWithSessions(Guid eventId);

        public Dictionary<Guid, List<T>> GetAllEventWithSessions();
    }
}
