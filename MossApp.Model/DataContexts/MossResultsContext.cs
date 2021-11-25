using Microsoft.EntityFrameworkCore;
using MossApp.Data.Models;
using System;

namespace MossApp.Data
{
    public class MossResultsContext : DbContext
    {
        public DbSet<Results> Results { get; set; }
        public DbSet<MatchPair> MatchPairs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            _ = options.UseSqlServer(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=MossDB;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            _ = modelBuilder.Entity<Results>(entity =>
              {
                  _ = entity.HasData(new Models.Results
                  {
                      Id = 1,
                      DateSubmitted = DateTime.Now,
                      Options = "example options"
                  });
                  _ = entity.HasKey(e => e.Id);

                  _ = entity.Property(e => e.Options).HasMaxLength(25);
              });




            _ = modelBuilder.Entity<MatchPair>(entity =>
              {
                  _ = entity.HasIndex(e => e.Id, "IX_MatchPairs_ResultId");

                  _ = entity.Property(e => e.AlphaFileName).HasMaxLength(50);

                  _ = entity.Property(e => e.AlphaLines).HasMaxLength(15);

                //entity.Property(e => e.AlphaScore).HasColumnType("decimal(18, 2)");

                _ = entity.Property(e => e.BetaFileName).HasMaxLength(50);

                  _ = entity.Property(e => e.BetaLines).HasMaxLength(15);

                //entity.Property(e => e.BetaScore).HasColumnType("decimal(18, 2)");



                _ = entity.HasOne(d => d.Results)
                      .WithMany(p => p.MatchPairs);
              });
            _ = modelBuilder.Entity<MatchPair>().Property<int>("ResultsId");

            _ = modelBuilder.Entity<MatchPair>().HasData(new
            {
                Id = 1,
                ResultsId = 1,
                LinesMatched = 45,
                AlphaFileName = "AlphaFile",
                BetaFileName = "BetaFile",
                AlhpaLines = "12 - 43",
                BetaLines = "43 - 51",
                AlphaScore = 66.6m,
                BetaScore = 33.3m
            });
        }
    }
}
