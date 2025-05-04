using System.Linq.Expressions;

namespace APICatalogo.Repositories.Interfaces;

public interface IRepository<T>
{
    //Cuidado para não violar o princípio ISP
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate); //Aceita como argumento uma expressão lambda (recebe um objeto T e retonra um bool com base no predicate)
    T Create(T entity);
    T Update(T entity);
    T Delete(T entity);
}
