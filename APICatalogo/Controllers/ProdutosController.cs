using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProdutosController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("produtos/{id}")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosCategoria(int id)
        {
           var produtos = await _unitOfWork.ProdutoRepository.GetProdutosPorCategoriaAsync(id);
           if (produtos is null)
                return NotFound();

           //var destino = _mapper.Map<Destino>(origem);
           var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

           return Ok(produtosDto);
        }


        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get([FromQuery] ProdutosParameters produtosParameters)
        {
            var produtos = await _unitOfWork.ProdutoRepository.GetProdutosAsync(produtosParameters);
            return ObterProdutos(produtos);
        }

        [HttpGet("filter/preco/pagination")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosFilterPreco([FromQuery] ProdutosFiltroPreco produtosFilterParameters)
        {
            var produtos = await _unitOfWork.ProdutoRepository.GetProdutosFiltroPrecoAsync(produtosFilterParameters);
            return ObterProdutos(produtos);
        }
        private ActionResult<IEnumerable<ProdutoDTO>> ObterProdutos(X.PagedList.IPagedList<Produto> produtos)
        {
            var metadata = new
            {
                produtos.Count,
                produtos.PageSize,
                produtos.PageCount,
                produtos.TotalItemCount,
                produtos.HasNextPage,
                produtos.HasPreviousPage
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));
            var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
            return Ok(produtosDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get() //ActionResult -> pode retornar uma lista de produtos ou todos os métodos de ActionResult
        {
            var produtos = await _unitOfWork.ProdutoRepository.GetAllAsync();
            if (produtos is null)
            {
                return NotFound("Produto não encontrado...");
            }

            var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
            return Ok(produtosDto);
        }

        [HttpGet("{id:int}", Name="ObterProduto")]
        public async Task<ActionResult<ProdutoDTO>> Get(int id)
        {
            var produto = await _unitOfWork.ProdutoRepository.GetAsync(c => c.ProdutoId == id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado...");
            }
            var produtoDto = _mapper.Map<ProdutoDTO>(produto);
            return Ok(produtoDto);
        }   

        [HttpPost]
        public async Task<ActionResult<ProdutoDTO>> Post(ProdutoDTO produtoDto)
        {
            if (produtoDto is null)
                return BadRequest(); //HTTP 400

          var produto = _mapper.Map<Produto>(produtoDto);

          var novoProduto = _unitOfWork.ProdutoRepository.Create(produto);
          await _unitOfWork.CommitAsync();

            var novoProdutoDto = _mapper.Map<ProdutoDTO>(novoProduto);

            return new CreatedAtRouteResult("ObterProduto", new { id = novoProdutoDto.ProdutoId }, novoProdutoDto);
        }

        [HttpPatch("{id}/UpdatePartial")]
        public async Task<ActionResult<ProdutoDTOUpdateResponse>> Patch(int id, JsonPatchDocument<ProdutoDTOUpdateRequest> patchProdutoDto)
        {
            if (patchProdutoDto is null || id <= 0)
                return BadRequest("Dados inválidos.");

            var produto = await  _unitOfWork.ProdutoRepository.GetAsync(c => c.ProdutoId == id);

            if (produto is null)
                return NotFound("Produto não encontrado.");

            var produtoUpdateRequest = _mapper.Map<ProdutoDTOUpdateRequest>(produto);

            patchProdutoDto.ApplyTo(produtoUpdateRequest, ModelState);

            if (!(ModelState.IsValid) || !(TryValidateModel(produtoUpdateRequest)))
                return BadRequest(ModelState);

            _mapper.Map(produtoUpdateRequest, produto);

            _unitOfWork.ProdutoRepository.Update(produto);
            await _unitOfWork.CommitAsync();

            return Ok(_mapper.Map<ProdutoDTOUpdateResponse>(produto));
        }



        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProdutoDTO>> Put(int id, ProdutoDTO produtoDto)
        {
            if (id != produtoDto.ProdutoId)
                return BadRequest();

            var produto = _mapper.Map<Produto>(produtoDto);

            var produtoAtualizado = _unitOfWork.ProdutoRepository.Update(produto);
            await _unitOfWork.CommitAsync();

            var produtoAtualizadoDto = _mapper.Map<ProdutoDTO>(produtoAtualizado);

            return Ok(produtoAtualizadoDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProdutoDTO>> Delete(int id)
        {
           var produto = await _unitOfWork.ProdutoRepository.GetAsync(c => c.ProdutoId == id);
           if (produto is null)
           {
               return NotFound("Produto não encontrado...");
           }

           var produtoDeletado = _unitOfWork.ProdutoRepository.Delete(produto);
           await _unitOfWork.CommitAsync();

            var produtoDeletadoDto = _mapper.Map<ProdutoDTO>(produtoDeletado);

           return Ok(produtoDeletadoDto);
        }   
    }
}
