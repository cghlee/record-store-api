﻿using Microsoft.AspNetCore.Mvc;
using RecordStoreAPI.Classes;
using RecordStoreAPI.Services;

namespace RecordStoreAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AlbumsController : ControllerBase
{
    private readonly IAlbumsService _albumsService;
    public AlbumsController(IAlbumsService albumsService)
    {
        _albumsService = albumsService;
    }

    [HttpGet]
    public IActionResult GetAllAlbums()
    {
        List<Album> allAlbums = _albumsService.GetAllAlbums();
        return Ok(allAlbums);
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetAlbumById(int id)
    {
        Album? foundAlbum = _albumsService.FindAlbumById(id);

        if (foundAlbum == null)
            return BadRequest($"No album exists with an ID of {id}.");

        return Ok(foundAlbum);
    }

    [HttpPost]
    public IActionResult PostNewAlbum(Album newAlbum)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Album albumWithIdAdded = _albumsService.AddNewAlbum(newAlbum);
        return Ok(albumWithIdAdded);
    }
}
