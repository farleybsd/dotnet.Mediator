namespace AspNet_MediatR1.Domain.Command
{
    public class ProdutoUpdateCommand : ProdutoCreateCommand
    {
        public int Id { get; set; }
    }
}
