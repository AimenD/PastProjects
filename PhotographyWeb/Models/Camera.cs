using System.ComponentModel.DataAnnotations;

namespace PhotographyWeb.Models
{
    public class Camera
    {
        [Key]
        public int CameraId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Price { get; set; } 
        public string Lens { get; set; }
        public bool IsOwned { get; set; }
        public string ImageSensor { get; set; }
        public int Memory { get; set; }
    }
}
