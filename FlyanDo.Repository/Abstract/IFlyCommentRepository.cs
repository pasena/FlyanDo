using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyanDo.Entity;

namespace FlyanDo.Repository.Abstract
{
    public interface IFlyCommentRepository
    {
        IQueryable<FlyComment> GetAll();
        FlyComment GetById(int id);

        void Save(FlyComment comment);
        void Delete(int id);
    }
}
