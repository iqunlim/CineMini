using CineMini.Models;

namespace CineMini.Services.Interfaces;

public interface IMoviesDataProvider
{
    List<Movie> GetData();
    Movie GetMovie(int id);
    List<Movie> GetByGenre(string genre);
}
