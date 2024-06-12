using System.ComponentModel.DataAnnotations;

namespace PhotographyWeb.Models
{
    public class CameramanEmail
    {
        [Key]
        public int CameramanId { get; set; }
        public string Email { get; set; }
    }
}
