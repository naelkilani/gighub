using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.Repositories
{
    public interface IGenresRepository
    {
        IEnumerable<Genre> GetGenres();
    }
}