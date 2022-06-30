using NetCoreMultiLanguageMiddleware.Middlewares;
using NetCoreMultiLanguageMiddleware.Services;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddViewLocalization();

// her bir request için yeni yeni instance oluþtur.
//builder.Services.AddTransient<ExceptionMiddleware>();

// session, handler, validation da ben transient service
builder.Services.AddTransient<TransientService>(); 

// veri tabaný iþlemleri ayaparken yada bir uzak sunucuya istek atarken performans amaçlý tercih ediyor.repositorylerin hepsi xmlwriter,excelwriter service scope service tanýmlýyorum
builder.Services.AddScoped<ScopeService>();


// uygulama genelinde konfigürasyon ayarlarý sms, mail atma servislerini instancelarý, bir uygulamayý çalýþtýrma sonlandýrma gibi iþlemler.
builder.Services.AddSingleton<SingletonService>();


builder.Services.AddLocalization(opt =>
{
  // Default buraya bakar
  opt.ResourcesPath = "Resources";
});


builder.Services.Configure<RequestLocalizationOptions>(opt =>
{

  //opt.SetDefaultCulture("fr");
  
  CultureInfo[] cultures = new CultureInfo[] { 
    new("en"),
    new("tr"),
    new("fr"),
  };

  opt.SupportedCultures = cultures; // uygulama bu diller ile çalýþsýn backend için
  opt.SupportedUICultures = cultures; // frontend html için
  opt.ApplyCurrentCultureToResponseHeaders = true; // Accept-Language request header ile dil deðiþimi yapma özelliði için açtýk.

  //opt.DefaultRequestCulture = new("en"); // uygulama ilk türkçe açýlsýn
  //opt.SetDefaultCulture("en");

});

var app = builder.Build();


// middleware ile çoklu dil desteðini request response sürecine ekledik.
app.UseRequestLocalization();

/// <summary>
/// buradan sonra artýk middleware tanýmlamasý yapabiliriz.
/// </summary>

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

// tüm middlewarelerimiz neredeyse use middleware olacak.


//app.UseMiddleware<ExceptionMiddleware>();

//app.Use((async (context, next) =>
//{

//  // HttpContext üzerinden cookie,session,authentication,user,path,request,response gibi nesnelere ulaþabiliriz.

//  if(context.Request.Path == "/" && context.Request.Method == HttpMethod.Get.ToString())
//  {
//    await context.Response.WriteAsync("Merhaba");
//    // next olmadýðýnda burada süreci kes alttaki kodlarý çalýþtýrma demek
//  }
//  else
//  {
//    // diðer middleware ile sürece devam
//    await next();
//  }

//}));

//app.Map("/Home/Privacy", async app =>
// {
//   app.Run(async context =>
//   {
//      context.Response.Headers.Add("x-client", "asdsad");
//   });
// });

//app.MapWhen(context => context.Request.Query.ContainsKey("name"), app =>
//{
//  app.Use(async (context, next) =>
//  {
//    context.Response.Cookies.Append("my-cookie", "cookie");
//    await next();
//  });
//});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
