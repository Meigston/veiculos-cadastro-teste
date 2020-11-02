namespace MServices.Api
{
    using System;
    using System.IO;

    using AutoMapper;

    using FluentValidation;
    using FluentValidation.AspNetCore;

    using MediatR;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.PlatformAbstractions;
    using Microsoft.OpenApi.Models;

    using MServices.Api.Validators;
    using MServices.Domain.Dtos;
    using MServices.Domain.Handlers;
    using MServices.Domain.Infra.DB;
    using MServices.Domain.Mapper;
    using MServices.Domain.Models;
    using MServices.Domain.Repositorys;
    using MServices.Domain.Services.Interfaces;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddFluentValidation(
                    options =>
                        {
                            options.RegisterValidatorsFromAssemblyContaining<Startup>();
                        });

            services.AddControllers();
            services.AddSwaggerGen(
                options =>
                    {
                        options.SwaggerDoc("v1",
                            new OpenApiInfo
                            {
                                Title = "API FROTA DE VEÍCULOS - TESTE INLOG",
                                Version = "v1",
                                Description = "API referênte ao teste INLOG",
                                Contact = new OpenApiContact
                                {
                                    Name = "Meigston Ramos",
                                    Url = new Uri("https://www.linkedin.com/in/meigston/")
                                }
                            });

                        var pathApp = PlatformServices.Default.Application.ApplicationBasePath;
                        var appName = PlatformServices.Default.Application.ApplicationName;
                        var pathDocXml = Path.Combine(pathApp, $"{appName}.xml");
                        options.IncludeXmlComments(pathDocXml);
                    });

            services.AddDbContext<FrotaContext>(opt => opt.UseSqlServer(this.Configuration.GetValue<string>("DataBaseConfig:Connection")));
            this.AddRepositorys(services);

            services.AddMediatR(typeof(CadastroVeiculoHandler));
            services.AddAutoMapper(expression => expression.AddProfile(typeof(MapperProfile)));

            services.AddCors(c =>
                {
                    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                    {
                        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Frota de Veículos");
                    });
        }

        private void AddRepositorys(IServiceCollection services)
        {
            services.AddScoped<IRepository<Veiculo>, VeiculoRepository>();
        }
    }
}
