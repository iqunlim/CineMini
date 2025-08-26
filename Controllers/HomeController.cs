using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CineMini.Models;
using CineMini.Services.Interfaces;

namespace CineMini.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    
    private IMoviesDataProvider MovieDataProvider { get; }

    // ~~~~~~ DEPENDENCY INJECTION ~~~~~~
    public HomeController(ILogger<HomeController> logger, IMoviesDataProvider moviesDataProvider)
    {
        // Dependency: Injected.
        // Since this is a singleton, every request can use this data
        // That way we do not have to create a whole object every single request!
        MovieDataProvider = moviesDataProvider;
        _logger = logger;

    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("movies")]
    public IActionResult Movies(string? genre)
    {
        ViewData["Title"] = "CineMini Movie Listings";
        ViewData["CurrentDate"] = DateTime.Now.ToShortDateString();
        
        var movies = genre != null
            ? MovieDataProvider.GetByGenre(genre)
            : MovieDataProvider.GetData();
        
        ViewBag.MovieCount = movies.Count;
        return View(movies);
    }

    [HttpGet("details")]
    public IActionResult Details(int id)
    {
        if (id <= 0) return NotFound();
        return View(MovieDataProvider.GetMovie(id));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
}
