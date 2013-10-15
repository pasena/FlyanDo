using System.Linq;
using FlyanDo.Entity;

namespace FlyanDo.Repository.Abstract
{
    public interface IFlyOwnerRepository
    {
        IQueryable<FlyOwner> GetAll();
        FlyOwner GetById(int id);

        void Insert(FlyOwner owner);
        void Update(FlyOwner owner);
        void Delete(int id);
    }
}
