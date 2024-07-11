using CinemaSuite.DataAccess.Repository.IRepository;
using CinemaSuite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CinemaSuite.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;   
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Movie> movieList = _unitOfWork.Movie.GetAll(includeProperties: "Category");
            return View(movieList);
        }
            public IActionResult Details(int movieId)
        {
            Movie movie = _unitOfWork.Movie.Get(u => u.Id == movieId, includeProperties: "Category");
            return View(movie);
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
