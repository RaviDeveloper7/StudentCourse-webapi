using AutoMapper;
using Moq;
using StudentCourseAPI.DTOs;
using StudentCourseAPI.Models;
using StudentCourseAPI.Repositories;
using StudentCourseAPI.Services;

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
            new ProductReadDto  { Id = 1, Name = "Item 1", Price = 10 },
            new ProductReadDto { Id = 2, Name = "Item 2", Price = 20 }
            };

            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(products);
            _mockMapper.Setup(m => m.Map<IEnumerable<ProductReadDto>>(products)).Returns(productDTOs);

            var result = await _service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Collection(result,
            item =>
            {
               Assert.Equal("Item 1", item.Name);
               Assert.Equal(10, item.Price);
            },
            item =>
            {
               Assert.Equal("Item 2", item.Name);
               Assert.Equal(20, item.Price);
            });
        }

        [Fact]
        public async Task GetProductByIdAsync_ValidId_ReturnsMappedDTO()
        {
            var product = new Product { Id = 1, Name = "Ash", Price = 10 };

            var productDTO = new ProductReadDto { Id = 1, Name = "Ash", Price = 10 };

            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(product);
            _mockMapper.Setup(m => m.Map<ProductReadDto>(product)).Returns(productDTO);

            var result = await _service.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Ash", result.Name);

        }


        [Fact]
        public async Task CreateAsync_ReturnsCreatedDTO()
        {
            var productCreateDTO = new ProductCreateDto { Name = "Ash", Price = 10 };
            var product = new Product { Id = 1, Name = "Ash", Price = 10 };
            var mappedResultDTO = new ProductReadDto { Id = 1, Name = "Ash", Price = 10 }; // Or ProductReadDto if that's your return type

            _mockMapper.Setup(m => m.Map<Product>(productCreateDTO)).Returns(product);  // Mapping DTO to Entity
            _mockRepo.Setup(r => r.AddAsync(It.IsAny<Product>())).ReturnsAsync(product); // Repo returns product
            _mockMapper.Setup(m => m.Map<ProductReadDto>(product)).Returns(mappedResultDTO); // Entity back to DTO

            var result = await _service.CreateAsync(productCreateDTO);

            Assert.NotNull(result); 
            Assert.Equal("Ash", result.Name);
        }   
                
        [Fact]
        public async Task UpdateAsynce_ReturnsUpdatedDTO()
        {
            var productId = 1;  
            var productUpdateDto = new ProductUpdateDto { name = "Ash", price = 20 };
            var existingProduct = new Product { Id = productId, Name = "Ash", Price = 10 };
            var updatedProduct = new Product { Id = productId, Name = "Ash", Price = 20 };

            _mockRepo.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(existingProduct);
            _mockMapper.Setup(m => m.Map(productUpdateDto,existingProduct)).Returns(existingProduct);

            var result = await _service.UpdateAsync(productId, productUpdateDto);

            Assert.True(result);
        

        }

        [Fact]
        public async Task DeleteAsync_ValidId_ReturnsTrue()
        {
            var productId = 1;

            var existingProduct = new Product { Id = productId, Name = "Ash", Price = 10 };
            _mockRepo.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(existingProduct);
            _mockRepo.Setup(r => r.DeleteAsync(productId)).Returns(Task.CompletedTask);

            var result = await _service.DeleteAsync(productId);
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_NonExistingProduct_ReturnsFalse()
        {
            var invalidProductId = 2;

            _mockRepo.Setup(r => r.GetByIdAsync(invalidProductId)).ReturnsAsync((Product)null);

            var result = await _service.DeleteAsync(invalidProductId);

            Assert.False(result);
            _mockRepo.Verify(r => r.GetByIdAsync(invalidProductId), Times.Once);
            _mockRepo.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Never);
        }
    }
}
