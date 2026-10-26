using blog_backend.Common;
using blog_backend.Database;
using blog_backend.Modules.Blogs;
using blog_backend.Modules.Users;
using blog_backend.Repositories;
using blog_backend.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Keep module registration explicit so the composition root reads like NestJS imports.
builder.Services
    .AddSharedModule(builder.Configuration)
    .AddCommonModule(builder.Configuration)
    .AddDatabaseModule(builder.Configuration)
    .AddRepositoriesModule()
    .AddUsersModule()
    .AddBlogsModule();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Middleware
app.UseHttpsRedirection();
// Auth middleware
app.UseAuthentication();
app.UseAuthorization();
// Map controllerså
app.MapControllers();

app.Run();
