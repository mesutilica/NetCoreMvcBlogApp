using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlogApp.WebUI.Models;
using BlogApp.Data.Abstract;

namespace BlogApp.WebUI.Controllers
{
   public class HomeController : Controller
   {
      private readonly ILogger<HomeController> _logger;
      private IBlogRepository blogRepository;
      public HomeController(ILogger<HomeController> logger, IBlogRepository repository)//
      {
         _logger = logger;
         blogRepository = repository;
      }

      public IActionResult Index()
      {
         return View(blogRepository.GetAll());
      }
      public IActionResult Privacy()
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
