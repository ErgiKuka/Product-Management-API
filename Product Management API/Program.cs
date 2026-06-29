
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Product_Management_API.Data;
using Product_Management_API.Middleware;
using Product_Management_API.Repositories.CategoryRepo;
using Product_Management_API.Repositories.ProductRepo;
using Product_Management_API.Services.CategoryServ;
using Product_Management_API.Services.ProductServ;
using Product_Management_API.UOW;

namespace Product_Management_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();
            //builder.Services.AddValidatorsFromAssemblyContaining<ProductUpdateValidator>();
            //builder.Services.AddValidatorsFromAssemblyContaining<CategoryCreateValidator>();
            //builder.Services.AddValidatorsFromAssemblyContaining<CategoryUpdateValidator>();
            builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(Program).Assembly));

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Management API");
                });
            }

            app.UseHttpsRedirection();


            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
