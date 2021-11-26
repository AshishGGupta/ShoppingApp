namespace ShoppingApp.DataAccess.DataAccess
{
    using ShoppingApp.DataAccess.IDataAccess;

    public class DbFacade : IDbFacade
    {
        public ICartDbService CartDbService { get; private set; }

        public IDBServices DBServices { get; private set; }

        public IOrderAndPaymentDBServices OrderDBServices { get; private set; }

        public IUserDbServices UserDBServices { get; private set; }

        public DbFacade(ICartDbService cartDb, IDBServices productDb, IOrderAndPaymentDBServices orderDb, IUserDbServices userDb)
        {
            CartDbService = cartDb;
            DBServices = productDb;
            OrderDBServices = orderDb;
            UserDBServices = userDb;
        }
    }
}
