namespace ShoppingApp.DataAccess.IDataAccess
{
    public interface IDbFacade
    {
        ICartDbService CartDbService { get; }
        IDBServices DBServices { get; }
        IOrderAndPaymentDBServices OrderDBServices { get; }
        IUserDbServices UserDBServices { get; }
    }    
}
