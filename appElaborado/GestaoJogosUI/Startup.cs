using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using Microsoft.AspNetCore.Authentication;
using Dominio.Servico;
using Repositorio.Repositorio;
using Repositorio.Context;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using GestaoJogosUI.Mapper;

namespace GestaoJogosUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<GestaoJogosUIContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("GestaoJogosUIContext")));

            services.AddSingleton(typeof(IRepositorioBase<>), typeof(RepositorioBase<>));
            services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddTransient<IAmigoRepositorio, AmigoRepositorio>();
            services.AddTransient<IJogoRepositorio, JogoRepositorio>();


            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            })
            .AddCookie(op =>
            {
                op.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                op.LoginPath = "/Login";
                op.LogoutPath = "/Login/Logout";
                op.Events.OnRedirectToLogin = async (context) =>
                {
                    await context.HttpContext.SignOutAsync();
                    context.Response.Redirect(context.RedirectUri);
                };
            });

            services.AddAutoMapper(x => x.AddProfile(new MapperGestao()));

            services.AddMvc();
        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();


            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
