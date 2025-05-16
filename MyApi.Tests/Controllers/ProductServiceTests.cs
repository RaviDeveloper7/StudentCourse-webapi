using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using StudentCourseAPI;
using StudentCourseAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using StudentCourseAPI.Repositories;
using StudentCourseAPI.Models;
using AutoMapper;
using StudentCourseAPI.Services;
using StudentCourseAPI.DTOs;

namespace MyApi.Tests.Controllers
{
    public class ProductServiceTests
    {
        private readonly Mock<IRepository<Product>> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ProductService _service;


        public ProductServiceTests()
        {
            _mockRepo = new Mock<IRepository<Product>>();
            _mockMapper = new Mock<IMapper>();
            _service = new ProductService(_mockRepo.Object, _mockMapper.Object);

        }


        [Fact]
        public async Task GetAllProductAsync_ReturnsMappedDTOs()
        {
            var products = new List<Product>
            {
            new Product { Id = 1, Name = "Item 1", Price = 10 },
            new Product { Id = 2, Name = "Item 2", Price = 20 }
            };

            var productDTOs = new List<ProductReadDto>
            {
            new ProductReadDto { Id = 1, Name = "Item 1", Price = 10 },
            new ProductReadDto { Id = 2, Name = "Item 2", Price = 20 }
            };

            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(products);
            _mockMapper.Setup(m => m.Map<IEnumerable<ProductReadDto>>(products)).Returns(productDTOs);

            var result = await _service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Collection(result,
             item => Assert.Equal("Item 1", item.Name),
             item => Assert.Equal("Item 2", item.Name)
             );
        }

    }
}
