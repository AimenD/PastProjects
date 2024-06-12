using System.ComponentModel.DataAnnotations;

namespace PhotographyWeb.Models
{
    public class ConferenceVideoSpeaker
    {
        [Key]
        public int ConferenceVideoId { get; set; }
        public string Speaker { get; set; }
    }
}
