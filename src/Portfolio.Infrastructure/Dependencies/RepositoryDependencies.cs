using Microsoft.Extensions.DependencyInjection;
using Portfolio.Domain.Categories;
using Portfolio.Domain.Papers;
using Portfolio.Domain.Posts;
using Portfolio.Domain.Subscribers;
using Portfolio.Domain.Users;
using Portfolio.Infrastructure.Database.Datamodel.Categories;
using Portfolio.Infrastructure.Database.Datamodel.Papers;
using Portfolio.Infrastructure.Database.Datamodel.Posts;
using Portfolio.Infrastructure.Database.Datamodel.Subscribers;
using Portfolio.Infrastructure.Database.Datamodel.Users;

namespace Portfolio.Infrastructure.Dependencies
{
    public static class RepositoryDependencies
    {
        public static void RepositoriesConfigure(this IServiceCollection service)
        {
            service.AddScoped<ICategoryRepository, CategoryRepository>();
            service.AddScoped<IPaperRepository, PaperRepository>();
            service.AddScoped<IPostRepository, PostRepository>();
            service.AddScoped<ISubscriberRepository, SubscriberRepository>();
            service.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
