namespace ShoppingApp.DataAccess.DataAccess
{
    using ShoppingApp.DataAccess.IDataAccess;

    public class DbFacade : IDbFacade
    {
        public ICartDbService CartDbService { get; private set; }

        public IProductDbServices ProductDbServices { get; private set; }

        public IOrderAndPaymentDBServices OrderDBServices { get; private set; }

        public IUserDbServices UserDBServices { get; private set; }

        public DbFacade(ICartDbService cartDb, IProductDbServices productDb, IOrderAndPaymentDBServices orderDb, IUserDbServices userDb)
        {
            CartDbService = cartDb;
            ProductDbServices = productDb;
            OrderDBServices = orderDb;
            UserDBServices = userDb;
        }
    }
}
