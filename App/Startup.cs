using App.Extensions;
using App.Models;
using AsbtCore.UtilsV2;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.AspNetCore;
using System.Collections.Generic;
using System.IO.Compression;
using ZNetCS.AspNetCore.Compression;
using ZNetCS.AspNetCore.Compression.Compressors;

namespace App
{
    public class Startup
    {
        public IConfiguration conf { get; }
        public Startup(IConfiguration configuration)=>            conf = configuration;
        
        public void ConfigureServices(IServiceCollection services)
        {
            int OdataResultCount = conf["SystemParams:OdataResultCount"].ToInt();
            var section = conf.GetSection("SystemParams");
            services.Configure<Vars>(section);

            services.AddCompression(options => { options.Compressors = new List<ICompressor> { new GZipCompressor(CompressionLevel.Fastest), new DeflateCompressor(CompressionLevel.Fastest), new BrotliCompressor(CompressionLevel.Fastest) }; });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                        builder =>
                        {
                            builder.AllowAnyOrigin()
                                   .AllowAnyHeader()
                                   .AllowAnyMethod();
                        });
            });

            services.Configure<RequestLoggingOptions>(o =>
            {
                o.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    diagnosticContext.Set("RemoteIpAddress", httpContext.Connection.RemoteIpAddress.MapToIPv4());
                };
            });

            services.AddMyDbContext(conf);

            services.AddMyService(conf);

            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
                         
            services.AddMemoryCache();
            services.AddMyAuthentication(conf);
            services.AddMySwagger();
            services.AddMyMetricServer();
            services.ApiMyVersion();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors("AllowAllHeaders");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSerilogRequestLogging();
            app.UseMySwagger(provider);
            app.UseMyMetricServer();

            app.UpdateMigrateDatabase();
        }
    }
}
