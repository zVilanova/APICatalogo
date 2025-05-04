using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories.Interfaces;

public interface ICategoriaRepository : IRepository<Categoria>
{
    Task<X.PagedList.IPagedList<Categoria>> GetCategoriasAsync(CategoriasParameters categoriasParameters);
    Task<X.PagedList.IPagedList<Categoria>> GetCategoriasFiltroNomeAsync(CategoriasFiltroNome categoriasFiltroParams);
}
