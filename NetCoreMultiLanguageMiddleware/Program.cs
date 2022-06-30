using NetCoreMultiLanguageMiddleware.Middlewares;
using NetCoreMultiLanguageMiddleware.Services;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddViewLocalization();

// her bir request i�in yeni yeni instance olu�tur.
//builder.Services.AddTransient<ExceptionMiddleware>();

// session, handler, validation da ben transient service
builder.Services.AddTransient<TransientService>(); 

// veri taban� i�lemleri ayaparken yada bir uzak sunucuya istek atarken performans ama�l� tercih ediyor.repositorylerin hepsi xmlwriter,excelwriter service scope service tan�ml�yorum
builder.Services.AddScoped<ScopeService>();


// uygulama genelinde konfig�rasyon ayarlar� sms, mail atma servislerini instancelar�, bir uygulamay� �al��t�rma sonland�rma gibi i�lemler.
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

  opt.SupportedCultures = cultures; // uygulama bu diller ile �al��s�n backend i�in
  opt.SupportedUICultures = cultures; // frontend html i�in
  opt.ApplyCurrentCultureToResponseHeaders = true; // Accept-Language request header ile dil de�i�imi yapma �zelli�i i�in a�t�k.

  //opt.DefaultRequestCulture = new("en"); // uygulama ilk t�rk�e a��ls�n
  //opt.SetDefaultCulture("en");

});

var app = builder.Build();


// middleware ile �oklu dil deste�ini request response s�recine ekledik.
app.UseRequestLocalization();

/// <summary>
/// buradan sonra art�k middleware tan�mlamas� yapabiliriz.
/// </summary>

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

// t�m middlewarelerimiz neredeyse use middleware olacak.


//app.UseMiddleware<ExceptionMiddleware>();

//app.Use((async (context, next) =>
//{

//  // HttpContext �zerinden cookie,session,authentication,user,path,request,response gibi nesnelere ula�abiliriz.

//  if(context.Request.Path == "/" && context.Request.Method == HttpMethod.Get.ToString())
//  {
//    await context.Response.WriteAsync("Merhaba");
//    // next olmad���nda burada s�reci kes alttaki kodlar� �al��t�rma demek
//  }
//  else
//  {
//    // di�er middleware ile s�rece devam
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
