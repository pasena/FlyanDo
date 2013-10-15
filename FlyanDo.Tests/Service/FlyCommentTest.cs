using System;
using System.Collections.Generic;
using FlyanDo.Entity;
using FlyanDo.Repository.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using FlyanDo.Service;

namespace FlyanDo.Tests.Service
{
    [TestClass]
    public class FlyCommentTest
    {
        [TestMethod]
        public void CanGetAll()
        {
            var commentRepository = new Mock<IFlyCommentRepository>();

            commentRepository.Setup(s => s.GetAll())
                             .Returns(new List<FlyComment>
                                 {
                                     new FlyComment {Id = 1, Author = new FlyOwner {Id = 1}, Fly = new Fly {Id = 1}, Comment = "Comment" },
                                     new FlyComment {Id = 2, Author = new FlyOwner {Id = 2}, Fly = new Fly {Id = 1}, Comment = "Comment 2" }
                                 }.AsQueryable());

            var commentService = new FlyCommentService(commentRepository.Object);

            var comments = commentService.GetAll();

            Assert.AreEqual(comments.Count(), 2);
            commentRepository.Verify(v => v.GetAll(), Times.Once());
        }

        [TestMethod]
        public void CanGetById()
        {
            var commentRepository = new Mock<IFlyCommentRepository>();

            commentRepository.Setup(s => s.GetAll())
                             .Returns(new List<FlyComment>
                                 {
                                     new FlyComment {Id = 1, Author = new FlyOwner {Id = 1}, Fly = new Fly {Id = 1}, Comment = "Comment" },
                                     new FlyComment {Id = 2, Author = new FlyOwner {Id = 2}, Fly = new Fly {Id = 1}, Comment = "Comment 2" }
                                 }.AsQueryable());

            var commentService = new FlyCommentService(commentRepository.Object);

            var comment = commentService.GetById(1);

            Assert.AreEqual(comment.Id, 1);
            commentRepository.Verify(v => v.GetAll(), Times.Once());
        }

        [TestMethod]
        public void CanNotInsertFlyComment()
        {
            var commentRepository = new Mock<IFlyCommentRepository>();

            commentRepository.Setup(s => s.GetAll())
                             .Returns(new List<FlyComment>
                                 {
                                     new FlyComment {Id = 1, Author = new FlyOwner {Id = 1}, Fly = new Fly {Id = 1}, Comment = "Comment" },
                                     new FlyComment {Id = 2, Author = new FlyOwner {Id = 2}, Fly = new Fly {Id = 1}, Comment = "Comment 2" }
                                 }.AsQueryable());

            var commentService = new FlyCommentService(commentRepository.Object);

            try
            {
                commentService.Insert(null);
                Assert.Fail("Validation not implemented!");
            }
            catch (ArgumentException argx)
            {
                Assert.AreEqual("FlyComment is required!", argx.Message);
                commentRepository.Verify(v => v.Insert(It.IsAny<FlyComment>()), Times.Never());
            }
            catch (Exception)
            {
                Assert.Fail("Validation not implemented!");
            }
        }

        [TestMethod]
        public void CanNotInsertCommentWithotAutor()
        {
            var commentRepository = new Mock<IFlyCommentRepository>();

            commentRepository.Setup(s => s.GetAll())
                             .Returns(new List<FlyComment>
                                 {
                                     new FlyComment {Id = 1, Author = new FlyOwner {Id = 1}, Fly = new Fly {Id = 1}, Comment = "Comment" },
                                     new FlyComment {Id = 2, Author = new FlyOwner {Id = 2}, Fly = new Fly {Id = 1}, Comment = "Comment 2" }
                                 }.AsQueryable());

            var commentService = new FlyCommentService(commentRepository.Object);

            try
            {
                var comment = new FlyComment();
                commentService.Insert(comment);
                Assert.Fail("Validation not implemented!");
            }
            catch (ArgumentException argx)
            {
                Assert.AreEqual("Author is required!", argx.Message);
                commentRepository.Verify(v => v.Insert(It.IsAny<FlyComment>()), Times.Never());
            }
            catch (Exception)
            {
                Assert.Fail("Validation not implemented!");
            }
        }

