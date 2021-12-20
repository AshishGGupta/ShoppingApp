namespace ShoppingApp.DataAccess.IDataAccess
{
    public interface IDbFacade
    {
        ICartDbService CartDbService { get; }
        IProductDbServices ProductDbServices { get; }
        IOrderAndPaymentDBServices OrderDBServices { get; }
        IUserDbServices UserDBServices { get; }
    }    
}
