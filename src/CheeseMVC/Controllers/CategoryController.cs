using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CheeseDbContext context;

        //public string Name { get; private set; }

        public CategoryController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

            public IActionResult Index()
            {
            List<CheeseCategory> AllCat = context.Categories.ToList();

                 return View(AllCat);
            }
            public IActionResult Add()
            {
                 AddCategoryViewModel addCategoryViewModel = new AddCategoryViewModel();
                 return View(addCategoryViewModel);
            }
        [HttpPost]
        public IActionResult Add(AddCategoryViewModel addCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                CheeseCategory newCategory = new CheeseCategory
                {
                    Name = addCategoryViewModel.Name
                };

                context.Categories.Add(newCategory);
                context.SaveChanges();
                return Redirect("/Category");
            }
            return View();



        }
    }
    
}
