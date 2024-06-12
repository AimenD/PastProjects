using System.ComponentModel.DataAnnotations;

namespace PhotographyWeb.Models
{
    public class Photo
    {
        [Key]
        public int PhotoId { get; set; }
        public int Size { get; set; }
        public DateTime Date { get; set; }  
        public string FileFormat { get; set; }
        public string Resolution { get; set; }
        public int PhotoCameraId { get; set; }
        public int PhotographerId { get; set; }
    }
}
