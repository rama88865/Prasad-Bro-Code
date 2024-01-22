using INMAR.Service.Helpers;
using INMAR.Service.Interfaces;
using INMAR.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INMAR.Service.Controllers
{
    [IMARAuthorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllProducts()
        {
            var products = await productService.GetAllProduct();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct(long id)
        {
            var product = await productService.GetProduct(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> InsertOrUpdateProduct([FromBody] Product product)
        {
            if (await productService.InsertOrUpdateProduct(product))
                return Ok();

            return BadRequest("Failed to insert or update the product.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(long id)
        {
            if (await productService.DeleteProduct(id))
                return Ok();

            return NotFound();
        }
    }

}
