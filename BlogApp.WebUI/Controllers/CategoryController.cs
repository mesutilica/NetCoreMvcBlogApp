using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.WebUI.Controllers
{
   public class CategoryController : Controller
   {
      private ICategoryRepository repository;
      public CategoryController(ICategoryRepository _repo)
      {
         repository = _repo;
      }
      public IActionResult Index()
      {
         return View();
      }
      public IActionResult List()
      {
         return View(repository.GetAll());
      }
      public IActionResult Create()
      {
         return View();
      }
      [HttpPost]
      public IActionResult Create(Category category)
      {
         if (ModelState.IsValid)
         {
            repository.AddCategory(category);

            return RedirectToAction("List");
         }
         return View(category);
      }
      public IActionResult Edit(int id)
      {
         return View(repository.GetById(id));
      }
      [HttpPost]
      public IActionResult Edit(Category category)
      {
         if (ModelState.IsValid)
         {
            repository.UpdateCategory(category);
            TempData["mesaj"] = $"{category.Name} kategorisi güncellendi";
            return RedirectToAction("List");
         }
         return View(category);
      }
      public IActionResult Delete(int id)
      {
         return View(repository.GetById(id));
      }
      [HttpPost]
      public IActionResult Delete(Category category)
      {
         if (ModelState.IsValid)
         {
            repository.DeleteCategory(category.CategoryId);

            return RedirectToAction("List");
         }
         return View(category);
      }
   }
}