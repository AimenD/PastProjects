using System.ComponentModel.DataAnnotations;

namespace PhotographyWeb.Models
{
    public class VideoGrapher
    {
        [Key]
        public int VideographerId { get; set; }
        public int VideoCameraOwned { get; set; }
    }
}
