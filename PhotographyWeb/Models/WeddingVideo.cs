using System.ComponentModel.DataAnnotations;

namespace PhotographyWeb.Models
{
    public class WeddingVideo
    {
        [Key]
        public int WeddingVideoId { get; set; }
        public string Groom { get; set; }
        public string Bride { get; set; }

    }
}
