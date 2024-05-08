using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{ 
    //This is the interface for EventDetailsRepo
    public interface IEventDetailsRepo<T>
    {
        //initializing all the methods
        public bool SaveEventDetails(T eventDetails);

        public bool DeleteEventDetails(Guid eventId);

        public bool UpdateEventDetails(T eventDetails);

        public T GetEventByEventId(Guid eventId);

        public ICollection<T> GetAllEventDetails();

        public ICollection<T> ParticipantRegisteredEvent(string emailId);

        
        public List<T> GetEventByEventName(string eventName);

    }
}
