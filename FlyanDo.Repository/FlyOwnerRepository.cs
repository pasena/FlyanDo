using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyanDo.Repository.Abstract;
using FlyanDo.Entity;

namespace FlyanDo.Repository
{
    public class FlyOwnerRepository : IFlyOwnerRepository
    {
        private FlyanDoContext _context;

        public FlyOwnerRepository(FlyanDoContext context)
        {
            _context = context;
        }

        public IQueryable<FlyOwner> GetAll()
        {
            return _context.FlyOwners;
        }

        public FlyOwner GetById(int id)
        {
            return _context.FlyOwners.SingleOrDefault(w => w.Id == id);
        }

        public void Insert(FlyOwner owner)
        {
            _context.FlyOwners.Add(owner);
            _context.SaveChanges();
        }

        public void Update(FlyOwner owner)
        {
            if (owner.Id > 0)
            {
                var ownerToUpdate = GetById(owner.Id);

                if (ownerToUpdate != null)
                {
                    _context.Entry(ownerToUpdate).CurrentValues.SetValues(owner);
                }
            }

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var ownerToDelete = GetById(id);

            if (ownerToDelete != null)
            {
                _context.FlyOwners.Remove(ownerToDelete);
                _context.SaveChanges();
            }
        }
    }
}
