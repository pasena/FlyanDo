using System.Linq;
using FlyanDo.Entity;
using FlyanDo.Repository.Abstract;

namespace FlyanDo.Repository
{
    public class FlyRepository : IFlyRepository
    {
        private readonly FlyanDoContext _context;

        public FlyRepository(FlyanDoContext context)
        {
            _context = context;
        }

        public IQueryable<Fly> GetAll()
        {
            return _context.Flys;
        }

        public Fly GetById(int flyId)
        {
            return _context.Flys.SingleOrDefault(w => w.Id == flyId);
        }

        public void Insert(Fly fly)
        {
            _context.Flys.Add(fly);
            _context.SaveChanges();
        }

        public void Update(Fly fly)
        {
            var flyToUpdate = GetById(fly.Id);

            if (flyToUpdate != null)
            {
                _context.Entry(flyToUpdate).CurrentValues.SetValues(fly);
            }

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var flyToDelete = GetById(id);

            if (flyToDelete != null)
            {
                _context.Flys.Remove(flyToDelete);
                _context.SaveChanges();
            }
        }
    }
}
