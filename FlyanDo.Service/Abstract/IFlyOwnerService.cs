using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyanDo.Entity;

namespace FlyanDo.Service.Abstract
{
    public interface IFlyOwnerService
    {
        IQueryable<FlyOwner> GetAll();
        FlyOwner GetById(int id);

        void Insert(FlyOwner owner);
        void Update(FlyOwner owner);
        void Delete(int id);
    }
}
