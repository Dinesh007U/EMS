using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class SpeakerDetailsInfoRepo: ISpeakerDetailsInfoRepo<SpeakersDetails>
    {
       
//This method is used for saving SpeakerDetail

        public bool SaveSpeakerDetails(SpeakersDetails speakerDetails)

        {

            using (EventManagementDbContext dbContext = new EventManagementDbContext())

            {

                dbContext.SpeakersDetails.Add(speakerDetails);

                dbContext.SaveChanges();

                return true;

            }

        }

        //This method is used for Deleting speaker details

        public bool DeleteSpeakerDetails(Guid speakerId)

        {

            using (EventManagementDbContext dbContext = new EventManagementDbContext())

            {

                var listOfIsNotDelete = dbContext.SpeakersDetails.Where(l => l.IsDeleted != true).ToList();

                var deleteSpeaker = listOfIsNotDelete.Where(d => d.SpeakerId == speakerId).FirstOrDefault();

                if (deleteSpeaker != null)

                {
                    dbContext.SpeakersDetails.Remove(deleteSpeaker);

                    dbContext.SaveChanges();

                    return true;

                }

            }

            return false;

        }

        //This method is used for Updating SpeakerDetails

        public bool UpdateSpeakerDetails(SpeakersDetails speakerDetails)

        {

            using (EventManagementDbContext dbContext = new EventManagementDbContext())

            {

                var isNotDeleteList = dbContext.SpeakersDetails.Where(l => l.IsDeleted != true).ToList();

                var updateSpeaker = isNotDeleteList.Where(u => u.SpeakerId == speakerDetails.SpeakerId).FirstOrDefault();

                updateSpeaker.SpeakerName = speakerDetails.SpeakerName;

                dbContext.SaveChanges();

                return true;

            }

        }

        //This method is used for displaying all the speaker details

        public ICollection<SpeakersDetails> GetAllSpeakerDetails()

        {

            using (EventManagementDbContext dbContext = new EventManagementDbContext())

            {

                return dbContext.SpeakersDetails.Where(d => d.IsDeleted != true).ToList();

            }

        }

        //This method is used for Getting the speaker details by SpeakerID

        public SpeakersDetails GetSpeakerById(Guid speakerId)

        {

            using (EventManagementDbContext dbContext = new EventManagementDbContext())

            {

                var listOfIsNotDelete = dbContext.SpeakersDetails.Where(l => l.IsDeleted != true).ToList();

                var getById = listOfIsNotDelete.Where(g => g.SpeakerId == speakerId).FirstOrDefault();

                return getById;

            }

        }

        //This method is used for Getting the speaker details by Speaker For Event
        public ICollection<SpeakersDetails> GetAllSpeakerForEvent(Guid id)
        {
            ISessionInfoRepo<SessionInfo> sessionInfoRepo = new SessionInfoRepo();

            var allSessionInEvent = sessionInfoRepo.GetEventWithSessions(id);

            var allSpeakerInEvent = new List<SpeakersDetails>();

            foreach (var session in allSessionInEvent)
            {
                var speaker = GetSpeakerById(session.SpeakerId);
                if (speaker != null)
                {
                    allSpeakerInEvent.Add(speaker);
                }
            }

            return allSpeakerInEvent;
        }

    }
}