        [TestMethod]
        public void CanNotInsertCommentWithotFly()
        {
            var commentRepository = new Mock<IFlyCommentRepository>();

            commentRepository.Setup(s => s.GetAll())
                             .Returns(new List<FlyComment>
                                 {
                                     new FlyComment {Id = 1, Author = new FlyOwner {Id = 1}, Fly = new Fly {Id = 1}, Comment = "Comment" },
                                     new FlyComment {Id = 2, Author = new FlyOwner {Id = 2}, Fly = new Fly {Id = 1}, Comment = "Comment 2" }
                                 }.AsQueryable());

            var commentService = new FlyCommentService(commentRepository.Object);

            try
            {
                var comment = new FlyComment { Author = new FlyOwner { Id = 1 } };
                commentService.Insert(comment);
                Assert.Fail("Validation not implemented!");
            }
            catch (ArgumentException argx)
            {
                Assert.AreEqual("Fly is required!", argx.Message);
                commentRepository.Verify(v => v.Insert(It.IsAny<FlyComment>()), Times.Never());
            }
            catch (Exception)
            {
                Assert.Fail("Validation not implemented!");
            }
        }

        [TestMethod]
        public void CanNotInsertWithotComment()
        {
            var commentRepository = new Mock<IFlyCommentRepository>();

            commentRepository.Setup(s => s.GetAll())
                             .Returns(new List<FlyComment>
                                 {
                                     new FlyComment {Id = 1, Author = new FlyOwner {Id = 1}, Fly = new Fly {Id = 1}, Comment = "Comment" },
                                     new FlyComment {Id = 2, Author = new FlyOwner {Id = 2}, Fly = new Fly {Id = 1}, Comment = "Comment 2" }
                                 }.AsQueryable());

            var commentService = new FlyCommentService(commentRepository.Object);

            try
            {
                var comment = new FlyComment { Author = new FlyOwner { Id = 1 }, Fly = new Fly { Id = 1 } };
                commentService.Insert(comment);
                Assert.Fail("Validation not implemented!");
            }
            catch (ArgumentException argx)
            {
                Assert.AreEqual("Comment is required!", argx.Message);
                commentRepository.Verify(v => v.Insert(It.IsAny<FlyComment>()), Times.Never());
            }
            catch (Exception)
            {
                Assert.Fail("Validation not implemented!");
            }
        }

        [TestMethod]
        public void CanInsertComment()
        {
            var commentRepository = new Mock<IFlyCommentRepository>();

            commentRepository.Setup(s => s.GetAll())
                             .Returns(new List<FlyComment>
                                 {
                                     new FlyComment {Id = 1, Author = new FlyOwner {Id = 1}, Fly = new Fly {Id = 1}, Comment = "Comment" },
                                     new FlyComment {Id = 2, Author = new FlyOwner {Id = 2}, Fly = new Fly {Id = 1}, Comment = "Comment 2" }
                                 }.AsQueryable());

            var commentService = new FlyCommentService(commentRepository.Object);

            var comment = new FlyComment { Author = new FlyOwner { Id = 1 }, Fly = new Fly { Id = 1 }, Comment = "Comment 3" };

            commentService.Insert(comment);

            commentRepository.Verify(v => v.Insert(It.IsAny<FlyComment>()), Times.Once());
        }

        [TestMethod]
        public void CanDeleteComment()
        {
            var commentRepository = new Mock<IFlyCommentRepository>();

            commentRepository.Setup(s => s.GetAll())
                             .Returns(new List<FlyComment>
                                 {
                                     new FlyComment {Id = 1, Author = new FlyOwner {Id = 1}, Fly = new Fly {Id = 1}, Comment = "Comment" },
                                     new FlyComment {Id = 2, Author = new FlyOwner {Id = 2}, Fly = new Fly {Id = 1}, Comment = "Comment 2" }
                                 }.AsQueryable());

            var commentService = new FlyCommentService(commentRepository.Object);

            commentService.Delete(1);

            commentRepository.Verify(v => v.Delete(It.IsAny<int>()), Times.Once());
        }

    }
}
