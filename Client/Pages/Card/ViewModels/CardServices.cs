namespace Client.Pages.Card.ViewModels;

public static class CardServices
{
    public static IServiceCollection AddCardServices(this IServiceCollection services)
    {
        services.AddScoped<ICardListVM, CardListVM>();
        return services;
    }
}
