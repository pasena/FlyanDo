using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyanDo.Entity;

namespace FlyanDo.Repository.Abstract
{
    public interface IFlyRepository
    {
        IQueryable<Fly> GetAll();
        Fly GetById(int id);

        void Insert(Fly fly);
        void Update(Fly fly);
        void Delete(int id);
    }
}
