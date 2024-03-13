
using ASP_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASP_Project.Data
{
    public class DataContext: IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            

        }


        public DbSet<CinemaEntity> CinemaEntities {get;set;}
        public DbSet<PlaceEntity> PlaceEntities {get; set;}
        public DbSet<MovieEntity> MovieEntities {get;set;}
        public DbSet<ChatEntity> ChatEntities {get;set;}
        public DbSet<ReportEntity> ReportEntities {get;set;}
        public DbSet<ChatRecordEntity> ChatRecordEntities {get;set;}
        public DbSet<ProgramMovieEntity> ProgramMovieEntities {get;set;}
        public DbSet<MessageRecordEntity> MessageRecordEntities {get;set;}
        public DbSet<RequestEntity> RequestEntities {get; set;}
        public DbSet<FavoriteEntity> FavoriteEntities {get;set;}
    }
}