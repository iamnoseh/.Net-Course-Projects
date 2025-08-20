// using Domain.DTOs.ProductDto;
// using Infrastructure.Interfaces;
// using Infrastructure.Responces;
// using Microsoft.AspNetCore.Mvc;
//
// namespace WebApp.Controllers;
// [ApiController]
// [Route("api/[controller]")]
// public class ProductController(IProductServices  service):ControllerBase
// {
//     [HttpPost]
//     public async Task<Responce<string>> CreateProduct(CreateProductDto product)
//     {
//         return await service.AddProduct(product);
//     }
//
//     [HttpPut]
//     public async Task<Responce<string>> UpdateProduct(UpdateProductDto product)
//     {
//         return await service.UpdateProduct(product);
//     }
//
//     [HttpDelete]
//     public async Task<Responce<string>> DeleteProduct(int id)
//     {
//         return await service.DeleteProduct(id);
//     }
//
//     [HttpGet]
//     public async Task<Responce<List<GetProductDto>>> ListProducts()
//     {
//         return await service.ListProducts();
//     }
//
//     [HttpGet("{id}")]
//     public async Task<Responce<GetProductDto>> FindProduct(int id)
//     {
//         return await service.FindProduct(id);
//     }
//
//     [HttpGet("category/{categoryId}")]
//     public async Task<Responce<List<GetProductDto>>> FindProductsByCategories(int categoryId)
//     {
//         return await service.FindProductsByCategories(categoryId);
//     }
//     
// }