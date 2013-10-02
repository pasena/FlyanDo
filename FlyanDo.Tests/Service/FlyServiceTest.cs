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
    public class FlyServiceTest
    {
        [TestMethod]
        public void CanGetAll()
        {
            var flyRepository = new Mock<IFlyRepository>();

            flyRepository.Setup(s => s.GetAll()).Returns(new List<Fly>
                {
                    new Fly { Id = 1, DateOfFly = DateTime.Now },
                    new Fly { Id = 2, DateOfFly = DateTime.Now }
                }.AsQueryable());

            var flyService = new FlyService(flyRepository.Object);

            var flys = flyService.GetAll();

            Assert.AreEqual(flys.Count(), 2);
            Assert.AreEqual(flys.First().Id, 1);
            flyRepository.Verify(v => v.GetAll(), Times.Once());
        }

        [TestMethod]
        public void CanGetById()
        {
            var flyRepository = new Mock<IFlyRepository>();

            flyRepository.Setup(s => s.GetById(It.IsAny<int>())).Returns(
                    new Fly { Id = 1, DateOfFly = DateTime.Now });

            var flyService = new FlyService(flyRepository.Object);

            Assert.AreEqual(flyService.GetById(1).Id, 1);
            flyRepository.Verify(v => v.GetById(It.IsAny<int>()), Times.Once());
        }

        [TestMethod]
        public void CanNotInsertWithNullFly()
        {
            var flyRepository = new Mock<IFlyRepository>();
            var flyService = new FlyService(flyRepository.Object);

            try
            {
                flyService.Insert(null);
                Assert.Fail("Validation not implemented!");
            }
            catch (ArgumentException argex)
            {
                Assert.AreEqual(argex.Message, "Fly is required!");
                flyRepository.Verify(v => v.Insert(It.IsAny<Fly>()), Times.Never());
            }
        }

        [TestMethod]
        public void CanNotInsertWithoutDescription()
        {
            var flyRepository = new Mock<IFlyRepository>();
            var flyService = new FlyService(flyRepository.Object);

            var fly = new Fly { DateOfFly = DateTime.Now };

            try
            {
                flyService.Insert(fly);
                Assert.Fail("Validation not implemented!");
            }
            catch (ArgumentException argex)
            {
                Assert.AreEqual(argex.Message, "Description is required!");
                flyRepository.Verify(v => v.Insert(It.IsAny<Fly>()), Times.Never());
            }
        }

        [TestMethod]
        public void CanNotInsertWithoutOwner()
        {
            var flyRepository = new Mock<IFlyRepository>();
            var flyService = new FlyService(flyRepository.Object);

            var fly = new Fly { DateOfFly = DateTime.Now, Description = "First fly" };

            try
            {
                flyService.Insert(fly);
                Assert.Fail("Validation not implemented!");
            }
            catch (ArgumentException argex)
            {
                Assert.AreEqual(argex.Message, "Owner is required!");
                flyRepository.Verify(v => v.Insert(It.IsAny<Fly>()), Times.Never());
            }
        }

        [TestMethod]
        public void CanInsert()
        {
            var flyRepository = new Mock<IFlyRepository>();
            var flyService = new FlyService(flyRepository.Object);

            var fly = new Fly { DateOfFly = DateTime.Now, Description = "First fly", Owner = new FlyOwner { Id = 1, Name = "Owner 1" } };

            flyService.Insert(fly);

            flyRepository.Verify(v => v.Insert(It.IsAny<Fly>()), Times.Once());
        }

        [TestMethod]
        public void CanNotUpdateWithNullFly()
        {
            var flyRepository = new Mock<IFlyRepository>();
            var flyService = new FlyService(flyRepository.Object);

            try
            {
                flyService.Update(null);
                Assert.Fail("Validation not implemented!");
            }
            catch (ArgumentException argex)
            {
                Assert.AreEqual(argex.Message, "Fly is required!");
                flyRepository.Verify(v => v.Update(It.IsAny<Fly>()), Times.Never());
            }
        }

        [TestMethod]
        public void CanNotUpdateWithoutDescription()
        {
            var flyRepository = new Mock<IFlyRepository>();

            flyRepository.Setup(s => s.GetById(1)).Returns(new Fly { Id = 1, DateOfFly = DateTime.Now, Description="First Fly", Owner = new FlyOwner { Id = 1, Name = "Owner 1" } });

            var flyService = new FlyService(flyRepository.Object);

            try
            {
                var fly = flyService.GetById(1);

                fly.Description = string.Empty;

                flyService.Update(fly);

                Assert.Fail("Validation not implemented!");
            }
            catch (ArgumentException argex)
            {
                Assert.AreEqual(argex.Message, "Description is required!");
                flyRepository.Verify(v => v.Update(It.IsAny<Fly>()), Times.Never());
            }

            flyRepository.Verify(v => v.GetById(It.IsAny<int>()), Times.AtLeastOnce());
        }

        [TestMethod]
        public void CanNotUpdateAbsentFly()
        {
            var flyRepository = new Mock<IFlyRepository>();

            flyRepository.Setup(s => s.GetById(1)).Returns(
             new Fly { Id = 1, DateOfFly = DateTime.Now, Owner = new FlyOwner { Id = 1, Name = "Owner 1" } });

            var flyService = new FlyService(flyRepository.Object);

            try
            {
                flyService.Update(new Fly { Id = 0 });
                Assert.Fail("Validation not implemented!");
            }
            catch (ArgumentException argex)
            {
                Assert.AreEqual(argex.Message, "Fly not exists!");
                flyRepository.Verify(v => v.Update(It.IsAny<Fly>()), Times.Never());
            }

            flyRepository.Verify(v => v.GetById(It.IsAny<int>()), Times.Once());
        }

        [TestMethod]
        public void CanNotUpdateWithoutOwner()
        {
            var flyRepository = new Mock<IFlyRepository>();
            var flyService = new FlyService(flyRepository.Object);

            try
            {
                flyService.Update(new Fly { Id = 1 });
                Assert.Fail("Validation not implemented!");
            }
            catch (ArgumentException argex)
            {
                Assert.AreEqual(argex.Message, "Fly not exists!");
                flyRepository.Verify(v => v.Update(It.IsAny<Fly>()), Times.Never());
            }

            flyRepository.Verify(v => v.GetById(It.IsAny<int>()), Times.Once());
        }

        [TestMethod]
        public void CanUpdate()
        {
            var flyRepository = new Mock<IFlyRepository>();

            flyRepository.Setup(s => s.GetById(It.IsAny<int>())).Returns(
                new Fly { Id = 1, DateOfFly = DateTime.Now, Description="First Fly" , Owner = new FlyOwner { Id = 1, Name = "Owner 1" } });

            var flyService = new FlyService(flyRepository.Object);

            var fly = new Fly { Id = 1, DateOfFly = DateTime.Now,  Description="First Fly Update", Owner = new FlyOwner { Id = 2, Name = "Update Owner 1" } };

            flyService.Update(fly);

            flyRepository.Verify(v => v.Update(It.IsAny<Fly>()), Times.Once());
        }

        [TestMethod]
        public void CanDelete()
        {
            var flyRepository = new Mock<IFlyRepository>();

            flyRepository.Setup(s => s.GetById(It.IsAny<int>())).Returns(
                new Fly { Id = 1, DateOfFly = DateTime.Now, Description = "First Fly", Owner = new FlyOwner { Id = 1, Name = "Owner 1" } });

            var flyService = new FlyService(flyRepository.Object);

            flyService.Delete(1);

            flyRepository.Verify(v => v.Delete(It.IsAny<int>()), Times.Once());
        }
    }
}
