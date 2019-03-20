using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RecipeAPI.Controllers;
using RecipeAPI.Models;
using RecipeAPI.Models.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace RecipeAPI.Tests.Controllers
{
    public class ShopListsControllerTest
    {

        private readonly Mock<IRepository> _mockRepository;
        private readonly Mock<ILogger<ShopListsController>> _mockLogger;
        private readonly IMapper _mapper;
        private readonly ShopListsController _shopListsController;

        public ShopListsControllerTest()
        {
            _mockRepository = new Mock<IRepository>();
            _mockLogger = new Mock<ILogger<ShopListsController>>();
            _mapper = InitMapper();
            _shopListsController = new ShopListsController(_mockRepository.Object, _mockLogger.Object, _mapper);
        }

        public IMapper InitMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();

            return mapper;
        }

        [Fact]
        public void GetAllShopLists_WhenCalled_ReturnsListWithShopListModels()
        {

            _mockRepository.Setup(x => x.GetAllShopLists()).Returns(new List<ShopList> { new ShopList { ShopListId = 1, Name = "ShopList1", CreatedDate = DateTime.Now } });

            var result = _shopListsController.GetShopLists();

            var actionResult = Assert.IsType<ActionResult<List<ShopListModel>>>(result);
            var returnValue = Assert.IsType<List<ShopListModel>>(actionResult.Value);
            var shopList = returnValue.FirstOrDefault();
            Assert.Equal("ShopList1", shopList.Name);
        }


        [Fact]
        public void GetShopListById_WhenCalled_ReturnsNotFoundObjectForNonExistingShopList()
        {
            int shopListId = 999;

            _mockRepository.Setup(x => x.GetShopListById(shopListId)).Returns((ShopList)null);

            var result = _shopListsController.GetShopList(shopListId);

            var actionResult = Assert.IsType<ActionResult<ShopListModel>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }



    }
}
