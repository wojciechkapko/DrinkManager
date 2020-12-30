using AutoMapper;
using BLL.Contracts.Requests;
using BLL.Handlers;
using BLL.Interfaces;
using BLL.Services;
using Domain;
using DrinkManager.API.Extensions;
using DrinkManager.API.MapperProfiles;
using DrinkManager.API.Middlewares;
using DrinkManager.API.Resources;
using FluentValidation.AspNetCore;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Persistence.Repositories;
using ReportingModule.API.Services;
using Serilog;
using System;
using System.Text;
using System.Threading.Tasks;

namespace DrinkManager.API
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
            services.AddDbContext<DrinkAppContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            var builder = services.AddIdentityCore<AppUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;

            });
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identityBuilder.AddRoles<IdentityRole>();
            identityBuilder.AddEntityFrameworkStores<DrinkAppContext>();
            identityBuilder.AddSignInManager<SignInManager<AppUser>>();


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"])),
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
            });

            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<ILoginHandler, LoginHandler>();
            services.AddScoped<IRegisterHandler, RegisterHandler>();
            services.AddScoped<IUserAccessor, UserAccessor>();


            services.AddLocalization();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.SetDefaultCulture("en-GB");
                options.AddSupportedCultures("en-GB", "pl-PL");
                options.AddSupportedUICultures("en-GB", "pl-PL");
                options.FallBackToParentUICultures = true;

                options
                    .RequestCultureProviders
                    .Remove(typeof(AcceptLanguageHeaderRequestCultureProvider));
            });


            // .AddFacebook(facebookOptions =>
            //     {
            //         facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
            //         facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            //     })
            // .AddGoogle(options =>
            //     {
            //         IConfigurationSection googleAuthNSection =
            //             Configuration.GetSection("Authentication:Google");
            //
            //         options.ClientId = googleAuthNSection["ClientId"];
            //         options.ClientSecret = googleAuthNSection["ClientSecret"];
            //     });

            services.AddScoped<IDrinkRepository, DrinkRepository>();
            services.AddScoped<IDrinkSearchService, DrinkSearchService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IReportingModuleService, ReportingModuleService>();
            services.AddScoped<IFavouriteRepository, FavouriteRepository>();
            services.AddScoped<ISettingRepository, SettingRepository>();


            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddSingleton<BackgroundJobScheduler>();
            services.AddHostedService(provider => provider.GetService<BackgroundJobScheduler>());


            services.AddScoped<RequestLocalizationCookiesMiddleware>();

            services.AddControllers()
                .AddFluentValidation(cfg =>
                {
                    cfg.RegisterValidatorsFromAssemblyContaining<LoginRequest>();
                })
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(SharedResource));
                });

            //services.AddHttpContextAccessor();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
                });
            });

            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DrinkProfile());
                cfg.AddProfile(new IngredientProfile());
                cfg.AddProfile(new ReviewProfile());
            }).CreateMapper());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRequestLocalization();
            app.UseRequestLocalizationCookies();
            app.UseSerilogRequestLogging();

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToController("Index", "Fallback");
            });

            SeedData(serviceProvider).Wait();
        }


        private async Task SeedData(IServiceProvider services)
        {
            var context = services.GetRequiredService<DrinkAppContext>();

            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();


            await Seeder.SeedData(context, userManager, roleManager, Configuration);
        }
    }
}