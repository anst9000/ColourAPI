using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ColourAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ColourAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
			var server = Configuration["DBServer"] ?? "localhost";
			var port = Configuration["DBPort"] ?? "1433";
			var database = Configuration["Database"] ?? "Colours";
			var user = Configuration["DBUser"] ?? "SA";
			var password = Configuration["DBPassword"] ?? "Pa$$w0rd2019";

			services.AddDbContext<ColourContext>( options =>
				options.UseSqlServer($"Server={server},{port};Initial Catalog={database};User ID={user};Password={password}"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseMvc();

			PrepDB.PrepPopulation(app);
        }
    }
}
