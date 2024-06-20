using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ContosoPizza.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzaController : ControllerBase
    {
        private readonly PizzaService _pizzaService;

        public PizzaController(PizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }

        [HttpGet]
        public ActionResult<List<Pizza>> GetAll() =>
            _pizzaService.GetAll();

        // GET by Id action
        [HttpGet("{id}")]
        public ActionResult<Pizza> Get(Guid id)
        {
            var pizza = _pizzaService.Get(id);

            if (pizza == null)
                return NotFound();

            return pizza;
        }

        // POST action
        [HttpPost]
        public IActionResult Create(Pizza pizza)
        {
            _pizzaService.Add(pizza);
            return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
        }

        // PUT action
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, Pizza pizza)
        {
            if (id != pizza.Id)
                return BadRequest();

            var existingPizza = _pizzaService.Get(id);
            if (existingPizza is null)
                return NotFound();

            _pizzaService.Update(pizza);
            return NoContent();
        }

        // DELETE action
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var existingPizza = _pizzaService.Get(id);
            if (existingPizza is null)
                return NotFound();

            _pizzaService.Delete(id);

            return NoContent();
        }
    }
}
