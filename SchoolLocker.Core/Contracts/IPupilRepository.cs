using SchoolLocker.Core.Entities;

namespace SchoolLocker.Core.Contracts
{
    public interface IPupilRepository
    {
        Pupil[] GetAll();
        void Add(Pupil pupil);
        Pupil GetById(int id);
        void Delete(Pupil pupil);
    }
}
