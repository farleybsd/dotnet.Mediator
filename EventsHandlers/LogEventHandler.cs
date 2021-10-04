using Mediador.Notifications;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mediador.EventsHandlers
{
    public class LogEventHandler :
                            INotificationHandler<ProdutoCreateNotification>,
                            INotificationHandler<ProdutoUpdateNotification>,
                            INotificationHandler<ProdutoDeleteNotification>,
                            INotificationHandler<ErroNotification>
    {
        public Task Handle(ProdutoCreateNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"CRIACAO: '{notification.Id} " +
                    $"- {notification.Nome} - {notification.Preco}'");
            });
        }
        public Task Handle(ProdutoUpdateNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"ALTERACAO: '{notification.Id} - {notification.Nome} " +
                    $"- {notification.Preco} - {notification.IsConcluido}'");
            });
        }
        public Task Handle(ProdutoDeleteNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"EXCLUSAO: '{notification.Id} " +
                    $"- {notification.IsConcluido}'");
            });
        }

        public Task Handle(ErroNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"ERRO: '{notification.Erro} \n {notification.PilhaErro}'");
            });
        }
    }
}

/*Precisamos criar uma classe do tipo Notification Handler que deverá “escutar” 
 * todas as notificações, pois todas serão apenas registradas no console.
 */

/*Podemos definir tantas classes Notification Handlers quanto forem necessárias.
 * Caso uma notificação seja “ouvida” por mais de um Notification Handlers,
 * todos serão invocados quando a notificação for gerada.
 */