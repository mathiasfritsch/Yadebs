using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace Yadebs.IntegrationTests;

class WebApplicationSut : WebApplicationFactory<Program>
{

    protected override IHost CreateHost(IHostBuilder builder)
    {
        return base.CreateHost(builder);
    }
}