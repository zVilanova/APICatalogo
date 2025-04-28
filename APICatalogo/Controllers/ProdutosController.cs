using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository; //Precisa da implementação para o método específico
        //private readonly IRepository<Produto> _repository;
        //IProdutoRepository já implementa o Repositório genérico, não sendo necessária sua implementação de forma redundante

        public ProdutosController(IProdutoRepository produtoRepository) //IRepository<Produto> repository 
        {
            _produtoRepository = produtoRepository;
           // _repository = repository; //Operações e lógica de acesso a dados estão concetradas no repositório
        }

        [HttpGet("produtos/{id}")]
        public ActionResult<IEnumerable<Produto>> GetProdutosCategoria(int id)
        {
           var produtos = _produtoRepository.GetProdutosPorCategoria(id);
           if (produtos is null)
                return NotFound();

           return Ok(produtos);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get() //ActionResult -> pode retornar uma lista de produtos ou todos os métodos de ActionResult
        {
            var produto = _produtoRepository.GetAll();
            if (produto is null)
            {
                return NotFound("Produto não encontrado...");
            }
            return Ok(produto);
        }

        [HttpGet("{id:int}", Name="ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _produtoRepository.Get(c => c.ProdutoId == id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado...");
            }
            return Ok(produto);
        }   

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null)
                return BadRequest(); //HTTP 400
           
          var novoProduto = _produtoRepository.Create(produto);

            return new CreatedAtRouteResult("ObterProduto", new { id = novoProduto.ProdutoId }, novoProduto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            var produtoAtualizado = _produtoRepository.Update(produto);

            return Ok(produtoAtualizado);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
           var produto = _produtoRepository.Get(c => c.ProdutoId == id);
           if (produto is null)
           {
               return NotFound("Produto não encontrado...");
           }

           var produtoDeletado = _produtoRepository.Delete(produto);
           return Ok(produtoDeletado);
        }   
    }
}
