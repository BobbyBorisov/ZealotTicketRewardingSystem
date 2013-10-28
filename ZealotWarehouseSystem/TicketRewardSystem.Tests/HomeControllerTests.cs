using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using TicketRewardSystem.Repository;
using TicketRewardSystem.Controllers;
using TicketRewardSystem.Models;
using System.Collections.Generic;
using Kendo.Mvc;
using Kendo.Mvc.UI;
using System.Web.Mvc;
using TicketRewardSystem.ViewModels;

namespace TicketRewardSystem.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void CreatePageShouldReturnProperCountOfTickets()
        {
            var list = new List<Ticket>();
            list.Add(new Ticket());
            list.Add(new Ticket());

            var ticketRepoMock = new Mock<IRepository<Ticket>>();
            ticketRepoMock.Setup(x => x.All()).Returns(list.AsQueryable());

            var uofMock = new Mock<IUowData>();
            uofMock.Setup(x => x.Tickets).Returns(ticketRepoMock.Object);

            var kendoDataRequest = new DataSourceRequest();
            var controller = new HomeController(uofMock.Object);

            Ticket ticket = new Ticket()
            {
                Title = "Ticket title",
                Description = "Description of ticket"
            };

            //get the result back from the controller
            var controllerResult = controller.Create(ticket);
            var a = 5;

        }

        [TestMethod]
        public void TicketPageShouldReturnProperCountOfTickets()
        {
            var list = new List<Ticket>();
            list.Add(new Ticket());
            list.Add(new Ticket());

            var ticketRepoMock = new Mock<IRepository<Ticket>>();
            ticketRepoMock.Setup(x => x.All()).Returns(list.AsQueryable());

            var uofMock = new Mock<IUowData>();
            uofMock.Setup(x => x.Tickets).Returns(() => { return ticketRepoMock.Object; });

            //var kendoDataRequest = new DataSourceRequest();
            var controller = new HomeController(uofMock.Object);

            var viewResult = controller.Tickets();
            var model = viewResult as IEnumerable<Ticket>;
            Assert.IsNotNull(model, "The model is null");
            Assert.AreEqual(2, model.Count());

        }
    }
}
