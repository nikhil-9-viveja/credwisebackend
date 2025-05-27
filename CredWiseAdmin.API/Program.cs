using CredWiseAdmin.Repository;
using CredWiseAdmin.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using CredWiseAdmin.Service.Interfaces;
using CredWiseAdmin.Service;
//using CredWiseAdmin.Core.Data;

namespace CredWiseAdmin.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Add DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register Repositories
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<ILoanProductRepository, LoanProductRepository>();
            
            // Register FD Repositories
            builder.Services.AddScoped<IFDTypeRepository, FDTypeRepository>();
            builder.Services.AddScoped<IFDApplicationRepository, FDApplicationRepository>();
            builder.Services.AddScoped<IFDTransactionRepository, FDTransactionRepository>();

            // Register Services
            builder.Services.AddScoped<ILoanProductService, LoanProductService>();
            
            // Register FD Services
            builder.Services.AddScoped<IFDTypeService, FDTypeService>();
            builder.Services.AddScoped<IFDApplicationService, FDApplicationService>();
            builder.Services.AddScoped<IFDTransactionService, FDTransactionService>();

            // Register AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Register UserRepository
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            // CORS for Angular dev server
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularDev",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<CredWiseAdmin.API.Middleware.ExceptionMiddleware>();

            app.UseHttpsRedirection();

            // Enable CORS globally
            app.UseCors("AllowAngularDev");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
