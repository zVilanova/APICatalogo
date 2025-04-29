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
        private readonly IUnitOfWork _unitOfWork;
        public ProdutosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("produtos/{id}")]
        public ActionResult<IEnumerable<Produto>> GetProdutosCategoria(int id)
        {
           var produtos = _unitOfWork.ProdutoRepository.GetProdutosPorCategoria(id);
           if (produtos is null)
                return NotFound();

           return Ok(produtos);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get() //ActionResult -> pode retornar uma lista de produtos ou todos os métodos de ActionResult
        {
            var produto = _unitOfWork.ProdutoRepository.GetAll();
            if (produto is null)
            {
                return NotFound("Produto não encontrado...");
            }
            return Ok(produto);
        }

        [HttpGet("{id:int}", Name="ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _unitOfWork.ProdutoRepository.Get(c => c.ProdutoId == id);
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
           
          var novoProduto = _unitOfWork.ProdutoRepository.Create(produto);
          _unitOfWork.Commit();

            return new CreatedAtRouteResult("ObterProduto", new { id = novoProduto.ProdutoId }, novoProduto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            var produtoAtualizado = _unitOfWork.ProdutoRepository.Update(produto);
            _unitOfWork.Commit();

            return Ok(produtoAtualizado);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
           var produto = _unitOfWork.ProdutoRepository.Get(c => c.ProdutoId == id);
           if (produto is null)
           {
               return NotFound("Produto não encontrado...");
           }

           var produtoDeletado = _unitOfWork.ProdutoRepository.Delete(produto);
            _unitOfWork.Commit();
           return Ok(produtoDeletado);
        }   
    }
}
