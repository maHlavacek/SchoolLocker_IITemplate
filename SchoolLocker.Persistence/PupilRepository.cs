using SchoolLocker.Core.Contracts;
using SchoolLocker.Core.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolLocker.Persistence
{
    internal class PupilRepository : IPupilRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PupilRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public Pupil[] GetAll()
        {
            return _dbContext
                .Pupils
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToArray();
        }


        public void Add(Pupil pupil)
        {
            throw new NotImplementedException();
        }

        public Pupil GetById(int id)
        {
            return _dbContext
                .Pupils
                .Find(id);
        }

        public void Delete(Pupil pupil)
        {
            _dbContext
                .Pupils
                .Remove(pupil);
        }
    }
}