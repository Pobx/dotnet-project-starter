using Domain.DTO.Response;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExampleController : ControllerBase
{

    private readonly IUnitOfWork _unitOfWork;

    public ExampleController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = new ResponseEntity<IEnumerable<Example>>();
        response.Entity = await _unitOfWork.Examples.GetAllAsync();

        return Ok(response);
    }

    [HttpGet()]
    public async Task<IActionResult> GetById(long id)
    {
        var response = new ResponseEntity<Example>();
        response.Entity = await _unitOfWork.Examples.GetByIdAsync(id);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Example entity)
    {
        var response = new ResponseEntity<Example>();
        _unitOfWork.Examples.Add(entity);

        await _unitOfWork.CompleteAsync();

        response.Entity = entity;

        var actionName = nameof(GetById);
        var routeValues = new { id = entity.Id };
        return CreatedAtAction(actionName, routeValues, response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Example entity)
    {
        var response = new ResponseEntity<Example>();
        _unitOfWork.Examples.Update(entity);

        await _unitOfWork.CompleteAsync();

        response.Entity = entity;

        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveAsync([FromQuery] long id)
    {
        _unitOfWork.Examples.Remove(id);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}

