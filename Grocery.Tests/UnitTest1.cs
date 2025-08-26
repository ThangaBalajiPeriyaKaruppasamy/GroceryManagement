using GroceryManagement.API.Controllers;
using GroceryManagement.API.Models;
using GroceryManagement.API.Services;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace GroceryManagement.Tests
{
    [TestFixture]
    public class GroceryControllerTests
    {
        private GroceryController _controller = null!;
        private IGroceryService _service = null!;

        [SetUp]
        public void Setup()
        {
            _service = new InMemoryGroceryService();
            _controller = new GroceryController(_service);
        }

        [Test]
        public void GetAll_InitiallyEmpty_ReturnsEmptyList()
        {
            var result = _controller.GetAll().Result as OkObjectResult;
            Assert.IsNotNull(result);
            var items = result.Value as IEnumerable<GroceryItem>;
            Assert.IsNotNull(items);
            Assert.IsEmpty(items);
        }

        [Test]
        public void Create_ValidItem_ReturnsCreatedItemWithId()
        {
            var item = new GroceryItem { Name = "Apple", Quantity = 5, Price = 1.99m };
            var result = _controller.Create(item).Result as CreatedAtRouteResult;
            Assert.IsNotNull(result);
            var created = result.Value as GroceryItem;
            Assert.IsNotNull(created);
            Assert.AreEqual(1, created.Id);
            Assert.AreEqual(item.Name, created.Name);
        }

        [Test]
        public void GetById_ExistingItem_ReturnsItem()
        {
            var created = _service.Create(new GroceryItem { Name = "Banana", Quantity = 3, Price = 0.99m });
            var result = _controller.GetById(created.Id).Result as OkObjectResult;
            Assert.IsNotNull(result);
            var fetched = result.Value as GroceryItem;
            Assert.IsNotNull(fetched);
            Assert.AreEqual(created.Id, fetched.Id);
        }

        [Test]
        public void GetById_NonExisting_ReturnsNotFound()
        {
            var result = _controller.GetById(999).Result;
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Update_ExistingItem_UpdatesFields()
        {
            var created = _service.Create(new GroceryItem { Name = "Carrot", Quantity = 2, Price = 0.50m });
            var updated = new GroceryItem { Name = "Carrot", Quantity = 10, Price = 0.45m };

            var result = _controller.Update(created.Id, updated);
            Assert.IsInstanceOf<NoContentResult>(result);

            var fetched = _service.GetById(created.Id);
            Assert.IsNotNull(fetched);
            Assert.AreEqual(10, fetched.Quantity);
            Assert.AreEqual(0.45m, fetched.Price);
        }

        [Test]
        public void Update_NonExisting_ReturnsNotFound()
        {
            var result = _controller.Update(999, new GroceryItem());
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Delete_ExistingItem_RemovesItem()
        {
            var created = _service.Create(new GroceryItem { Name = "Dates", Quantity = 1, Price = 3.50m });
            var result = _controller.Delete(created.Id);
            Assert.IsInstanceOf<NoContentResult>(result);

            Assert.IsNull(_service.GetById(created.Id));
        }

        [Test]
        public void Delete_NonExisting_ReturnsNotFound()
        {
            var result = _controller.Delete(999);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
