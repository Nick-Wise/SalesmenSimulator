using Microsoft.Extensions.DependencyInjection;
using SalesmenSimulator.Services;

public static class ServiceRegistration
{
  public static IServiceCollection AddGameServices(this IServiceCollection services)
  {
    services.AddSingleton<IGameService, GameService>();
    return services;
  }
}