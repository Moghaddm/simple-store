using Mappers;
using Persistence;
using Microsoft.AspNetCore.Mvc;
using CRUD.Api.Domain;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;
    private readonly StoreContext _context;

    public ProductsController(StoreContext context, ILogger<ProductsController> logger) =>
        (_context, _logger) = (context, logger);

    [AllowAnonymous]
    [HttpGet(Name = "GetProducts")]
    public async Task<ActionResult<List<AddEditProductDTO>>> Get() =>
        Ok(_context.Products.Select(product => ToDtoMapper.ToProductDtoMap(product)).ToList());

    [HttpGet("{id}", Name = "GetProduct")]
    public async Task<ActionResult<AddEditProductDTO>> GetById([FromRoute] int id)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == id);
        if (product is not null)
            return Ok(ToDtoMapper.ToProductDtoMap(product));
        return NotFound("Product Does Not Exist!");
    }

    [HttpPost("[action]", Name = "InsertProduct")]
    public async Task<IActionResult> Insert([FromBody] AddEditProductDTO product)
    {
        if (product is not null)
        {
            _logger.LogInformation("Product {0} is Added To Database!", product!.Name);
            await _context.Products.AddAsync(ToDtoMapper.ToProductDtoMap(product));
            await _context.SaveChangesAsync();
            return Ok($"Product {product!.Name} is Added To Database!");
        }
        return BadRequest("Your Product Is Not Fully Sended !");
    }

    [HttpPut("[action]/{id}", Name = "UpdateProduct")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] AddEditProductDTO product
    )
    {
        if (!(_context.Products.FirstOrDefault(p => p.Id == id) is Product found))
            return NotFound($"Product Whit {id} ID Is Not Exist!");
        var newProduct = ToDtoMapper.ToProductDtoMap(product);
        found.Name = newProduct.Name;
        found.Description = newProduct.Description;
        found.Attachments = newProduct.Attachments;
        found.Comments = newProduct.Comments;
        found.Price = newProduct.Price;
        found.Rate = newProduct.Rate;
        await _context.SaveChangesAsync();
        _logger.LogInformation($"Product {product!.Name} is Up To Date In Database!");
        return Ok("Updated!");
    }

    [HttpPut("[action]/{id}", Name = "DeleteProduct")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (!(_context.Products.FirstOrDefault(p => p.Id == id) is Product found))
            return NotFound($"Product Whit {id} ID Is Not Exist!");
        _context.Products.Remove(found);
        await _context.SaveChangesAsync();
        _logger.LogInformation($"Product {found!.Name} is Up To Date In Database!");
        return Ok("Deleted!");
    }
}
