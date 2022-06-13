using Microsoft.AspNetCore.Mvc;
using CoffeeStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStore.ViewComponents
{
    public class GenreNavigation : ViewComponent
    {
        private ICoffeeStoreRepository repository;
        public GenreNavigation(ICoffeeStoreRepository repo)
        {
            repository = repo;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedGenre = RouteData?.Values["genre"];
            return View(repository.Coffees
            .Select(x => x.Genre)
            .Distinct()
            .OrderBy(x => x));
        }
    }
}
