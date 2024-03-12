using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace Yadebs.IntegrationTests;

class WebApplicationSut : WebApplicationFactory<Program>
{

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(_ => { });
        return base.CreateHost(builder);
    }
}