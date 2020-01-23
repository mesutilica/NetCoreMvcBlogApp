using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogApp.WebUI.Controllers
{
   public class BlogController : Controller
   {
      private IBlogRepository _BlogRepository;
      private ICategoryRepository _CategoryRepository;
      public BlogController(IBlogRepository blogRepository, ICategoryRepository categoryRepository)
      {
         _BlogRepository = blogRepository;
         _CategoryRepository = categoryRepository;
      }
      public IActionResult Index()
      {
         return View();
      }
      public IActionResult List()
      {
         return View(_BlogRepository.GetAll());
      }
      public IActionResult Create()
      {
         ViewBag.CategoryId = new SelectList(_CategoryRepository.GetAll(), "CategoryId", "Name");
         return View();
      }
      [HttpPost]
      public IActionResult Create(Blog blog)
      {
         blog.Date = DateTime.Now;
         if (ModelState.IsValid)
         {
            _BlogRepository.Add(blog);
            return RedirectToAction("List");
         }
         return View(blog);
      }
      public IActionResult Edit(int id)
      {
         ViewBag.CategoryId = new SelectList(_CategoryRepository.GetAll(), "CategoryId", "Name");
         return View(_BlogRepository.GetById(id));
      }
      [HttpPost]
      public IActionResult Edit(Blog blog)
      {
         blog.Date = DateTime.Now;
         if (ModelState.IsValid)
         {
            _BlogRepository.Update(blog);
            return RedirectToAction("List");
         }
         return View(blog);
      }
      public IActionResult Delete(int id)
      {
         return View(_BlogRepository.GetById(id));
      }
      [HttpPost, ActionName("Delete")]
      public IActionResult BlogSil(int BlogId)
      {
         _BlogRepository.Delete(BlogId);
         return RedirectToAction("List");
      }
   }
}