using Domain.DTO.Response;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("v1/[controller]/[action]")]
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

    [HttpGet]
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

    [HttpGet]
    public async Task<IActionResult> Hello()
    {
        var random = new Random();
        Thread.Sleep(2000);
        throw new Exception("Random Timeout");
        var value = random.Next(0, 2);
        if (value == 0)
        {
            //Thread.Sleep(10000);
            //throw new Exception("Random Timeout");
        }

        return Ok(value);
    }

   
}

