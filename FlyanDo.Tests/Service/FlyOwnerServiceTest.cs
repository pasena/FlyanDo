using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlyanDo.Repository.Abstract;
using FlyanDo.Repository;
using FlyanDo.Entity;
using FlyanDo.Service;
using Moq;
using System.Linq;
using System.Collections.Generic;

namespace FlyanDo.Tests.Service
{
    [TestClass]
    public class FlyOwnerServiceTest
    {
        [TestMethod]
        public void CanGetAll()
        {
            var flyOwnerRepository = new Mock<IFlyOwnerRepository>();

            flyOwnerRepository.Setup(s => s.GetAll()).Returns(new List<FlyOwner>
                {
                    new FlyOwner { Id = 1, Name = "First Owner", NickName="Nick", PicturePath="picture" },
                    new FlyOwner { Id = 2, Name = "Second Owner", NickName="Name", PicturePath="picture" }
                }.AsQueryable());

            var ownerService = new FlyOwnerService(flyOwnerRepository.Object);

            var owners = ownerService.GetAll();

            Assert.AreEqual(owners.Count(), 2);
            Assert.AreEqual(owners.First().Id, 1);
        }

        [TestMethod]
        public void CanGetById()
        {
            var ownerRepository = new Mock<IFlyOwnerRepository>();

            ownerRepository.Setup(s => s.GetById(1)).Returns(new FlyOwner { Id = 1, Name = "First Owner", NickName = "Nick", PicturePath = "picture" });

            var ownerService = new FlyOwnerService(ownerRepository.Object);

            var owners = ownerService.GetById(1);

            Assert.AreEqual(owners.Id, 1);
            Assert.AreEqual(owners.Name, "First Owner");
        }

        [TestMethod]
        public void CanNotInsertWithNullOwner()
        {
            var ownerRepository = new Mock<IFlyOwnerRepository>();
            var ownerService = new FlyOwnerService(ownerRepository.Object);

            try
            {
                ownerService.Insert(null);
                Assert.Fail("Validation not implemented!");
            }
            catch (ArgumentException argex)
            {
                Assert.AreEqual("Owner is required!", argex.Message);
                ownerRepository.Verify(v => v.Insert(It.IsAny<FlyOwner>()), Times.Never());
            }
        }

        [TestMethod]
        public void CanNotInsertWithoutName()
        {
            var ownerRepository = new Mock<IFlyOwnerRepository>();
            var ownerService = new FlyOwnerService(ownerRepository.Object);

            try
            {
                ownerService.Insert(new FlyOwner { Name = string.Empty });

                Assert.Fail("Validation not implemented!");
            }
            catch (ArgumentException argex)
            {
                Assert.AreEqual("Name is required!", argex.Message);
                ownerRepository.Verify(v => v.Insert(It.IsAny<FlyOwner>()), Times.Never());
            }
        }

        [TestMethod]
        public void CanNotInsertWithoutNickName()
        {
            var ownerRepository = new Mock<IFlyOwnerRepository>();
            var ownerService = new FlyOwnerService(ownerRepository.Object);

            try
            {
                ownerService.Insert(new FlyOwner { Name = "Firt Owner", NickName = string.Empty });

                Assert.Fail("Validation not implemented!");
            }
            catch (ArgumentException argex)
            {
                Assert.AreEqual("Nick is required!", argex.Message);
                ownerRepository.Verify(v => v.Insert(It.IsAny<FlyOwner>()), Times.Never());
            }
        }

        [TestMethod]
        public void CanInsertOwner()
        {
            var ownerRepository = new Mock<IFlyOwnerRepository>();
            var ownerService = new FlyOwnerService(ownerRepository.Object);

            ownerService.Insert(new FlyOwner { Name = "Firt Owner", NickName = "Nick" });

            ownerRepository.Verify(v => v.Insert(It.IsAny<FlyOwner>()), Times.Once());
        }

        [TestMethod]
        public void CanNotUpdateWithNullOwner()
        {
            var ownerRepository = new Mock<IFlyOwnerRepository>();
            var ownerService = new FlyOwnerService(ownerRepository.Object);

            try
            {
                ownerService.Update(null);
                Assert.Fail("Validation not implemented!");
            }
            catch (ArgumentException argex)
            {
                Assert.AreEqual("Owner is required!", argex.Message);
                ownerRepository.Verify(v => v.Update(It.IsAny<FlyOwner>()), Times.Never());
            }
        }

        [TestMethod]
        public void CanNotUpdatetWithoutName()
        {
            var ownerRepository = new Mock<IFlyOwnerRepository>();
            var ownerService = new FlyOwnerService(ownerRepository.Object);

            try
            {
                ownerService.Update(new FlyOwner { Name = string.Empty });

                Assert.Fail("Validation not implemented!");
            }
            catch (ArgumentException argex)
            {
                Assert.AreEqual("Name is required!", argex.Message);
                ownerRepository.Verify(v => v.Update(It.IsAny<FlyOwner>()), Times.Never());
            }
        }

        [TestMethod]
        public void CanNotUpdateWithoutNickName()
        {
            var ownerRepository = new Mock<IFlyOwnerRepository>();
            var ownerService = new FlyOwnerService(ownerRepository.Object);

            try
            {
                ownerService.Update(new FlyOwner { Name = "Firt Owner", NickName = string.Empty });

                Assert.Fail("Validation not implemented!");
            }
            catch (ArgumentException argex)
            {
                Assert.AreEqual("Nick is required!", argex.Message);
                ownerRepository.Verify(v => v.Update(It.IsAny<FlyOwner>()), Times.Never());
            }
        }

        [TestMethod]
        public void CanNotUpdateAbsentOwner()
        {
            var ownerRepository = new Mock<IFlyOwnerRepository>();

            ownerRepository.Setup(s => s.GetById(1)).Returns(new FlyOwner { Id = 1, Name = "First Owner", NickName = "Nick" });

            var ownerService = new FlyOwnerService(ownerRepository.Object);

            try
            {
                var owner = new FlyOwner { Id = 2, Name = "Second Owner", NickName = "Nick" };
                ownerService.Update(owner);

                Assert.Fail("Validation not Implemented!");
            }
            catch (ArgumentException argex)
            {
                Assert.AreEqual("Owner not exists!", argex.Message);
                ownerRepository.Verify(v => v.Update(It.IsAny<FlyOwner>()), Times.Never());
            }
        }

        [TestMethod]
        public void CanUpdate()
        {
            var ownerRepository = new Mock<IFlyOwnerRepository>();

            ownerRepository.Setup(s => s.GetById(1)).Returns(new FlyOwner { Id = 1, Name = "First Owner", NickName = "Nick" });

            var ownerService = new FlyOwnerService(ownerRepository.Object);

            var owner = new FlyOwner { Id = 1, Name = "Second Owner", NickName = "Nick" };

            ownerService.Update(owner);
            
            ownerRepository.Verify(v => v.Update(It.IsAny<FlyOwner>()), Times.Once());
        }

        [TestMethod]
        public void CanDelete()
        {
            var ownerRepository = new Mock<IFlyOwnerRepository>();

            ownerRepository.Setup(s => s.GetById(1)).Returns(new FlyOwner { Id = 1, Name = "First Owner", NickName = "Nick" });

            var ownerService = new FlyOwnerService(ownerRepository.Object);

            ownerService.Delete(1);

            ownerRepository.Verify(v => v.Delete(It.IsAny<int>()), Times.Once());
        }
    }
}
