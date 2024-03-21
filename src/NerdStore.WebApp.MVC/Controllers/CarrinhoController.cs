using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Core.Bus;
using NerdStore.Vendas.Application.Commands;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class CarrinhoController : BaseController
    {
        private readonly IProdutoAppService _produtoAppService;
        private readonly IMediatrHandler _mediatorHandler;

        public CarrinhoController(IProdutoAppService produtoAppService, IMediatrHandler mediatorHandler)
        {
            _produtoAppService = produtoAppService;            
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("meu-carrinho")]
        public async Task<IActionResult> AdicionarItem(Guid id, int quantidade)
        {
            var produto = await _produtoAppService.ObterPorId(id);
            if (produto == null) return BadRequest();

            if (produto.QuantidadeEstoque < quantidade)
            {
                TempData["Erro"] = "Produto com estoque insuficiente";
                return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
            }

            var command = new AdicionarItemPedidoCommand(ClienteId, produto.Id, produto.Nome, quantidade, produto.Valor);
            await _mediatorHandler.EnviarComando(command);

            if (OperacaoValida())
            {
                return RedirectToAction("Index");
            }

            TempData["Erros"] = ObterMensagensErro();
            return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
        }
    }
}
