﻿using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepository : IPlatformRepository
    {
        //private readonly AppDbContext _context;

        //public PlatformRepository(AppDbContext context) => _context = context;

        //public void CreatePlatform(Platform platform) => _context.Platforms.Add(platform);

        //public IEnumerable<Platform> GetAllPlatforms() => _context.Platforms.ToList();

        //public Platform GetPlatformById(string id) => _context.Platforms.FirstOrDefault(p => p.Id == id);

        //public bool SaveChanges() => _context.SaveChanges() >= 0;
        public void CreatePlatform(Platform platform)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            throw new NotImplementedException();
        }

        public Platform GetPlatformById(string id)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
