using Fantasy.Backend.UnitsOfWork.Implementations;
using Fantasy.Backend.UnitsOfWork.Interfaces;
using Fantasy.Backend.UnitWork.Interfaces;
using Fantasy.Shared.Entities;
using Fantasy.Shared.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Fantasy.Backend.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class TeamsController : GenericController<Team>
{
    private readonly ITeamsUnitOfWork _teamsUnitOfWork;

    public TeamsController(IGenericUnitOfWork<Team> unitOfWork, ITeamsUnitOfWork teamsUnitOfWork) : base(unitOfWork)
    {
        _teamsUnitOfWork = teamsUnitOfWork;
    }

    [HttpGet]
    public override async Task<IActionResult> GetAsync()
    {
        var response = await _teamsUnitOfWork.GetAsync();
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest();
    }

    [HttpGet("{id}")]
    public override async Task<IActionResult> GetAsync(int id)
    {
        var response = await _teamsUnitOfWork.GetAsync(id);
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return NotFound(response.Message);
    }

    [HttpGet("combo/{countryId:int}")]
    public async Task<IActionResult> GetComboAsync(int countryId)
    {
        return Ok(await _teamsUnitOfWork.GetComboAsync(countryId));
    }

    /// Este prefijo "full" se hace para que swagger no se confunda con el metodo post del controllador generico
    [HttpPost("full")]
    public async Task<IActionResult> PostAsync(TeamDTO teamDTO)
    {
        var action = await _teamsUnitOfWork.AddAsync(teamDTO);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest(action.Message);
    }

    /// Este prefijo "full" se hace para que swagger no se confunda con el metodo put del controllador generico
    [HttpPut("full")]
    public async Task<IActionResult> PutAsync(TeamDTO teamDTO)
    {
        var action = await _teamsUnitOfWork.UpdateAsync(teamDTO);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest(action.Message);
    }
}