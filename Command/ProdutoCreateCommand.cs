using MediatR;
namespace Mediador.Command
{
    public class ProdutoCreateCommand : IRequest<string>
    {
        public string Nome { get; private set; }
        public decimal Preco { get; private set; }
    }
}

/*
 *Todas as classes implementam IRequest<T> onde especificamos o tipo de dados que será retornado
 *quando o comando for processado, e, também, através da qual vinculamos os comandos com as classes Command Handlers.
 *É assim que a  MediatR sabe qual objeto deve ser invocado quando um request for gerado.
 */


/*
 * Assim para cada Command vamos criar um Command Handler, embora podemos implementar um único objeto Command Handler
 * para tratar todos os Commands criados na aplicação.
 */