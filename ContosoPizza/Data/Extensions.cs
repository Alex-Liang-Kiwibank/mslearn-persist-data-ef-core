namespace ContosoPizza.Data;

public static class Extensions
{
    public static void CreateDbIfNotExists(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var pizzaContext = scope.ServiceProvider.GetRequiredService<PizzaContext>();
        pizzaContext.Database.EnsureCreated();
        DbInitializer.Initialize(pizzaContext);
    }
}