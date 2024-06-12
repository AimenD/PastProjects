using Microsoft.EntityFrameworkCore;
using PhotographyWeb.Models;

namespace PhotographyWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<PhotographyWeb.Models.CameramanEmail> CameramanEmail { get; set; }
        public DbSet<PhotographyWeb.Models.Camera> Camera { get; set; }
        public DbSet<PhotographyWeb.Models.Cameraman> Cameraman { get; set; }
        public DbSet<PhotographyWeb.Models.ConcertVideo> ConcertVideo { get; set; }
        public DbSet<PhotographyWeb.Models.ConferenceVideo> ConferenceVideo { get; set; }
        public DbSet<PhotographyWeb.Models.ConferenceVideoSpeaker> ConferenceVideoSpeaker { get; set; }
        public DbSet<PhotographyWeb.Models.EventPhoto> EventPhoto { get; set; }
        public DbSet<PhotographyWeb.Models.Photo> Photo { get; set; }
        public DbSet<PhotographyWeb.Models.PhotoCamera> PhotoCamera { get; set; }
        public DbSet<PhotographyWeb.Models.Photographer> Photographer { get; set; }
        public DbSet<PhotographyWeb.Models.StockPhoto> StockPhoto { get; set; }
        public DbSet<PhotographyWeb.Models.Video> Video { get; set; }
        public DbSet<PhotographyWeb.Models.VideoCamera> VideoCamera { get; set; }
        public DbSet<PhotographyWeb.Models.VideoGrapher> VideoGrapher { get; set; }
        public DbSet<PhotographyWeb.Models.WeddingPhoto> WeddingPhoto { get; set; }
        public DbSet<PhotographyWeb.Models.WeddingVideo> WeddingVideo { get; set; }
    }
}
