using System;
using System.Collections.Generic;
using CheeseMVC.Models;
using System.Linq;
namespace CheeseMVC.ViewModels
{
    public class ViewMenuViewModel
    {
        public Menu Menu { get; set; }

        public IList<CheeseMenu> Items { get; set; }

    }
}