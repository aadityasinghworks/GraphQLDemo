using GraphQL.Demo.Api.Schema.Mutations;
using GraphQL.Demo.Api.Schema.Queries;
using GraphQL.Demo.Api.Schema.Subscriptions;
using GraphQL.Demo.Api.Services;
using GraphQL.Demo.Api.Services.Courses;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>()
                .AddInMemorySubscriptions();

var connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddPooledDbContextFactory<SchoolDbContext>(options => options.UseSqlite(connectionString));

// Register services
builder.Services.AddScoped<CoursesRepository>();

var app = builder.Build();

// Run migrations before starting
using (var scope = app.Services.CreateScope())
{
    var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<SchoolDbContext>>();
    using var context = factory.CreateDbContext();
    context.Database.Migrate();
}

app.MapGet("/", () => "Hello World!");
app.UseWebSockets();
app.MapGraphQL("/graphql"); // explicitly map endpoint

app.Run();
