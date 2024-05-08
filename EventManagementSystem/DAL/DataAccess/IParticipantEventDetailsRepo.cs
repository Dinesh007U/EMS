using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    //This is interface for IParticipantDetails
    public interface IParticipantEventDetailsRepo<T>
    {
        //initializing all the methods
        public bool SaveParticipantDetails(T participantDetails);

        public bool DeleteParticipantDetails(Guid Id);

        public bool UpdateParticipantDetails(T participantDetails);

        public T GetParticipantById(Guid Id);

        public ICollection<T> GetAllParticipantDetails();

    }
}
