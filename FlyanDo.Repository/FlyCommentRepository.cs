﻿using System.Linq;
using FlyanDo.Entity;
using FlyanDo.Repository.Abstract;

namespace FlyanDo.Repository
{
    public class FlyCommentRepository : IFlyCommentRepository
    {
        private FlyanDoContext _context;

        public FlyCommentRepository(FlyanDoContext context)
        {
            _context = context;
        }

        public IQueryable<FlyComment> GetAll()
        {
            return _context.FlyComments;
        }

        public FlyComment GetById(int id)
        {
            return _context.FlyComments.SingleOrDefault(w => w.Id == id);
        }

        public void Insert(FlyComment comment)
        {
            _context.FlyComments.Add(comment);
            _context.SaveChanges();
        }

        public void Update(FlyComment comment)
        {
            if (comment.Id > 0)
            {
                var commentToUpdate = GetById(comment.Id);

                if (commentToUpdate != null)
                {
                    _context.Entry(commentToUpdate).CurrentValues.SetValues(comment);
                }
            }

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var commnetToDelete = GetById(id);

            if (commnetToDelete != null)
            {
                _context.FlyComments.Remove(commnetToDelete);
            }

            _context.SaveChanges();
        }
    }
}
