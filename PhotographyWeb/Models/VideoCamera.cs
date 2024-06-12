using System.ComponentModel.DataAnnotations;

namespace PhotographyWeb.Models
{
    public class VideoCamera
    {
        [Key]
        public int VideoCameraId { get; set; }
        public int MovieSize { get; set; }
    }
}
