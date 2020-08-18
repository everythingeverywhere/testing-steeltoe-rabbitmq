using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;
using Steeltoe.Messaging.RabbitMQ.Config;
using Steeltoe.Messaging.RabbitMQ.Extensions;

namespace WriteTo_RabbitMQ
{
    public class Startup
    {
        public const string RECEIVE_AND_CONVERT_QUEUE = "steeltoe_message_queue";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            // Add steeltoe rabbit services
            services.AddRabbitServices();
                
            // Add the steeltoe rabbit admin client... will be used to declare queues below
            services.AddRabbitAdmin();

            // Add a queue to the message container that the rabbit admin will discover and declare at startup
            services.AddRabbitQueue(new Queue(RECEIVE_AND_CONVERT_QUEUE));

            // Add the rabbit client template used for send and receiving messages
            services.AddRabbitTemplate();

        }
 
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }



       
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
