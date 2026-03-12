using Assessment15.DTOs;
using Assessment15.Services;
using Microsoft.AspNetCore.Mvc;

namespace Assessment15.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _service;
    public ProductsController(ProductService service) => _service = service;

    // ✅ Response caching enabled for this endpoint
    // Cache for 30 seconds
    [HttpGet]
    [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var (items, total) = await _service.GetPagedAsync(page, pageSize);

        return Ok(new
        {
            page,
            pageSize,
            total,
            totalPages = (int)Math.Ceiling(total / (double)pageSize),
            items
        });
    }

    [HttpGet("{id:int}")]
    [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any)]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound(new { error = "Not Found", message = "Product not found" });
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name)) return BadRequest(new { error = "Bad Request", message = "Name is required" });
        if (dto.Price < 0) return BadRequest(new { error = "Bad Request", message = "Price must be >= 0" });

        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name)) return BadRequest(new { error = "Bad Request", message = "Name is required" });
        if (dto.Price < 0) return BadRequest(new { error = "Bad Request", message = "Price must be >= 0" });

        var ok = await _service.UpdateAsync(id, dto);
        if (!ok) return NotFound(new { error = "Not Found", message = "Product not found" });

        return NoContent(); // ✅ proper status code
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _service.DeleteAsync(id);
        if (!ok) return NotFound(new { error = "Not Found", message = "Product not found" });

        return NoContent();
    }
}