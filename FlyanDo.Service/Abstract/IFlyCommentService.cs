using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyanDo.Entity;

namespace FlyanDo.Service.Abstract
{
    public interface IFlyCommentService
    {
        IQueryable<FlyComment> GetAll();
        FlyComment GetById(int id);

        void Insert(FlyComment comment);
        void Delete(int id);
    }
}
