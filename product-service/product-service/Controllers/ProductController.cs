
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using product_service.Models;
using MassTransit;
using shared;
using System.Linq;

namespace product_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IHostEnvironment _environment;


        private readonly IProductRepository ProductRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        public ProductController(IProductRepository ProductRepository, IPublishEndpoint publishEndpoint, IHostEnvironment environment)
        {
            this.ProductRepository = ProductRepository;
            this._publishEndpoint = publishEndpoint;
            this._environment = environment;
        }
        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            try
            {
                return Ok(await ProductRepository.GetProducts());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }

        [HttpGet("/spareParts/{productId:int}")]
        public async Task<ActionResult> GetSpareParts(int productId)
        {
            try
            {
                return Ok(await ProductRepository.GetSpareParts(productId));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                var result = await ProductRepository.GetProduct(id);
                if (result == null) return NotFound();
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product Product)
        {
            try
            {
                if (Product == null)
                {
                    return BadRequest();
                }
                else
                // Add custom model validation error
                {
                    var prod = await ProductRepository.GetProductByProductName(Product.ProductName);
                    if (prod != null)
                    {
                        ModelState.AddModelError("name", "Product name already in use");
                        return BadRequest(ModelState);
                    }
                    else
                    {
                        var createdProduct = await ProductRepository.AddProduct(Product);
                        await _publishEndpoint.Publish<shared.ProductCreated>(new shared.ProductCreated
                        {
                            Id = createdProduct.ProductId,
                            Name = createdProduct.ProductName,
                            Photo = createdProduct.ProductPhoto,

                        });

                        return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.ProductId },
                        createdProduct);
                    }
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }



        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            try
            {
                var ProductToDelete = await ProductRepository.GetProduct(id);
                if (ProductToDelete == null)
                {
                    return NotFound($"Product with Id ={id} not found");

                }
                return await ProductRepository.DeleteProduct(id);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }
        }
  

        [HttpPost("/images")]
        public async Task<IActionResult> Post([FromForm] IFormFile image)
        {
            if (image == null || image.Length == 0)
                return BadRequest("Upload a file");

            string fileName = image.FileName;
            string extension = Path.GetExtension(fileName);

            string[] allowedExtensions = { ".jpg", ".png", ".bmp" };

            if (!allowedExtensions.Contains(extension))
                return BadRequest("File is not a valid image");

          //  string newFileName = $"{productName}{extension}";
            string filePath = Path.Combine(_environment.ContentRootPath, "wwwroot", "Images", fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await image.CopyToAsync(fileStream);
            }


            return Ok($"Images/{fileName}");
        }

        [HttpGet("/image")]
        public async Task<IActionResult> GetImage(string imageName)
        {

            {

                Byte[] b;
                b = await System.IO.File.ReadAllBytesAsync(Path.Combine(_environment.ContentRootPath, "wwwroot", "Images", $"{imageName}"));
                return File(b, "image/jpeg");
            }

        }
    }
}

//[HttpPut("{id:int}")]
//public async Task<ActionResult<Product>> UpdateProduct(int id, Product Product)
//{
//    try
//    {
//        if (id != Product.ProductId)
//            return BadRequest("Product ID mismatch");
//        var ProductToUpdate = await ProductRepository.GetProduct(id);
//        if (ProductToUpdate == null)
//            return NotFound($"Product with Id= {id} not found");
//        return await ProductRepository.UpdateProduct(Product);

//    }
//    catch (Exception)
//    {
//        return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");

//    }
//}

//[HttpGet("{search}")]
//public async Task<ActionResult<IEnumerable<Product>>> Search(string name, string email)
//{
//    try
//    {
//        var result = await ProductRepository.Search(name, email);
//        if (result.Any())
//        {
//            return Ok(result);
//        }
//        return NotFound();
//    }
//    catch (Exception)
//    {
//        return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
//    }
//}