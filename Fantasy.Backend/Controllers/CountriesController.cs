using Fantasy.Backend.Data;
using Fantasy.Backend.UnitsOfWork.Interfaces;
using Fantasy.Backend.UnitWork.Interfaces;
using Fantasy.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fantasy.Backend.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class CountriesController : GenericController<Country>
{
    private readonly ICountriesUnitOfWork _countriesUnitOfWork;

    public CountriesController(IGenericUnitOfWork<Country> unitOfWork, ICountriesUnitOfWork countriesUnitOfWork) : base(unitOfWork)
    {
        _countriesUnitOfWork = countriesUnitOfWork;
    }

    [HttpGet]
    public override async Task<IActionResult> GetAsync()
    {
        var response = await _countriesUnitOfWork.GetAsync();
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest();
    }

    [HttpGet("{id}")]
    public override async Task<IActionResult> GetAsync(int id)
    {
        var response = await _countriesUnitOfWork.GetAsync(id);
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return NotFound(response.Message);
    }

    [HttpGet("combo")]
    public async Task<IActionResult> GetComboAsync()
    {
        return Ok(await _countriesUnitOfWork.GetComboAsync());
    }
}