using Mediador.Command;
using Mediador.Entity;
using Mediador.Repository.Ioc;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Mediador.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Produto> _repository;
        public ProdutosController(IMediator mediator, IRepository<Produto> repository)
        {
            this._mediator = mediator;
            this._repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repository.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _repository.Get(id));
        }
        [HttpPost]
        public async Task<IActionResult> Post(ProdutoCreateCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Put(ProdutoUpdateCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var obj = new ProdutoDeleteCommand { Id = id };
            var result = await _mediator.Send(obj);
            return Ok(result);
        }
    }
}

/*
 * Injetamos no construtor uma instância do repositório e da interface IMediator pois precisamos 
 * enviar as requisições dos nosso objetos Command usando o método Send.
 * Aqui a interface IMediator faz o papel da classe mediadora que usa o método Send par chamar os comand handlers definidos.
 * Assim para tudo isso funcionar precisamos adicionar o MediatR como um serviço e registrar o serviço do
 * repositório no método ConfigureServices:
 */
