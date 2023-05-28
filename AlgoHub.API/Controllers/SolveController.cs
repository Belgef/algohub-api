using AlgoHub.API.Models;
using AlgoHub.API.ViewModels;
using AlgoHub.BLL.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AlgoHub.API.Controllers;

[ApiController]
[Route("[controller]/")]
public class SolveController : ControllerBase
{
    private readonly ISolveService _solveService;
    private readonly IMapper _mapper;

    public SolveController(ISolveService solveService, IMapper mapper)
    {
        _solveService = solveService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<SolveViewModel[]>> Get(int problemId)
    {
        var solves = await _solveService.GetSolves(problemId);

        return solves != null ? Ok(_mapper.Map<SolveViewModel[]>(solves)) : BadRequest();
    }

    [HttpPost]
    [Authorize(Roles = "User,Administrator")]
    public async Task<ActionResult<string[]?>> VerifyAndAddSolve([FromForm] SolveCreateViewModel solve)
    {
        string? userId = User.FindFirstValue("Id");

        if (userId == null)
        {
            return Unauthorized();
        }

        SolveCreateModel model = _mapper.Map<SolveCreateModel>(solve);
        model.AuthorId = Guid.Parse(userId!);

        string[]? result = await _solveService.VerifyAndAddSolve(model);

        return result != null ? Ok(result) : BadRequest();
    }
}