using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.AspNetCore.Http;
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
        public IActionResult Index(int? id, string q)
        {
            var sorgu = _BlogRepository.GetAll().Where(i => i.isApproved);
            if (id != null)
            {
                sorgu = sorgu.Where(i => i.CategoryId == id);
            }
            if (!string.IsNullOrWhiteSpace(q))
            {
                sorgu = sorgu.Where(i => i.Title.Contains(q) || i.Description.Contains(q) || i.Body.Contains(q));
            }
            return View(sorgu.OrderByDescending(i => i.Date));
        }
        public IActionResult List()
        {
            return View(_BlogRepository.GetAll());
        }
        public IActionResult Details(int id)
        {
            return View(_BlogRepository.GetById(id));
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
        public async Task<IActionResult> Edit(Blog blog, IFormFile file)
        {
            blog.Date = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    blog.Image = file.FileName;
                }

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