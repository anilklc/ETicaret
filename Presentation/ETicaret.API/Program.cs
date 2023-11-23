using ETicaret.Application.Validators.Products;
using ETicaret.Infrastructure.Filters;
using ETicaret.Persistence;
using ETicaret.Infrastructure;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructurServices();
//burda controller yapsýna validationfilter ekledik  sistemin kendisine ait filter yapýsýný kapatýp
//tamamen kendimize ait bir filter yapýsýný entegre ettik
builder.Services.AddControllers(options=>options.Filters.Add<ValidationFilter>()).AddFluentValidation(configuration=>configuration
.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    .ConfigureApiBehaviorOptions(options=>options.SuppressModelStateInvalidFilter=true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Production ve Development alanlarýný ayýrdýk
var env = builder.Environment;
builder.Configuration.SetBasePath(env.ContentRootPath).AddJsonFile("appsettings.json",optional:false)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json",optional:true);

builder.Services.AddCors(options =>
{
    options.AddPolicy("Allow", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("Allow");

app.Run();
