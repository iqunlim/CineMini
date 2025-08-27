using CineMini.Models;

namespace CineMini.Services.Interfaces;

public interface IMoviesDataProvider
{
    List<Movie> GetAll();
    Movie GetMovie(int id);
    List<Movie> GetByGenre(string genre);
}
