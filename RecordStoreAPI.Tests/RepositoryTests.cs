﻿using Microsoft.EntityFrameworkCore;
using Moq;
using RecordStoreAPI.Classes;
using RecordStoreAPI.DbContexts;
using RecordStoreAPI.Repositories;

namespace RecordStoreAPI.Tests;

public class RepositoryTests
{
    AlbumsDbContext _testDbContext;
    AlbumsRepository albumsRepository;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AlbumsDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        _testDbContext = new AlbumsDbContext(options);

        albumsRepository = new AlbumsRepository(_testDbContext);
    }

    [TearDown]
    public void Teardown()
    {
        _testDbContext.Database.EnsureDeleted();
        _testDbContext.Dispose();
    }

    #region GetAllAlbums method tests
    [Test]
    public void GetAllAlbums_ReturnsListOfAlbumsType()
    {
        // Arrange
        var expectedAlbum = new Album { Id = 1, Name = "Album1", Artist = "Artist1", Composer = "Composer1", Genre = "Genre1", Year = 2001 };
        _testDbContext.Albums.Add(expectedAlbum);
        _testDbContext.SaveChanges();

        // Act
        var result = albumsRepository.GetAllAlbums();

        // Assert
        Assert.That(result, Is.TypeOf<List<Album>>());
    }

    [Test]
    public void GetAllAlbums_ReturnsRetrievedAlbums()
    {
        // Arrange
        var expectedAlbum = new Album { Id = 1, Name = "Album1", Artist = "Artist1", Composer = "Composer1", Genre = "Genre1", Year = 2001 };
        _testDbContext.Albums.Add(expectedAlbum);
        _testDbContext.SaveChanges();

        var expectedList = new List<Album>
        {
            expectedAlbum,
        };

        // Act
        var result = albumsRepository.GetAllAlbums();

        // Assert
        Assert.That(result, Is.EquivalentTo(expectedList));
    }
    #endregion

    #region AddNewAlbum method tests
    [Test]
    public void AddNewAlbum_ReturnsAlbumType()
    {
        // Arrange
        var inputAlbum = new Album { Name = "Album1", Artist = "Artist1", Composer = "Composer1", Genre = "Genre1", Year = 2001 };

        var expected = new Album
        {
            Id = 1,
            Name = inputAlbum.Name,
            Artist = inputAlbum.Artist,
            Composer = inputAlbum.Composer,
            Genre = inputAlbum.Genre,
            Year = inputAlbum.Year
        };

        // Act
        var result = albumsRepository.AddNewAlbum(inputAlbum);

        // Assert
        Assert.That(result, Is.TypeOf<Album>());
    }

    [Test]
    public void AddNewAlbum_ReturnsAlbumWithId()
    {
        // Arrange
        var inputAlbum = new Album { Name = "Album1", Artist = "Artist1", Composer = "Composer1", Genre = "Genre1", Year = 2001 };

        var expected = new Album
        {
            Id = 1,
            Name = inputAlbum.Name,
            Artist = inputAlbum.Artist,
            Composer = inputAlbum.Composer,
            Genre = inputAlbum.Genre,
            Year = inputAlbum.Year
        };

        // Act
        var result = albumsRepository.AddNewAlbum(inputAlbum);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(expected.Id));
            Assert.That(result.Name, Is.EqualTo(expected.Name));
            Assert.That(result.Artist, Is.EqualTo(expected.Artist));
            Assert.That(result.Composer, Is.EqualTo(expected.Composer));
            Assert.That(result.Genre, Is.EqualTo(expected.Genre));
            Assert.That(result.Year, Is.EqualTo(expected.Year));
        });
    }
    #endregion
}