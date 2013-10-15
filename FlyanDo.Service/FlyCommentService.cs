using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyanDo.Service.Abstract;
using FlyanDo.Entity;
using FlyanDo.Repository.Abstract;

namespace FlyanDo.Service
{
    public class FlyCommentService : IFlyCommentService
    {
        private IFlyCommentRepository _commentRepository;

        public FlyCommentService(IFlyCommentRepository commentRepo)
        {
            _commentRepository = commentRepo;
        }

        public IQueryable<FlyComment> GetAll()
        {
            return _commentRepository.GetAll();
        }

        public FlyComment GetById(int id)
        {
            return GetAll().FirstOrDefault(f => f.Id == id);
        }

        public void Insert(FlyComment comment)
        {
            ValidateInsert(comment);
            _commentRepository.Insert(comment);
        }

        public void Delete(int id)
        {
            _commentRepository.Delete(id);
        }

        private void ValidateInsert(FlyComment comment)
        {
            if (comment == null)
                throw new ArgumentException("FlyComment is required!");

            if (comment.Author == null)
                throw new ArgumentException("Author is required!");

            if (comment.Fly == null)
                throw new ArgumentException("Fly is required!");

            if (string.IsNullOrWhiteSpace(comment.Comment))
                throw new ArgumentException("Comment is required!");
        }

    }
}
