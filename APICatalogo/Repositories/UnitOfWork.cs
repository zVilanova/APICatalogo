using APICatalogo.Context;
using APICatalogo.Repositories.Interfaces;

namespace APICatalogo.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IProdutoRepository? _produtoRepo;
    private ICategoriaRepository? _categoriaRepo;
    public AppDbContext _context;
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IProdutoRepository ProdutoRepository
    {
        get
        {
            //Verifica se já existe uma instância de ProdutoRepository, caso não tenha cria uma passando a instância do contexto
            //Só cria um repositório se necessário
            return _produtoRepo = _produtoRepo ?? new ProdutoRepository(_context);
        }
    }
    public ICategoriaRepository CategoriaRepository
    {
        get
        {
            //Verifica se já existe uma instância de ProdutoRepository, caso não tenha cria uma passando a instância do contexto
            //Só cria um repositório se necessário
            return _categoriaRepo = _categoriaRepo ?? new CategoriaRepository(_context);
        }
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync(); //Confirma todas as alterações presentes
    }
    public void Dispose()
    {
        _context.Dispose();
    }
}
