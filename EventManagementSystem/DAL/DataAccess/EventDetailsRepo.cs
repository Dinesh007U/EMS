using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    //This is the derived class for EventDetails
    public class EventDetailsRepo : IEventDetailsRepo<EventDetails>
    {
        //This method is used to delete the EventDetails
        public bool DeleteEventDetails(Guid eventId)
        {

            using (EventManagementDbContext dbContext = new EventManagementDbContext())
            {
                var allEventDetail = dbContext.EventDetails.Where(d => d.IsDeleted != true).ToList();

                var eventDetail = allEventDetail.Where(d => d.EventId == eventId).FirstOrDefault();

                if (eventDetail != null)
                {
                    dbContext.EventDetails.Remove(eventDetail);
                    dbContext.SaveChanges();

                    return true;
                }
            }
            return false;

        }

        //This Method is used for returning all the eventDetails
        public ICollection<EventDetails> GetAllEventDetails()
        {
            using (EventManagementDbContext dbContext = new EventManagementDbContext())
            {
                return dbContext.EventDetails.Where(d => d.IsDeleted != true).ToList();
            }
        }

        //This method is used for GetEventByEventId
        public EventDetails GetEventByEventId(Guid eventId)
        {
            using (EventManagementDbContext dbContext = new EventManagementDbContext())
            {
                var allEventDetails = dbContext.EventDetails.Where(d => d.IsDeleted != true).ToList();

                return allEventDetails.Where(d => d.EventId == eventId).FirstOrDefault();

            }
        }

        //This method is used for saveEventDetails
        public bool SaveEventDetails(EventDetails eventDetails)
        {
            using (EventManagementDbContext dbContext = new EventManagementDbContext())
            {
                dbContext.EventDetails.Add(eventDetails);

                dbContext.SaveChanges();

                return true;
            }

        }

        //This method is used for UpdatingEventDetails
        public bool UpdateEventDetails(EventDetails eventDetails)
        {
            using (EventManagementDbContext dbContext = new EventManagementDbContext())
            {
                var allEventDetails = dbContext.EventDetails.Where(d => d.IsDeleted != true).ToList();

                var oldDetails = allEventDetails.Where(d => d.EventId == eventDetails.EventId).FirstOrDefault();

                if(oldDetails != null)
                {
                    oldDetails.EventCategory = eventDetails.EventCategory;

                    oldDetails.EventName = eventDetails.EventName;

                    oldDetails.EventDate = eventDetails.EventDate;

                    oldDetails.Description = eventDetails.Description;

                    oldDetails.Status = eventDetails.Status;

                    dbContext.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }


        }

        //This event is used to get the event by eventName
        public EventDetails GetEventByName(string eventName)
        {
            using (EventManagementDbContext dbContext = new EventManagementDbContext())
            {
                var allEventDetails = dbContext.EventDetails.Where(d => d.IsDeleted != true).ToList();

                return allEventDetails.Where(x => x.EventName.Equals(eventName)).FirstOrDefault();

            }
        }

        // Method to display participant's registered events
        public ICollection<EventDetails> ParticipantRegisteredEvent(string emailId)
        {
            ParticipantEventDetailsRepo participantEventDetailsRepo = new ParticipantEventDetailsRepo();
            var participant = participantEventDetailsRepo.GetAllParticipantDetails().Where(x => x.ParticipantEmailId.Equals(emailId)).ToList();
            var eventList = new List<EventDetails>();
            if (participant != null)
            {
                EventDetailsRepo eventDetails = new EventDetailsRepo();

                foreach (var j in participant)
                {
                    foreach (var i in eventDetails.GetAllEventDetails())
                    {
                        if (i.EventId.Equals(j.EventId))
                        {
                            eventList.Add(i);
                        }
                    }
                }
                return eventList;
            }
            else
            {
                return eventList;
            }
        }

        public List<EventDetails> GetEventByEventName(string eventName)
        {
            using (EventManagementDbContext dbContext = new EventManagementDbContext())
            {
                // Retrieve all event details where IsDeleted is not true
                var allEventDetails = dbContext.EventDetails.Where(d => d.IsDeleted != true).ToList();

                // Filter event details by event name
                var eventsByName = allEventDetails.Where(x => x.EventName.Equals(eventName)).ToList();

                return eventsByName;
            }
        }

    }
}
