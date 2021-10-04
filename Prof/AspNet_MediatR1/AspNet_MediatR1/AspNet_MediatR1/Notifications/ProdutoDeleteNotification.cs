using MediatR;

namespace AspNet_MediatR1.Notifications
{
    public class ProdutoDeleteNotification : INotification
    {
        public int Id { get; set; }
        public bool IsConcluido { get; set; }
    }
}
