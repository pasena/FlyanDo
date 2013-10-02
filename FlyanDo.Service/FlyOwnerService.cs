using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyanDo.Service.Abstract;
using FlyanDo.Entity;
using FlyanDo.Repository.Abstract;
using System.Transactions;

namespace FlyanDo.Service
{
    public class FlyOwnerService : IFlyOwnerService
    {
        private IFlyOwnerRepository _ownerRepository;

        public FlyOwnerService(IFlyOwnerRepository ownerRepo)
        {
            _ownerRepository = ownerRepo;
        }

        public IQueryable<FlyOwner> GetAll()
        {
            return _ownerRepository.GetAll();
        }

        public FlyOwner GetById(int id)
        {
            return _ownerRepository.GetById(id);
        }

        public void Insert(FlyOwner owner)
        {
            ValidateInsert(owner);

            using (var scope = new TransactionScope())
            {
                _ownerRepository.Insert(owner);    
                scope.Complete();
            }
            
        }

        public void Update(FlyOwner owner)
        {
            ValideteUpdate(owner);

            using (var scope = new TransactionScope())
            {
                _ownerRepository.Update(owner);
                scope.Complete();
            }
        }

        public void Delete(int id)
        {
            using (var scope = new TransactionScope())
            {
                _ownerRepository.Delete(id);
                scope.Complete();
            }
        }

        private void ValidateInsert(FlyOwner owner)
        {
            if(owner == null)
                throw new ArgumentException("Owner is required!");

            if(string.IsNullOrWhiteSpace(owner.Name))
                throw new ArgumentException("Name is required!");

            if (string.IsNullOrWhiteSpace(owner.NickName))
                throw new ArgumentException("Nick is required!");
        }

        private void ValideteUpdate(FlyOwner owner)
        {
            if(owner == null)
                throw new ArgumentException("Owner is required!");

            if (string.IsNullOrWhiteSpace(owner.Name))
                throw new ArgumentException("Name is required!");

            if (string.IsNullOrWhiteSpace(owner.NickName))
                throw new ArgumentException("Nick is required!");

            if(_ownerRepository.GetById(owner.Id) == null)
                throw new ArgumentException("Owner not exists!");
        }
    }
}
