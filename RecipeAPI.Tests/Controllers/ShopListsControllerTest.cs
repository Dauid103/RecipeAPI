using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RecipeAPI.Controllers;
using RecipeAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RecipeAPI.Tests.Controllers
{
    public class ShopListsControllerTest
    {

        private readonly Mock<IRepository> _mockRepository;
        private readonly Mock<ILogger<ShopListsController>> _mockLogger;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ShopListsController _shopListsController;

        public ShopListsControllerTest()
        {
            _mockRepository = new Mock<IRepository>();
            _mockLogger = new Mock<ILogger<ShopListsController>>();
            _mockMapper = new Mock<IMapper>();
            _shopListsController = new ShopListsController(_mockRepository.Object, _mockLogger.Object, _mockMapper.Object);
        }


        [Fact]
        public void GetAllShopLists_WhenCalled_ReturnsListWithShopListModels()
        {

            var shopListModels = new List<ShopListModel> { new ShopListModel { ShopListId = 1, Name = "ShopList1", CreatedDate = DateTime.Now } };
            
            _mockMapper.Setup(m => m.Map<List<ShopListModel>>(It.IsAny<List<ShopList>>())).Returns(shopListModels);

            var okResult = _shopListsController.GetShopLists();

            var items = Assert.IsType<List<ShopListModel>>(okResult.Value);
            Assert.NotEmpty(items);

        }

    }
}
