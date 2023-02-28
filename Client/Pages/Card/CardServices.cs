using Client.Pages.Card.ViewModels;

namespace Client.Pages.Card;

public static class CardServices
{
    public static IServiceCollection AddCardServices(this IServiceCollection services)
    {
        services.AddScoped<ICardListVM, CardListVM>();
        services.AddScoped<ICardVM, CardVM>();
        return services;
    }
}
