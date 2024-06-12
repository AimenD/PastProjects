using System.ComponentModel.DataAnnotations;

namespace PhotographyWeb.Models
{
    public class WeddingPhoto
    {
        [Key]
        public int WeddingPhotoId { get; set; }
        public string Groom { get; set; }
        public string Bride { get; set; }
    }
}
