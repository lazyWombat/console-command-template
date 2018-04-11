using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.ConsoleApplication1.Hello
{
    public class HelloWorldOptions : IOptionsConfigurer
    {
        public int DefaultDelayInSec { get; set; }

        void IOptionsConfigurer.Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<HelloWorldOptions>(configuration.GetSection("HelloWorld"));
        }
    }
}