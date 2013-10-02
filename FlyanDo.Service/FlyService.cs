using System;
using System.Linq;
using System.Transactions;
using FlyanDo.Entity;
using FlyanDo.Repository.Abstract;
using FlyanDo.Service.Abstract;


namespace FlyanDo.Service
{
    public class FlyService : IFlyService
    {
        private IFlyRepository _flyRepository;

        public FlyService(IFlyRepository flyRepo)
        {
            _flyRepository = flyRepo;
        }

        public IQueryable<Fly> GetAll()
        {
            return _flyRepository.GetAll();
        }

        public Fly GetById(int id)
        {
            return _flyRepository.GetById(id);
        }

        public void Insert(Fly fly)
        {
            ValidadeInsert(fly);

            using (var scope = new TransactionScope())
            {
                _flyRepository.Insert(fly);
                scope.Complete();
            }
        }

        public void Update(Fly fly)
        {
            ValidateUpdate(fly);

            using (var scope = new TransactionScope())
            {
                _flyRepository.Update(fly);
                scope.Complete();
            }
        }

        public void Delete(int id)
        {
            using (var scope = new TransactionScope())
            {
                _flyRepository.Delete(id);
                scope.Complete();
            }
        }
        
        private void ValidadeInsert(Fly fly)
        {
            if (fly == null)
                throw new ArgumentException("Fly is required!");

            if (string.IsNullOrWhiteSpace(fly.Description))
                throw new ArgumentException("Description is required!");

            if (fly.Owner == null)
                throw new ArgumentException("Owner is required!");
        }

        private void ValidateUpdate(Fly fly)
        {
            if (fly == null)
                throw new ArgumentException("Fly is required!");

            if(_flyRepository.GetById(fly.Id) == null)
                throw new ArgumentException("Fly not exists!");

            if(string.IsNullOrWhiteSpace(fly.Description))
                throw new ArgumentException("Description is required!");
        }
    }
}
