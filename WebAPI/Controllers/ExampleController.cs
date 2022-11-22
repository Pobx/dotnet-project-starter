using Domain.DTO.Response;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<ActionResult<IEnumerable<Example>>> GetAllAsync()
    {
        var response = new ResponseEntity<IEnumerable<Example>>();
        response.Entity = await _unitOfWork.Examples.GetAllAsync();

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Example>> AddAsync([FromBody] Example entity)
    {
        var response = new ResponseEntity<Example>();
        await _unitOfWork.Examples.AddAsync(entity);
        await _unitOfWork.Complete();

        response.Entity = entity;

        return Created("", response);
    }
}

