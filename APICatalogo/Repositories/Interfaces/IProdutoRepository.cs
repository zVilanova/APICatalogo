using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories.Interfaces;

public interface IProdutoRepository : IRepository<Produto>
{
    Task<X.PagedList.IPagedList<Produto>> GetProdutosAsync(ProdutosParameters produtosParameters);
    Task<X.PagedList.IPagedList<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltroParams);
    Task<IEnumerable<Produto>> GetProdutosPorCategoriaAsync(int id);
}
