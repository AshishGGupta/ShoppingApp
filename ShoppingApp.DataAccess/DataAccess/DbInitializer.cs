using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShoppingApp.DataAccess.IDataAccess;
using System;
using System.Linq;

namespace ShoppingApp.DataAccess.DataAccess
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ShoppingDbContext _dbContext;
        private readonly ILogger<DbInitializer> _logger;

        public DbInitializer(ShoppingDbContext dbContext, ILogger<DbInitializer> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public void InitializeDB()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Count() > 0)
                {
                    _dbContext.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured while migrating the changes to the database. Error: " + ex.Message);
            }
        }
    }
}
