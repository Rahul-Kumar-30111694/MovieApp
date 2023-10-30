using MovieApp.Services;
using MovieApp.Database;
using MainProject.Login.Service;
using MainProject.SignUp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IDatabaseCollections, DatabaseCollections>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IHomePageService, HomePageService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ISignUpService, SignUpService>();
builder.Services.AddScoped<IForgotPasswordService, ForgotPasswordService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
