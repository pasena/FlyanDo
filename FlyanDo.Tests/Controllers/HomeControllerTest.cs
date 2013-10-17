using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using FlyanDo.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlyanDo.Web;
using FlyanDo.Web.Controllers;
using Moq;
using FlyanDo.Service.Abstract;

namespace FlyanDo.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            var flyService = new Mock<IFlyService>();

            // Arrange
            HomeController controller = new HomeController(flyService.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("Modify this template to jump-start your ASP.NET MVC application.", result.ViewBag.Message);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController(It.IsAny<IFlyService>());

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController(It.IsAny<IFlyService>());

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
