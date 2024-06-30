//РАБОТАЕТ Основная
//using Microsoft.AspNetCore.Server.Kestrel.Core;
//using Microsoft.OpenApi.Models;
//using ParserZakupkiGovRu_with_ASP_VER_1._0.Services;
//using System.Net;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllers();

//// Add configuration settings
//builder.Configuration.AddJsonFile("appsettings.json", false, true);

//// Register services
//builder.Services.AddTransient<PageLoader>();
//builder.Services.AddTransient<PageParser>();

//// Add CORS policy
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll",
//        builder =>
//        {
//            builder.AllowAnyOrigin()
//                   .AllowAnyMethod()
//                   .AllowAnyHeader();
//        });
//});

//// Add Swagger
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Parser API", Version = "v1" });
//    c.MapType<int?>(() => new OpenApiSchema { Type = "integer", Format = "int32", Nullable = true });
//});

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//    app.UseSwagger();
//    app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Parser API V1");
//        c.RoutePrefix = string.Empty;
//    });
//}

//app.UseHttpsRedirection();

//// Use CORS policy
//app.UseCors("AllowAll");

//app.UseAuthorization();

//app.MapControllers();

//app.Run();







//РАБОТАЕТ Правильная через костыли
//using Microsoft.OpenApi.Models;
//using ParserZakupkiGovRu_with_ASP_VER_1._0.Services;
//using Microsoft.AspNetCore.Server.Kestrel.Core;
//using System.Net;

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers();

//builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

//builder.Services.AddTransient<PageLoader>();
//builder.Services.AddTransient<PageParser>();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll",
//        builder =>
//        {
//            builder.AllowAnyOrigin()
//                   .AllowAnyMethod()
//                   .AllowAnyHeader();
//        });
//});

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Parser API", Version = "v1" });
//    c.MapType<int?>(() => new OpenApiSchema { Type = "integer", Format = "int32", Nullable = true });
//});

//builder.WebHost.ConfigureKestrel(serverOptions =>
//{
//    serverOptions.Listen(IPAddress.Any, 5264, listenOptions =>
//    {
//        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
//    });
//    serverOptions.Listen(IPAddress.Any, 7062, listenOptions =>
//    {
//        listenOptions.UseHttps();
//    });
//});

//var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//    app.UseSwagger();
//    app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Parser API V1");
//        c.RoutePrefix = string.Empty;
//    });
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseCors("AllowAll");

//app.UseAuthorization();

//app.MapControllers();

//app.MapFallbackToFile("/index.html");

//app.Run();





using ParserZakupkiGovRu_with_ASP_VER_1._0.Interfaces;
using ParserZakupkiGovRu_with_ASP_VER_1._0.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddTransient<PageLoader>();
//builder.Services.AddTransient<PageParser>();

builder.Services.AddScoped<IPageLoader, PageLoader>();
builder.Services.AddScoped<IPageParser, PageParser>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger/index.html");
        return;
    }
    await next();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();