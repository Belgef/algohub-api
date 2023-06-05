using AlgoHub.API.ViewModels;
using AlgoHub.BLL.Interfaces;
using AlgoHub.BLL.Services;
using AlgoHub.DAL;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlgoHub.API.Controllers;

[ApiController]
[Route("[controller]/")]
public class PingController : ControllerBase
{
    [HttpGet("/ping")]
    public ActionResult<string?> Ping() => Ok("ping");
}
