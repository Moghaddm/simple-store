using Castle.DynamicProxy.Contributors;
using CRUD.Api.Controllers;
using CRUD.Api.Domain;
using DTOs;
using EntityFrameworkCoreMock;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CRUD.Tests.Controllers
{
    public class ProductsControllerTest
    {
        private readonly ProductsController _controller;
        // private ProductsController ProductControllerInit(DbContextMock<StoreContext> dbContextMock)
        // {
        //     return new ProductsController(dbContextMock.Object);
        // }

        private List<Product> getInitialDbEntities()
        {
            return new List<Product> {
             new Product {Id = 1, Name="Test1", Description = "Test Desc 1"},
             new Product {Id = 2, Name="Test2", Description = "Test Desc 2"},
             new Product {Id = 3, Name="Test3", Description = "Test Desc 3"}
        };
        }

        private DbContextMock<StoreContext> getDbContext(List<Product> initialEntities)
        {
            DbContextMock<StoreContext> dbContextMock = new DbContextMock<StoreContext>(
                new DbContextOptionsBuilder<StoreContext>().Options);
            dbContextMock.CreateDbSetMock(item => item.Products);
            return dbContextMock;
        }

        [Fact]
        public async void GetAllProducts_Test()
        {
            // Arrange
            //int id = 4;
            //var product = new Product();
            DbContextMock<StoreContext> dbContextMock = getDbContext(getInitialDbEntities());
            // ProductsController productsController = ProductControllerInit(dbContextMock);

            // Act
            var result =await _controller.Get();
            // Assert
            Assert.IsType<OkObjectResult>(result);
            //Assert.Equal(1,items.Count);
        }
        [Fact]
        public async void GetByIdProduct_Test()
        {
            // Arrange
            int id = 1;
            //var product = new Product();
            //DbContextMock<StoreContext> dbContextMock = getDbContext(getInitialDbEntities());
            //ProductsController productsController = ProductControllerInit(dbContextMock);
            DbContextMock<StoreContext> dbContextMock = getDbContext(getInitialDbEntities());
            // ProductsController productsController = ProductControllerInit(dbContextMock);
            // Act
            // var result = productsController.GetById(2);

            // Assert
            // Assert.Equal(id, result.Id);
            //Assert.IsType<OkObjectResult>(result);
            //Assert.Equal(1,items.Count);
        }
    }
}
