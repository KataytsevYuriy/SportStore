﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models;

namespace SportStore.Controllers
{
    public class CategoriesController : Controller
    {
        private ICategoryRepository repository;
        public CategoriesController(ICategoryRepository repo)
        {
            repository = repo;
        }
        public IActionResult Index()
        {
            return View(repository.Categories);
        }
        public IActionResult AddCategory (Category category)
        {
            repository.AddCategory(category);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult EditCategory(long id)
        {
            ViewBag.EditId = id;
            return View("Index", repository.Categories);
        }
        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            repository.UpdateCategory(category);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DeleteCategory (Category category)
        {
            repository.DeleteCategory(category);
            return RedirectToAction(nameof(Index));
        }
    }
}