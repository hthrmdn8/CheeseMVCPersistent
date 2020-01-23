﻿using System.Collections.Generic;
using System.Linq;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        private readonly CheeseDbContext context;

        public CheeseController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        // /Cheese/Index -- Get request
        public IActionResult Index()
        {
            IList<Cheese> cheeses = context.Cheeses.Include(c => c.Category).ToList();

            return View(cheeses);
        }

        [Route("/Cheese")]
        [Route("/Cheese/Index")]
        [HttpPost]
        public IActionResult RemoveCheese(int[] cheeseIds)
        {
            foreach (int cheeseId in cheeseIds)
            {
                Cheese ch = context.Cheeses.Single(c => c.ID == cheeseId);
                context.Cheeses.Remove(ch);
            }

            context.SaveChanges();

            return Redirect("/Cheese/Index");
        }

        public IActionResult Add()
        {
            AddCheeseViewModel addCheeseViewModel =
                new AddCheeseViewModel(context.Categories.ToList());

            return View(addCheeseViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddCheeseViewModel addCheeseViewModel)
        {
            if (ModelState.IsValid)
            {
                CheeseCategory newCheeseCategory =
                    context.Categories.Single(c => c.ID == addCheeseViewModel.CategoryID);

                Cheese newCheese = new Cheese()
                {
                    Name = addCheeseViewModel.Name,
                    Description = addCheeseViewModel.Description,
                    //Rating = addCheeseViewModel.Rating,
                    Category = newCheeseCategory
                };

                context.Cheeses.Add(newCheese);
                context.SaveChanges();

                return Redirect("/Cheese");
            }

            return View(addCheeseViewModel);
        }

        /*GET /Cheese/Edit/ cheeseId;
        public IActionResult Edit(int cheeseId)
        {
            Cheese ch = context.Cheeses.Single(c => c.ID == cheeseId);

            AddEditCheeseViewModel vm = new AddEditCheeseViewModel(ch, context.Categories.ToList());

            return View(vm);
        }

        // POST /Cheese/Edit
        [HttpPost]
        public IActionResult Edit(AddEditCheeseViewModel vm)
        {
            // Validate the form data
            if (ModelState.IsValid)
            {
                Cheese ch = context.Cheeses.Single(c => c.ID == vm.CheeseId);
                ch.Name = vm.Name;
                ch.Description = vm.Description;
                ch.Category = context.Categories.Single(c => c.ID == vm.CategoryID);
                //ch.Rating = vm.Rating;

                context.SaveChanges();

                return Redirect("/Cheese");
            }

            return View(vm);
        }*/


    }
}