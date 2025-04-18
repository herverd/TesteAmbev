using Ambev.DeveloperEvaluation.Application;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Messaging;
using MediatR;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Rebus.Transport.InMem;

namespace Ambev.DeveloperEvaluation.WebApi.Startups
{
    internal static class WebApiStartup
    {
        public static void RegisterWebApiServices(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            services.AddHttpContextAccessor();

            services.AddAutoMapper(typeof(Program).Assembly, typeof(ApplicationLayer).Assembly);

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(ApplicationLayer).Assembly,
                    typeof(Program).Assembly
                );
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddScoped<ICurrentUserAccessor, HttpLoggedUserAccessor>();
        }

        public static void AddRebusMessaging(this IServiceCollection services)
        {
            services.AddRebus((configure, provider) => configure
                .Logging(l => l.Console())
                .Transport(t => t.UseInMemoryTransport(new InMemNetwork(true), "carts_queue"))
                .Routing(r => r.TypeBased()
                    .Map<SaleCreatedEvent>("carts.sale.created")
                    .Map<SaleModifiedEvent>("carts.sale.modified")
                    .Map<SaleCancelledEvent>("carts.sale.cancelled")
                    .Map<ItemCancelledEvent>("carts.sale_item.cancelled"))
                , onCreated: async bus =>
                {
                    await bus.Subscribe<SaleCreatedEvent>();
                    await bus.Subscribe<SaleModifiedEvent>();
                    await bus.Subscribe<SaleCancelledEvent>();
                    await bus.Subscribe<ItemCancelledEvent>();
                }
            );
            services.AutoRegisterHandlersFromAssemblyOf<Program>();
            services.AddScoped<IEventNotification, RebusMessageProducer>();
        }
    }
}
