using System.ComponentModel.DataAnnotations;

namespace PhotographyWeb.Models
{
    public class EventPhoto
    {
        [Key]
        public int EventPhotoId { get; set; }
        public string EventName { get; set; }
    }
}
