using System.ComponentModel.DataAnnotations;

namespace PhotographyWeb.Models
{
    public class StockPhoto
    {
        [Key]
        public int StockPhotoId { get; set; }
        public int Price { get; set; }
    }
}
