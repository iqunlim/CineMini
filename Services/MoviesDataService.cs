using CineMini.Models;
using CineMini.Services.Interfaces;

namespace CineMini.Services;

// Utter overkill to make this interface
// But it does future-proof it in case we move to a DB

public class MoviesDataService : IMoviesDataProvider
{
    private readonly List<Movie> _data;

    public MoviesDataService()
    {
        try
        {
            var fileData = File.ReadAllText("Data/movies.json");
            _data = System.Text.Json.JsonSerializer.Deserialize<List<Movie>>(fileData) ?? [];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _data = [];
        }
        
    }
    
    public List<Movie> GetData()
    {
        return _data;
    }

    public Movie GetMovie(int id)
    {
        return _data[id];
    }

    public List<Movie> GetByGenre(string genre)
    {
        return _data
            .Where(movie => string.Equals(movie.Genre, genre, StringComparison.CurrentCultureIgnoreCase))
            .ToList();
    }
}