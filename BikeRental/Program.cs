using BikeRental.Business.Interfaces;
using BikeRental.Business.Services;
using BikeRental.Filters.ActionFilters;
using BikeRental.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.AddExtensions();

#region --- Dependency Injection ---
builder.Services.AddSingleton<IJWTAuthService, JWTAuthService>();
builder.Services.AddScoped<IUserAuthService, UserAuthService>();
builder.Services.AddScoped<IUserManagementService, UserManagementService>();
builder.Services.AddScoped<IBikeService, BikeService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IBikeRatingService, BikeRatingService>();
#endregion

#region --- Action Filters ---
builder.Services.AddScoped<ManagerRoleAttribute>(); 
builder.Services.AddScoped<UserRoleAttribute>(); 
#endregion

var app = builder.Build();
if (app.Environment.IsDevelopment()) { app.UseDeveloperExceptionPage(); }
app.ConfigureExtensions();
app.Run();