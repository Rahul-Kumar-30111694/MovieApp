namespace MovieApp.Models
{
    public class PagedMoviesViewModel
    {
        public List<MoviesInDB> Movies { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }

}