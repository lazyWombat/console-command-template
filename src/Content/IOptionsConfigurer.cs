using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.ConsoleApplication1
{
    public interface IOptionsConfigurer
    {
        void Configure(IServiceCollection services, IConfiguration configuration);
    }
}
