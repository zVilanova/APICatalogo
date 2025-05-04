using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories.Interfaces;
using X.PagedList;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<X.PagedList.IPagedList<Categoria>> GetCategoriasAsync(CategoriasParameters categoriasParams)
    {
        var categorias = await GetAllAsync();

        var categoriasOrdenadas = categorias.OrderBy(p => p.CategoriaId).AsQueryable();

        var resultado = await categoriasOrdenadas.ToPagedListAsync(categoriasParams.PageNumber, categoriasParams.PageSize);
        return resultado;
    }

    public async Task<X.PagedList.IPagedList<Categoria>> GetCategoriasFiltroNomeAsync(CategoriasFiltroNome categoriasFiltroParams)
    {
        var categorias = await GetAllAsync();

        if (!string.IsNullOrEmpty(categoriasFiltroParams.Nome))
        {
            categorias = categorias.Where(c => c.Nome.Contains(categoriasFiltroParams.Nome));
        }

        var categoriasFiltradas = await categorias.ToPagedListAsync(categoriasFiltroParams.PageNumber, categoriasFiltroParams.PageSize);
        return categoriasFiltradas;
    }
}
