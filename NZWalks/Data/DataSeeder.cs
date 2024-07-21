namespace MWalks.API.Data
{
    public static class DataSeeder
    {
        public static void SeedRegions(this ModelBuilder modelBuilder)
        {
            var regions = new List<Region>
        {
            new Region
            {
                Id = Guid.Parse("c084a776-99eb-4c2d-9efb-4f964e17f4a3"),
                Name = "Auckland",
                Code = "AKL",
                ImageUrl = null // Use a real image URL if needed
            },
            new Region
            {
                Id = Guid.Parse("29ff1a54-b7ad-42a0-9e74-fc8cc51bda7b"),
                Name = "Wellington",
                Code = "WLG",
                ImageUrl = null
            },
            new Region
            {
                Id = Guid.Parse("f3d6b858-d0b3-4012-a1d8-50da8cbba12e"),
                Name = "Christchurch",
                Code = "CHC",
                ImageUrl = null
            },
            new Region
            {
                Id = Guid.Parse("3f88b2a8-50c4-46e6-8e4f-9a9b9b4f0ae5"),
                Name = "Hamilton",
                Code = "HAM",
                ImageUrl = null
            },
            new Region
            {
                Id = Guid.Parse("d1c9914b-05c3-4d7a-86ab-1b3c5827cbbc"),
                Name = "Napier",
                Code = "NAP",
                ImageUrl = null
            }
        };

            modelBuilder.Entity<Region>().HasData(regions);
        }

        public static void SeedDifficulties(this ModelBuilder modelBuilder)
        {
            var difficulties = new List<Difficulty>
        {
            new Difficulty
            {
                Id = Guid.Parse("7a1bc1f1-9dcb-42d1-906d-f5bb7921d001"),
                Name = "Easy"
            },
            new Difficulty
            {
                Id = Guid.Parse("fdc29a29-09c9-4897-8c6d-bd62b2f9eab4"),
                Name = "Moderate"
            },
            new Difficulty
            {
                Id = Guid.Parse("9294e56f-6b1d-4784-b64c-ec86ad3a3f98"),
                Name = "Hard"
            },
            new Difficulty
            {
                Id = Guid.Parse("7e22f1d8-3c3a-4e74-bb38-5b350156b9fc"),
                Name = "Expert"
            }
        };

            modelBuilder.Entity<Difficulty>().HasData(difficulties);
        }

        public static void SeedWalks(this ModelBuilder modelBuilder)
        {
            var walks = new List<Walk>
        {
            new Walk
            {
                Id = Guid.Parse("56a9b16f-6cfa-4d25-92c7-63d0e3f6d6e4"),
                Name = "Milford Track",
                Description = "Milford Track is a 53.5 km walk.",
                LengthInKm = 53.5,
                ImageUrl = null, // Use a real image URL if needed
                RegionId = Guid.Parse("c084a776-99eb-4c2d-9efb-4f964e17f4a3"),
                DifficultyId = Guid.Parse("9294e56f-6b1d-4784-b64c-ec86ad3a3f98")
            },
            new Walk
            {
                Id = Guid.Parse("7eafc632-1ed5-4317-8ff0-5a4d015e5c98"),
                Name = "Tongariro Alpine Crossing",
                Description = "The Tongariro Alpine Crossing is a 19.4 km walk.",
                LengthInKm = 19.4,
                ImageUrl = null,
                RegionId = Guid.Parse("29ff1a54-b7ad-42a0-9e74-fc8cc51bda7b"),
                DifficultyId = Guid.Parse("fdc29a29-09c9-4897-8c6d-bd62b2f9eab4")
            },
            new Walk
            {
                Id = Guid.Parse("0a2d547c-7ef0-4bbf-918d-6dfe56b8bc02"),
                Name = "Kepler Track",
                Description = "Kepler Track is a 60 km walk.",
                LengthInKm = 60,
                ImageUrl = null,
                RegionId = Guid.Parse("f3d6b858-d0b3-4012-a1d8-50da8cbba12e"),
                DifficultyId = Guid.Parse("9294e56f-6b1d-4784-b64c-ec86ad3a3f98")
            },
            new Walk
            {
                Id = Guid.Parse("ff0bb60f-f7f1-442b-95e8-e2c9f6d97ad6"),
                Name = "Routeburn Track",
                Description = "Routeburn Track is a 32 km walk.",
                LengthInKm = 32,
                ImageUrl = null,
                RegionId = Guid.Parse("3f88b2a8-50c4-46e6-8e4f-9a9b9b4f0ae5"),
                DifficultyId = Guid.Parse("fdc29a29-09c9-4897-8c6d-bd62b2f9eab4")
            },
            new Walk
            {
                Id = Guid.Parse("82a9b1f6-4e7e-4d85-9f8d-763e3e8b08f5"),
                Name = "Abel Tasman Coast Track",
                Description = "Abel Tasman Coast Track is a 60 km walk.",
                LengthInKm = 60,
                ImageUrl = null,
                RegionId = Guid.Parse("d1c9914b-05c3-4d7a-86ab-1b3c5827cbbc"),
                DifficultyId = Guid.Parse("7a1bc1f1-9dcb-42d1-906d-f5bb7921d001")
            }
        };

            modelBuilder.Entity<Walk>().HasData(walks);
        }

        public static void SeedRoles(this ModelBuilder modelBuilder)
        {
            var readerRoleId = "4d8c548f-48c4-4ec2-8802-367ac1c39144";
            var writerRoleId = "b3572aab-b884-46e2-a878-e3c93a8c6548";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp =readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                },
                 new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp =writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }

    }
}