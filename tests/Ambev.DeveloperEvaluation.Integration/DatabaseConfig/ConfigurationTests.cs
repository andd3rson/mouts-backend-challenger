using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Integration.DatabaseConfig;

public  class ConfigurationTests
{
    public DefaultContext Context { get; }
    public ISaleRepository SaleRepository { get; }

    public ConfigurationTests(string dbName)
    {
        var options = new DbContextOptionsBuilder<DefaultContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        Context = new DefaultContext(options);
        SaleRepository = new SalesRepository(Context);

      
    }
}
