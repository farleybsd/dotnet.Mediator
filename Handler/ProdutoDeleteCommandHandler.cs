using Mediador.Command;
using Mediador.Entity;
using Mediador.Notifications;
using Mediador.Repository.Ioc;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mediador.Handler
{
    public class ProdutoDeleteCommandHandler : IRequestHandler<ProdutoDeleteCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Produto> _repository;

        public ProdutoDeleteCommandHandler(IMediator mediator, IRepository<Produto> repository)
        {
            this._mediator = mediator;
            this._repository = repository;
        }
        public async Task<string> Handle(ProdutoDeleteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.Delete(request.Id);
                await _mediator.Publish(new ProdutoDeleteNotification
                { Id = request.Id, IsConcluido = true });
                return await Task.FromResult("Produto excluido com sucesso");
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ProdutoDeleteNotification
                {
                    Id = request.Id,
                    IsConcluido = false
                });
                await _mediator.Publish(new ErroNotification
                {
                    Erro = ex.Message,
                    PilhaErro = ex.StackTrace
                });
                return await Task.FromResult("Ocorreu um erro no momento da exclusão");
            }
        }
    }
}
/*Para cada objeto Command devemos ter um objeto Command Handler.
 * Vamos então criar os Command Handlers na pasta Handlers:
 */

/*
 * Os command handlers implementam a interface IRequestHandler, onde é especificada uma classe Command e o tipo de retorno.
 * Quando esta classe Command gerar uma solicitação, o MediatR irá invocar o command handler, chamando o método Handler.
 * É no método Handler onde são definidas instruções que devem ser realizadas para aplicar a solicitação definida pelo command.
 * Após a solicitação ser atendida, podemos usar o método Publish() para emitir uma notificação para todo sistema. 
 * Aqui o MediatR vai procurar pela classe com a implementação da interface INotificationHandler<notificacao> e invocar o 
 * método Handler() para processar aquela notificação que implementamos.
 */