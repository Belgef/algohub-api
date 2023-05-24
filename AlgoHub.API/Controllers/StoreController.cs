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
public class StoreController : ControllerBase
{
    private readonly IStorageService _storageService;

    public StoreController(IStorageService storageService)
    {
        _storageService = storageService;
    }

    [HttpPost]
    [Authorize(Roles = "User")]
    public async Task<ActionResult<string?>> UploadImage([FromForm] ImageViewModel image)
    {

        string? result = await _storageService.SaveFile(image.Image);

        return result != null ? Ok(result) : BadRequest();
    }
}