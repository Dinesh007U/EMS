using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{

    //This is the interface for SpeakerDetails

    public interface ISpeakerDetailsInfoRepo<T>

    {

        //initializing all the methods

        public bool SaveSpeakerDetails(T SpeakerDetails);

        public bool DeleteSpeakerDetails(Guid SpeakerId);

        public bool UpdateSpeakerDetails(T SpeakerDetails);

        public T GetSpeakerById(Guid SpeakerId);

        public ICollection<T> GetAllSpeakerDetails();

        public ICollection<SpeakersDetails> GetAllSpeakerForEvent(Guid id);

    }
}