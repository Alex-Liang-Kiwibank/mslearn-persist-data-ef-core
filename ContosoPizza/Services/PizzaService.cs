using ContosoPizza.Data;
using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Services;

public class PizzaService
{
    private readonly PizzaContext _context;

    public PizzaService(PizzaContext context)
    {
        _context = context;
    }

    public IEnumerable<Pizza> GetAll()
    {
        return _context.Pizzas.AsNoTracking().ToList();
    }

    public Pizza? GetById(int id)
    {
        return _context.Pizzas.Include(e => e.Toppings)
            .Include(e => e.Sauce)
            .Where(e => e.Id == id).AsNoTracking()
            .SingleOrDefault();
    }

    public Pizza? Create(Pizza newPizza)
    {
        _context.Pizzas.Add(newPizza);
        _context.SaveChanges();
        return newPizza;
    }

    public void AddTopping(int PizzaId, int ToppingId)
    {
        var pizza = GetById(PizzaId);
        var topping = _context.Toppings.Find(ToppingId);

        if (pizza is null || topping is null)
        {
            throw new Exception("Invalid Pizza or Topping");
        }

        if (pizza.Toppings is null)
        {
            pizza.Toppings = new List<Topping>();
        }

        pizza.Toppings.Add(topping);
        _context.SaveChanges();
    }

    public void UpdateSauce(int PizzaId, int SauceId)
    {
        var pizza = GetById(PizzaId);
        var sauce = _context.Sauces.Find(SauceId);

        if (pizza is null || sauce is null)
        {
            throw new Exception("Invalid Pizza or Sauce");
        }

        pizza.Sauce = sauce;
        _context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        var pizza = GetById(id);

        if (pizza is null)
        {
            throw new Exception("Invalid Pizza");
        }

        _context.Pizzas.Remove(pizza);
        _context.SaveChanges();
    }
}