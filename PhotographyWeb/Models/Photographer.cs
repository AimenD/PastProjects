using System.ComponentModel.DataAnnotations;

namespace PhotographyWeb.Models
{
    public class Photographer
    {
        [Key]
        public int PhotographerId { get; set; }
        public int PhotoCameraOwned { get; set; }
    }
}
