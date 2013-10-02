using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyanDo.Entity;

namespace FlyanDo.Repository.Abstract
{
    public interface IFlyOwnerRepository
    {
        IQueryable<FlyOwner> GetAll();
        FlyOwner GetById(int id);

        void Save(FlyOwner owner);
        void Delete(int id);
    }
}
