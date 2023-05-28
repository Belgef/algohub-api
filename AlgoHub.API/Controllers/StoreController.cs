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
public class StoreController : ControllerBase
{
    private readonly IStorageService _storageService;

    public StoreController(IStorageService storageService)
    {
        _storageService = storageService;
    }

    [HttpPost]
    [Authorize(Roles = "User,Administrator")]
    public async Task<ActionResult<string?>> UploadImage([FromForm] ImageViewModel image)
    {

        string? result = await _storageService.SaveFile(image.Image);

        return result != null ? Ok(result) : BadRequest();
    }
}
