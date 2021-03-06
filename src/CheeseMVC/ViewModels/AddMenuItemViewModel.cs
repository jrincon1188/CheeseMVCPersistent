﻿using CheeseMVC.Models;
using CheeseMVC.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CheeseMVC.ViewModels
{
    public class AddMenuItemViewModel
    {
        public int CheeseID { get; set; }
        public int MenuID { get; set; }
        public Menu Menu{ get; set; }
        public List <SelectListItem> Cheeses { get; set; }

        public AddMenuItemViewModel()
        {

        }
        public AddMenuItemViewModel(Menu menu, IEnumerable<Cheese> Cheese)
        {
            Cheeses = new List<SelectListItem>();
            foreach (var cheese in Cheese)
            {
                Cheeses.Add(new SelectListItem
                {
                    Value = cheese.ID.ToString(),
                    Text = cheese.Name
                });
                
            }
            Menu = menu;
        }  
    }
}
