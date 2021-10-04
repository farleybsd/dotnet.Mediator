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
    public class ProdutoCreateCommandHandler : IRequestHandler<ProdutoCreateCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Produto> _repository;

        public ProdutoCreateCommandHandler(IMediator mediator, IRepository<Produto> repository)
        {
            this._mediator = mediator;
            this._repository = repository;
        }
        public async Task<string> Handle(ProdutoCreateCommand request, CancellationToken cancellationToken)
        {
            var produto = new Produto {Nome = request.Nome,Preco = request.Preco };

            try
            {
                await _repository.Add(produto);
                await _mediator.Publish(new ProdutoCreateNotification { Id = produto.Id, Nome = produto.Nome, Preco = produto.Preco });
                return await Task.FromResult("Produto criada com sucesso");
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ProdutoCreateNotification { Id = produto.Id, Nome = produto.Nome, Preco = produto.Preco });
                await _mediator.Publish(new ErroNotification { Erro = ex.Message, PilhaErro = ex.StackTrace });
                return await Task.FromResult("Ocorreu um erro no momento da criação");
            }
        }
    }
}
/*Para cada objeto Command devemos ter um objeto Command Handler.
 * Vamos então criar os Command Handlers na pasta Handlers:
 */