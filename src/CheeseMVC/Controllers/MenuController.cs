using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Data;
using CheeseMVC.Models;
using Microsoft.EntityFrameworkCore;
using CheeseMVC.ViewModels;
 

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class MenuController : Controller
    {
        private readonly CheeseDbContext context;
        public MenuController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Menu> CheeseMenus  = context.Menus.ToList();
            return View(CheeseMenus);
        }
        public IActionResult Add()
        {
            AddMenuViewModel addMenuViewModel = new AddMenuViewModel();
            return View(addMenuViewModel);
        }
        [HttpPost]
        public IActionResult Add(AddMenuViewModel addMenuViewModel)
        {

            if (ModelState.IsValid)
            {
                Menu newMenu = new Menu
                {
                    Name = addMenuViewModel.Name
                };

                context.Menus.Add(newMenu);
                context.SaveChanges();
                return Redirect("/Menu/ViewMenu/" + newMenu.ID);
            }
            return View(addMenuViewModel);
        }
        [Route("/Menu/ViewMenu/{id}")]
        [HttpGet]
        public IActionResult ViewMenu(int id)
        {
            List<CheeseMenu> items = context
                .CheeseMenu
                .Include(item => item.Cheese)
                .Where(cm => cm.CheeseID == id)
                .ToList();
            Menu menu = context.Menus.Single(m => m.ID == id);

            ViewMenuViewModel viewMenu = new ViewMenuViewModel
            {
                Menu = menu,
                Items = items
            };

            ViewBag.Title = menu.Name;


            return View(viewMenu);
        }
       
        [HttpGet]
        public IActionResult AddItem (int id)
        {
            Menu menu = context.Menus.SingleOrDefault(m => m.ID == id);
            List<Cheese> cheeses = context.Cheeses.ToList();
            return View(new AddMenuItemViewModel(menu, cheeses));
        }
        [HttpPost]
        public IActionResult AddItem (AddMenuItemViewModel addMenuItemViewModel)
        {
            if (ModelState.IsValid)
            {
                var cheeseID = addMenuItemViewModel.CheeseID;
                var menu = addMenuItemViewModel.MenuID;
                IList<CheeseMenu> existingItems = context.CheeseMenu
                    .Where(cm => cm.CheeseID == cheeseID)
                    .Where(cm => cm.MenuID == menu).ToList();
               
                if ( existingItems.Count == 0)
                {
                    CheeseMenu newMenu = new CheeseMenu
                    {
                        Cheese = context.Cheeses.SingleOrDefault(c => c.ID == cheeseID),
                        Menu = context.Menus.SingleOrDefault(m => m.ID == menu )
                    };
                   
                    context.CheeseMenu.Add(newMenu);
                    context.SaveChanges();

                return Redirect("/Menu/Index");
                }
                return View(addMenuItemViewModel);
                //Redirect("/Menu/ViewMenu/{menuID}");
            }
            return View(addMenuItemViewModel);
        }
        
    }
}
