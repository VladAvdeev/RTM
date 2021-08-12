using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RTM.Server.Helpers;
using RTM.Server.Hubs;
using RTM.Server.Repositories;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace RTM.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            DataCache.Init();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region old 
            //    services.AddAuthentication(options =>
            //    {
            //        // Identity made Cookie authentication the default.
            //        // However, we want JWT Bearer Auth to be the default.
            //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    })
            //    .AddJwtBearer(options =>
            //{
            //    //options.Authority = /* TODO: Insert Authority URL here */;
            //    options.Events = new JwtBearerEvents
            //    {
            //        OnMessageReceived = context =>
            //        {
            //            var accessToken = context.Request.Query["access_token"];

            //            // If the request is for our hub...
            //            var path = context.HttpContext.Request.Path;
            //            if (!string.IsNullOrEmpty(accessToken) &&
            //                (path.StartsWithSegments("/notificationHub")))
            //            {
            //                // Read the token out of the query string
            //                context.Token = accessToken;
            //            }
            //            return Task.CompletedTask;
            //        }
            //    };
            //});
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //       .AddJwtBearer(options =>
            //       {
            //           options.Authority = "api/auth/token";
            //           options.RequireHttpsMetadata = false;
            //           options.TokenValidationParameters = new TokenValidationParameters
            //           {
            //                // укзывает, будет ли валидироваться издатель при валидации токена
            //                ValidateIssuer = true,
            //                // строка, представляющая издателя
            //                ValidIssuer = AuthOptions.ISSUER,

            //                // будет ли валидироваться потребитель токена
            //                ValidateAudience = true,
            //                // установка потребителя токена
            //                ValidAudience = AuthOptions.AUDIENCE,
            //                // будет ли валидироваться время существования
            //                ValidateLifetime = true,

            //                // установка ключа безопасности
            //                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            //                // валидация ключа безопасности
            //                ValidateIssuerSigningKey = true,
            //           };
            //           options.Events = new JwtBearerEvents
            //           {
            //               OnMessageReceived = context =>
            //               {
            //                   var accessToken = context.Request.Query["access_token"];

            //                   // если запрос направлен хабу
            //                   var path = context.HttpContext.Request.Path;
            //                   if (!string.IsNullOrEmpty(accessToken) &&
            //                       (path.StartsWithSegments("/notificationHub")))
            //                   {
            //                       // получаем токен из строки запроса
            //                       context.Token = accessToken;
            //                   }
            //                   return Task.CompletedTask;
            //               }
            //           };
            //       });
            #endregion

            services.AddAuthentication();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RTM API", Version = "v1" });
                c.AddSecurityDefinition("JWT Token", new OpenApiSecurityScheme
                {
                    Description = "JWT Token",
                    Type = SecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "bearer"
                });
                //c.OperationFilter<AuthFilter>();
                //c.OperationFilter<FileUploadFilter>();
            });
            services.AddSignalR();
            services.AddControllers();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();



            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chatHub");
                endpoints.MapHub<NotificationHub>("/notificationHub");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DisplayRequestDuration();
                c.DocExpansion(DocExpansion.None);
                c.EnableFilter();
                c.SwaggerEndpoint("v1/swagger.json", "RTM API");
            });
        }
    }
}
