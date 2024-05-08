using System.Collections.Generic;

namespace AppUI.Models
{
    /// <summary>
    /// Represents the administrative view containing event details, speaker details, and session-speaker mappings.
    /// </summary>
    public class Admin
    {
        public EventDetails AllEventDetails { get; set; }

        public List<SpeakersDetails> AllSpeakerDetails { get; set; }

        public Dictionary<SessionInfo, SpeakersDetails> SessionAndSpeakerDetails { get; set; }
    }
}
