namespace ShoppingApp.Models.MediatorClass
{
    using MediatR;

    public class LogoutUser: IRequest<bool>
    {
        /// <summary>
        /// UserName 
        /// </summary>
        public string UserToken { get; set; }

        /// <summary>
        /// SessionId
        /// </summary>
        public string SessionId { get; set; }
    }
}
