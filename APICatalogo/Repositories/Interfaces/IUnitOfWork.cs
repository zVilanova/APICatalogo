﻿namespace APICatalogo.Repositories.Interfaces;

public interface IUnitOfWork //Atua como um repositório de repositórios
{
    //Realiza as operações através das instâncias
    IProdutoRepository ProdutoRepository { get; }
    ICategoriaRepository CategoriaRepository { get; }
    Task CommitAsync(); //Confirma todas as alterações presentes
}
