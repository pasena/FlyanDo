using System.Linq;
using FlyanDo.Entity;

namespace FlyanDo.Repository.Abstract
{
    public interface IFlyCommentRepository
    {
        IQueryable<FlyComment> GetAll();
        FlyComment GetById(int id);

        void Insert(FlyComment comment);
        void Update(FlyComment comment);
        void Delete(int id);
    }
}
