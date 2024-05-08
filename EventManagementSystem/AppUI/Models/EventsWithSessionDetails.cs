using System.Collections.Generic;

namespace AppUI.Models
{
    //This model is used to combine the model for view
    /// <summary>
    /// Creating Model class for handle events with session data
    /// </summary>
    public class EventsWithSessionDetails
    {
        public EventDetails EventInformations { get; set; }

        public List<SessionInfo> SessionDetails { get; set; }

        public List<SpeakersDetails> SpeakersInformations { get; set; }

        public bool isRegister { get; set; }

        public bool isAttended { get; set; }

    }
}
