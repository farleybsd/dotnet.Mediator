using MediatR;
namespace Mediador.Notifications
{
    public class ProdutoCreateNotification : INotification
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
    }
}

/*
 * As notificações são necessárias para informar que uma requisição foi concluída com sucesso visto que as requisições
   Command não retornam nenhuma informação.
 *Para isso podemos invocar o método Publish() no método Handler da classe Command Handler passando por parâmetro uma notificação,
 *assim, todos os Event Handlers que estiverem “ouvindo” as notificações do tipo do objeto “publicado” serão notificados e poderão
  processá-lo.
*O método Publish() é o responsável por emitir a notificação em todo sistema, e, ele vai procurar a classe que possui a herança 
 da interface INotificationHandler<T> e invocar o método Handler() para processar aquela notificação.
*Para implementar as notificações temos que definir os objetos notification.
*/