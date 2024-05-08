using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    //Derived Class for IParticipantEventDetailsRepo
    public class ParticipantEventDetailsRepo : IParticipantEventDetailsRepo<ParticipantEventDetails>
    {
        //Method for deleting Participant Details
        public bool DeleteParticipantDetails(Guid Id)
        {
            using (EventManagementDbContext dbContext = new EventManagementDbContext())
            {
                var allParticipantDetail = dbContext.ParticipantEventDetails.Where(p => p.IsDeleted != true).ToList();
                var participantDetail = allParticipantDetail.Where(p => p.ID == Id).FirstOrDefault();
                if (participantDetail != null)
                {
                    dbContext.ParticipantEventDetails.Remove(participantDetail);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        //This Method is used for returning all the participant Details
        public ICollection<ParticipantEventDetails> GetAllParticipantDetails()
        {
            using (EventManagementDbContext dbContext = new EventManagementDbContext())
            {
                return dbContext.ParticipantEventDetails.Where(p => p.IsDeleted != true).ToList();

            }
        }

        //This Method is used for returning specific participant Detail by id 
        public ParticipantEventDetails GetParticipantById(Guid id)
        {
            using (EventManagementDbContext dbContext = new EventManagementDbContext())
            {
                var allParticipantDetails = dbContext.ParticipantEventDetails.Where(p => p.IsDeleted != true).ToList();
                return allParticipantDetails.Where(p => p.ID == id).FirstOrDefault();
            }
        }

        public bool SaveParticipantDetails(ParticipantEventDetails participantDetails)
        {
            try
            {
                using (EventManagementDbContext dbContext = new EventManagementDbContext())
                {
                    dbContext.ParticipantEventDetails.Add(participantDetails);
                    dbContext.SaveChanges();
                    return true;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Update IsAttended
        public bool UpdateParticipantDetails(ParticipantEventDetails participantDetails)
        {
            try
            {
                using (EventManagementDbContext dbContext = new EventManagementDbContext())
                {
                    var allParticipantDetails = dbContext.ParticipantEventDetails.Where(p => p.IsDeleted != true).ToList();

                    var oldParticipantDetails = allParticipantDetails.Where(p => p.ID == participantDetails.ID).FirstOrDefault();

                    if (oldParticipantDetails != null)
                    {
                        oldParticipantDetails.IsAttended = true;

                        dbContext.SaveChanges();

                        return true;
                    }
                    else
                    {
                        return false;
                    }

                    

                    
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        
    }
}
