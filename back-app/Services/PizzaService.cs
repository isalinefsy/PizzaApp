using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContosoPizza.Services
{
    public class PizzaService
    {
        private readonly PizzaContext _context;

        public PizzaService(PizzaContext context)
        {
            _context = context;
        }

        public List<Pizza> GetAll()
        {
            return _context.Pizzas.ToList();
        }

        public Pizza? Get(Guid id)
        {
            return _context.Pizzas.Find(id);
        }

        public void Add(Pizza pizza)
        {
            _context.Pizzas.Add(pizza);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var pizza = _context.Pizzas.Find(id);
            if (pizza == null)
                return;

            _context.Pizzas.Remove(pizza);
            _context.SaveChanges();
        }

        public void Update(Pizza pizza)
        {
            var existingPizza = _context.Pizzas.Find(pizza.Id);
            if (existingPizza == null)
                return;

            existingPizza.Name = pizza.Name;
            existingPizza.IsGlutenFree = pizza.IsGlutenFree;
            _context.SaveChanges();
        }
    }
}
