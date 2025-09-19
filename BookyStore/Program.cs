using BookyStore.DataAccess.Data;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Utility;
using Microsoft.EntityFrameworkCore.Internal;
using DataAccess.DbInitializer;
using Models;
using VNPay.Services;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();	
builder.Services.AddDbContext<AppDbContext>(option =>
	option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection"))
);
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(cfg =>
{
	cfg.Cookie.Name = "bookystore";
	cfg.IdleTimeout = new TimeSpan(24, 0, 0);
});
builder.Services.AddScoped<IVnPayService, VnPayService>();
builder.Services.AddOptions();
var mailSettings = builder.Configuration.GetSection("MailSettings");
builder.Services.Configure<MailSettings>(mailSettings);	
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddIdentity<ApplicationUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
		.AddEntityFrameworkStores<AppDbContext>()
		.AddDefaultTokenProviders();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});
builder.Services.AddAuthentication();
builder.Services.AddHangfire(configure => configure
		.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
		.UseSimpleAssemblyNameTypeSerializer()
		.UseRecommendedSerializerSettings()
		.UseSqlServerStorage(builder.Configuration.GetConnectionString("SqlServerConnection"),
		new Hangfire.SqlServer.SqlServerStorageOptions()
		{
			// custom 
		}));
builder.Services.AddHangfireServer();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseHangfireDashboard();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
InitializeDatabase();
app.MapRazorPages();
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
void InitializeDatabase()
{
	using (var scope = app.Services.CreateScope())
	{
		var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
		dbInitializer.InitializeAsync();
	}
}