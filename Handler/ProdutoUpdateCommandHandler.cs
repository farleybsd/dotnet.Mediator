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
    public class ProdutoUpdateCommandHandler : IRequestHandler<ProdutoUpdateCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Produto> _repository;

        public ProdutoUpdateCommandHandler(IMediator mediator, IRepository<Produto> repository)
        {
            this._mediator = mediator;
            this._repository = repository;
        }
        public async Task<string> Handle(ProdutoUpdateCommand request, CancellationToken cancellationToken)
        {
            var produto = new Produto
            {
                Id = request.Id,
                Nome = request.Nome,
                Preco = request.Preco
            };
            try
            {
                await _repository.Edit(produto);
                await _mediator.Publish(new ProdutoUpdateNotification
                { Id = produto.Id, Nome = produto.Nome, Preco = produto.Preco });
                return await Task.FromResult("Produto alterado com sucesso");
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ProdutoUpdateNotification
                {
                    Id = produto.Id,
                    Nome = produto.Nome,
                    Preco = produto.Preco
                });

                await _mediator.Publish(new ErroNotification
                {
                    Erro = ex.Message,
                    PilhaErro = ex.StackTrace
                });
                return await Task.FromResult("Ocorreu um erro no momento da alteração");
            }
        }
    }
}
/*Para cada objeto Command devemos ter um objeto Command Handler.
 * Vamos então criar os Command Handlers na pasta Handlers:
 */