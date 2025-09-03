using CineMini.Models;
using CineMini.Services.Interfaces;

namespace CineMini.Services;

// Utter overkill to make this interface
// But it does future-proof it in case we move to a DB

public class MoviesDataService : IMoviesDataProvider
{
    private readonly List<Movie> _data;
    private readonly List<string> _genres;

    public MoviesDataService()
    {
        try
        {
            var fileData = File.ReadAllText("Data/movies.json");
            _data = System.Text.Json.JsonSerializer.Deserialize<List<Movie>>(fileData) ?? [];
            _genres = _data.Select(m => m.Genre).Distinct().ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _data = [];
        }
        
    }
    
    public List<Movie> GetAll()
    {
        return _data;
    }

    public Movie GetMovie(int id)
    {
        // Make sure its a valid check
        if (id <= 0) throw new ArgumentException("ID must be greater than zero");
        // ID in the dataset is one less than the passed in ID. We correct it here.
        return _data[id-1];
    }

    public List<Movie> GetByGenre(string genre)
    {
        return _data
            .Where(movie => string.Equals(movie.Genre, genre, StringComparison.CurrentCultureIgnoreCase))
            .ToList();
    }

    public List<string> GetGenres()
    {
        return _genres;
    }
}