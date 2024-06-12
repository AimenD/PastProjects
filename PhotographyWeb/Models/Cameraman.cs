using System.ComponentModel.DataAnnotations;

namespace PhotographyWeb.Models
{
    public class Cameraman
    {
        [Key]
        public int CameramanId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal PhoneNumber { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
    }
}
