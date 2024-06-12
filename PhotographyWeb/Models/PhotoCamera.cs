using System.ComponentModel.DataAnnotations;

namespace PhotographyWeb.Models
{
    public class PhotoCamera
    {
        [Key]
        public int PhotoCameraId { get; set; }
        public string MaxResolution { get; set; }
    }
}
