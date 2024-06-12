using System.ComponentModel.DataAnnotations;

namespace PhotographyWeb.Models
{
    public class Video
    {
        [Key]
        public int VideoId { get; set; }
        public int Length { get; set; }
        public int Size { get; set; }
        public DateTime Date { get; set; }
        public string FileFormat { get; set; }
        public string VideoQuality { get; set; }
        public int BitRate { get; set; }
        public int VideoCameraId { get; set; }
        public int VideoGrapherId { get; set; }


    }
}
