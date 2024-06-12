using System.ComponentModel.DataAnnotations;

namespace PhotographyWeb.Models
{
    public class ConcertVideo
    {
        [Key]
        public int ConcertVideoId { get; set; }
        public string Artist { get; set; }
        public string ConcertArea { get; set; }
    }
}
