using MarketTracker.Entities;
using MarketTracker.Interfaces;
using MarketTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace SmartAssistInforTecApi.Controllers
{
    [Route("Api/BaseDeProdutos/")]
    public class BaseDeProdutosController : ControllerBase
    {
        #region Tentativa de Extração de Produtos do Carrefour

        //[HttpGet]
        //[Route("ExtrairProdutosCarrefourPorNomeDeProduto/{nomeProduto}"), AllowAnonymous]
        //public ActionResult<string> ExtrairProdutosCarrefourPorNomeDeProduto(string nomeProduto,
        //    [FromServices] IMercados _mercadosRepo,
        //    [FromServices] IProdutos _produtosRepo,
        //    [FromServices] IRelMercadoProdutoPreco _relMercadoProdutoPreco)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest();

        //    string erro = "";
        //    int qtdTentativasPorPeca = 5;

        //    string[] produtosToSearch = new string[] { "Cerveja", "Biscoito", "Vinho", "Arroz", "Feijão" };

        //    foreach (string produto in produtosToSearch)
        //    {

        //        WriteLog($"{DateTime.Now} - Buscando produtos '{produto}' no site. Link: https://mercado.carrefour.com.br/s/{produto}?map=term");

        //        try
        //        {
        //            string htmlPage = "";//GetHtmlResponseFromUrl($"https://mercado.carrefour.com.br/s/{produto}?map=term", qtdTentativasPorPeca);

        //            List<REL_MERCADO_PRODUTO_PRECO> produtos = LeitorDeProdutos.GetProdutosCarrefourByHtmlString(htmlPage);

        //            WriteLog($"{DateTime.Now} - Produtos lido com sucesso. {Environment.NewLine} {JsonConvert.SerializeObject(produtos)}");

        //            return Ok(produtos);
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest();
        //        }
        //    }

        //    return NotFound();
        //}

        #endregion

        [HttpGet]
        [Route("GetProdutoByID/{produtoID}"), AllowAnonymous]
        public ActionResult<PRODUTOS> GetProdutoByID(int produtoID,
            [FromServices] IProdutos _produtosRepo)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            PRODUTOS produtoDatabase;

            try
            {
                produtoDatabase = _produtosRepo.GetByProdutoByID(produtoID);

                if (produtoDatabase == null)
                    return NotFound("Não foi encontrado nenhum produto a partir do ID informado.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(produtoDatabase);
        }

        [HttpGet]
        [Route("ListarProdutos"), AllowAnonymous]
        public ActionResult<List<PRODUTOS>> ListarProdutos([FromServices] IProdutos _produtosRepo)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            List<PRODUTOS> produtosDatabase;

            try
            {
                produtosDatabase = _produtosRepo.ListarProdutos();

                if (produtosDatabase.Count == 0)
                    return NotFound("Não foi encontrado nenhum produto.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(produtosDatabase);
        }

        [HttpGet]
        [Route("BuscaProdutosPorNome/{nomeProduto}"), AllowAnonymous]
        public ActionResult<List<PRODUTOS>> BuscaProdutosPorNome(string nomeProduto,
            [FromServices] IProdutos _produtosRepo)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            List<PRODUTOS> produtosDatabase;

            try
            {
                produtosDatabase = _produtosRepo.BuscaProdutosPorNome(nomeProduto);

                if (produtosDatabase.Count == 0)
                    return NotFound("Não foi encontrado nenhum produto a partir do texto de busca.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(produtosDatabase);
        }

        [HttpGet]
        [Route("GetMercadoByID/{mercadoID}"), AllowAnonymous]
        public ActionResult<MERCADOS> GetMercadoByID(int mercadoID,
            [FromServices] IMercados _mercadosRepo)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            MERCADOS mercadoDatabase;

            try
            {
                mercadoDatabase = _mercadosRepo.GetByMercadoByID(mercadoID);

                if (mercadoDatabase == null)
                    return NotFound("Não foi encontrado nenhum mercado a partir do ID informado.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(mercadoDatabase);
        }

        [HttpGet]
        [Route("ListarMercados"), AllowAnonymous]
        public ActionResult<List<MERCADOS>> ListarMercados([FromServices] IMercados _mercadosRepo)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            List<MERCADOS> mercadosDatabase;

            try
            {
                mercadosDatabase = _mercadosRepo.ListarMercados();

                if (mercadosDatabase.Count == 0)
                    return NotFound("Não foi encontrado nenhum mercado.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(mercadosDatabase);
        }

        [HttpGet]
        [Route("BuscaMercadoPorNome/{nomeMercado}"), AllowAnonymous]
        public ActionResult<List<MERCADOS>> BuscaMercadoPorNome(string nomeMercado,
            [FromServices] IMercados _mercadosRepo)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            List<MERCADOS> mercadosDatabase;

            try
            {
                mercadosDatabase = _mercadosRepo.BuscaMercadosPorNome(nomeMercado);

                if (mercadosDatabase.Count == 0)
                    return NotFound("Não foi encontrado nenhum mercado a partir do texto de busca.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(mercadosDatabase);
        }

        [HttpPost]
        [Route("RegistrarProduto/{nomeProduto}"), AllowAnonymous]
        public ActionResult<PRODUTOS> RegistrarProduto(string nomeProduto,
            [FromServices] IProdutos _produtosRepo)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            PRODUTOS produtoCreated;

            try
            {
                produtoCreated = _produtosRepo.AdicionarProduto(nomeProduto, "");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(produtoCreated);
        }

        [HttpPost]
        [Route("RegistrarMercado/{nomeMercado}"), AllowAnonymous]
        public ActionResult<MERCADOS> RegistrarMercado(string nomeMercado,
            [FromServices] IMercados _mercadosRepo)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            MERCADOS mercadoCreated;

            try
            {
                mercadoCreated = _mercadosRepo.AdicionarMercado(nomeMercado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(mercadoCreated);
        }

        [HttpPost]
        [Route("RegistrarPrecoDeProduto/{mercadoID}/{produtoID}/{precoID}"), AllowAnonymous]
        public ActionResult<bool> RegistrarPrecoDeProduto(int mercadoID, int produtoID, decimal precoID,
            [FromServices] IRelMercadoProdutoPreco _relMercadoProdutoPreco)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                _relMercadoProdutoPreco.AdicionarNovaRelacaoMercadoProdutoPreco(mercadoID, produtoID, precoID);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(true);
        }

        [HttpGet]
        [Route("BuscarProduto/{textoDeBusca}"), AllowAnonymous]
        public ActionResult<List<REL_MERCADO_PRODUTO_PRECO>> BuscarProduto(string textoDeBusca,
            [FromServices] IRelMercadoProdutoPreco _relProdutoPreco)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            List<REL_MERCADO_PRODUTO_PRECO> produtos = null;

            try
            {
                produtos = _relProdutoPreco.BuscarPorTexto(textoDeBusca.ToUpper());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (produtos.Count == 0)
                return NotFound();

            return Ok(produtos.OrderBy(x => x.PRECO));
        }

        [HttpGet]
        [Route("BuscarProdutoByTextoAndLatLong/{textoDeBusca}/{latitude}/{longitude}"), AllowAnonymous]
        public ActionResult<List<BuscaProdutoViewModel>> BuscarProdutoByTextoAndLatLong(string textoDeBusca, double latitude, double longitude,
            [FromServices] IRelMercadoProdutoPreco _relProdutoPreco)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            List<REL_MERCADO_PRODUTO_PRECO> produtos = null;

            try
            {
                produtos = _relProdutoPreco.BuscarPorTexto(textoDeBusca.ToUpper());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (produtos.Count == 0)
                return NotFound();

            List<BuscaProdutoViewModel> returnList = new List<BuscaProdutoViewModel>();

            foreach (REL_MERCADO_PRODUTO_PRECO relacao in produtos)
            {
                returnList.Add(new BuscaProdutoViewModel
                {
                    DATA_HORA_REGISTRO = relacao.DATA_HORA_REGISTRO,
                    MERCADO = relacao.MERCADO.NOME,
                    ENDERECO_MERCADO = relacao.MERCADO.ENDERECO_COMPLETO,
                    PRODUTO = relacao.PRODUTO.NOME,
                    PRECO = relacao.PRECO,
                    DISTANCIA = CoordinatesHelper.GetDistanceBetweenPoints(latitude, longitude, Convert.ToDouble(relacao.MERCADO.LAT), Convert.ToDouble(relacao.MERCADO.LONG))
                });
            }

            return Ok(returnList.OrderBy(x => x.PRECO));
        }

        [HttpPost]
        [Route("BuscarByProdutosIDsListAndLatLong/{latitude}/{longitude}"), AllowAnonymous]
        public ActionResult<List<BuscaProdutoViewModel>> BuscarByProdutosIDsListAndLatLong([FromBody] List<int> produtosIds, double latitude, double longitude,
            [FromServices] IRelMercadoProdutoPreco _relProdutoPreco)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            List<REL_MERCADO_PRODUTO_PRECO> produtos = null;

            try
            {
                produtos = _relProdutoPreco.BuscarPorIdsProdutos(produtosIds);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (produtos.Count == 0)
                return NotFound();

            produtos = produtos
                .OrderBy(x => x.ID_MERCADO)
                .ToList();

            List<BuscaProdutoViewModel> returnList = new List<BuscaProdutoViewModel>();

            int mercadoAtual = -1;
            int quantidadeProdutos = 0;

            List<BuscaCarrinhoProdutosViewModel> carrinhoMercado = new List<BuscaCarrinhoProdutosViewModel>();

            foreach (REL_MERCADO_PRODUTO_PRECO relacao in produtos)
            {
                if (mercadoAtual != relacao.ID_MERCADO)
                {
                    carrinhoMercado.Add(new BuscaCarrinhoProdutosViewModel
                    {
                        MERCADO = relacao.MERCADO.NOME,
                        ENDERECO_MERCADO = relacao.MERCADO.ENDERECO_COMPLETO,
                        VALOR_TOTAL = relacao.PRECO,
                        DISTANCIA = CoordinatesHelper.GetDistanceBetweenPoints(latitude, longitude, Convert.ToDouble(relacao.MERCADO.LAT), Convert.ToDouble(relacao.MERCADO.LONG))
                    });

                    carrinhoMercado.Last().PRODUTOS_DISPONIVEIS.Add(relacao.ID_PRODUTO);
                    mercadoAtual = relacao.ID_MERCADO;
                }
                else
                {
                    carrinhoMercado.Last().PRODUTOS_DISPONIVEIS.Add(relacao.ID_PRODUTO);
                    carrinhoMercado.Last().VALOR_TOTAL += relacao.PRECO;
                }
            }

            return Ok(carrinhoMercado.OrderBy(x => x.VALOR_TOTAL));
        }

        [HttpGet]
        [Route("BuscarByProdutosIDsListAndLatLong/{latitude}/{longitude}/{ids}"), AllowAnonymous]
        public ActionResult<List<BuscaProdutoViewModel>> BuscarByProdutosIDsListAndLatLong(string ids, double latitude, double longitude,
            [FromServices] IRelMercadoProdutoPreco _relProdutoPreco)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            List<REL_MERCADO_PRODUTO_PRECO> produtos = null;

            string[] splitIds = ids.Split(",");

            List<int> produtosIds = new List<int>();

            foreach(string id in splitIds)
            {
                if (int.TryParse(id, out int convertedID))
                    produtosIds.Add(convertedID);
            }

            try
            {
                produtos = _relProdutoPreco.BuscarPorIdsProdutos(produtosIds);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (produtos.Count == 0)
                return NotFound();

            produtos = produtos
                .OrderBy(x => x.ID_MERCADO)
                .ToList();

            List<BuscaProdutoViewModel> returnList = new List<BuscaProdutoViewModel>();

            int mercadoAtual = -1;
            int quantidadeProdutos = 0;

            List<BuscaCarrinhoProdutosViewModel> carrinhoMercado = new List<BuscaCarrinhoProdutosViewModel>();

            foreach (REL_MERCADO_PRODUTO_PRECO relacao in produtos)
            {
                if (mercadoAtual != relacao.ID_MERCADO)
                {
                    carrinhoMercado.Add(new BuscaCarrinhoProdutosViewModel
                    {
                        MERCADO = relacao.MERCADO.NOME,
                        ENDERECO_MERCADO = relacao.MERCADO.ENDERECO_COMPLETO,
                        VALOR_TOTAL = relacao.PRECO * produtosIds.Where(x => x == relacao.ID_PRODUTO).Count(),
                        DISTANCIA = CoordinatesHelper.GetDistanceBetweenPoints(latitude, longitude, Convert.ToDouble(relacao.MERCADO.LAT), Convert.ToDouble(relacao.MERCADO.LONG))
                    });

                    carrinhoMercado.Last().PRODUTOS_DISPONIVEIS.Add(relacao.ID_PRODUTO);
                    mercadoAtual = relacao.ID_MERCADO;
                }
                else
                {
                    carrinhoMercado.Last().PRODUTOS_DISPONIVEIS.Add(relacao.ID_PRODUTO);
                    carrinhoMercado.Last().VALOR_TOTAL += relacao.PRECO;
                }
            }

            return Ok(carrinhoMercado.OrderBy(x => x.VALOR_TOTAL));
        }

        [HttpGet]
        [Route("ListarTodosOsProdutosRelacionados"), AllowAnonymous]
        public ActionResult<List<REL_MERCADO_PRODUTO_PRECO>> ListarTodosOsProdutosRelacionados([FromServices] IRelMercadoProdutoPreco _relProdutoPreco)
        {
            return Ok(_relProdutoPreco.ListarTodos());
        }

        #region OLD 
        //    SA_PECAS pecaToInsert = new SA_PECAS
        //    {
        //        ID_FABRICANTE = fabricanteID,
        //        NOME = pecaMolina.NOME.Trim(),
        //        DESCRICAO = pecaMolina.DESCRICAO.Trim(),
        //        COD_FABRICANTE = pecaMolina.COD.Trim(),
        //        COD_MONTADORA = "0",
        //        COD_BARRAS = pecaMolina.COD_BARRAS,
        //        ATIVO = pecaMolina.FORA_DE_LINHA ? "N" : "S"
        //    };

        //    if (!_pecasRepo.AddPeca(pecaToInsert, ref erro))
        //    {
        //        WriteLog($"{DateTime.Now} - {erro}");
        //        erro = "";
        //        continue;
        //    }

        //    WriteLog($"{DateTime.Now} - Peça inserida com sucesso. ID da peça: {pecaToInsert.ID}");

        //    SA_INPUT_MODELO inputModelo = new SA_INPUT_MODELO();
        //    List<SA_MODELO_FIPE> modelosFipe = new List<SA_MODELO_FIPE>();
        //    List<SA_REL_INPUT_MOD_FIPE> relacoesInputModeloFipe = new List<SA_REL_INPUT_MOD_FIPE>();

        //    foreach (Aplicacoes aplicacao in pecaMolina.APLICACOES)
        //    {
        //        WriteLog($"{DateTime.Now} - Inserindo aplicação '{aplicacao}'");

        //        inputModelo = _inputModeloRepo.InsertInputModelo(new SA_INPUT_MODELO
        //        {
        //            MONTADORA = aplicacao.Montadora.ToUpper(),
        //            MODELO = aplicacao.Modelo.ToUpper(),
        //            MOTOR = aplicacao.Motor.ToUpper()
        //        }, ref erro);

        //        if (!string.IsNullOrEmpty(erro))
        //        {
        //            WriteLog($"{DateTime.Now} - {erro}");
        //            erro = "";
        //            continue;
        //        }

        //        if (!_relInputModeloFipeRepo.GetIfInputHasAlreadyBeenRelatedToFipe(inputModelo.ID, ref erro))
        //        {
        //            modelosFipe = _fipeRepo.GetMatchesByInputs(inputModelo.MONTADORA, inputModelo.MODELO, inputModelo.MOTOR, ref erro);

        //            if (!string.IsNullOrEmpty(erro))
        //            {
        //                WriteLog($"{DateTime.Now} - {erro}");
        //                erro = "";
        //                continue;
        //            }

        //            relacoesInputModeloFipe = _relInputModeloFipeRepo.InsertRelInputModFipe(inputModelo, modelosFipe.Select(x => x.ID).ToList(), ref erro);

        //            if (!string.IsNullOrEmpty(erro))
        //            {
        //                WriteLog($"{DateTime.Now} - {erro}");
        //                erro = "";
        //                continue;
        //            }

        //            _relModeloPecas.InsertRelModeloPecas(pecaToInsert.ID, relacoesInputModeloFipe.Select(x => x.ID_FIPE).ToList(), aplicacao.AnoInicio, aplicacao.AnoFim, ref erro);

        //            if (!string.IsNullOrEmpty(erro))
        //            {
        //                WriteLog($"{DateTime.Now} - {erro}");
        //                erro = "";
        //                continue;
        //            }
        //        }
        //        else
        //        {
        //            relacoesInputModeloFipe = _relInputModeloFipeRepo.GetRelInputModFipeByInputID(inputModelo.ID, ref erro);

        //            if (!string.IsNullOrEmpty(erro))
        //            {
        //                WriteLog($"{DateTime.Now} - {erro}");
        //                erro = "";
        //                continue;
        //            }

        //            _relModeloPecas.InsertRelModeloPecas(pecaToInsert.ID, relacoesInputModeloFipe.Select(x => x.ID_FIPE).ToList(), aplicacao.AnoInicio, aplicacao.AnoFim, ref erro);

        //            if (!string.IsNullOrEmpty(erro))
        //            {
        //                WriteLog($"{DateTime.Now} - {erro}");
        //                erro = "";
        //                continue;
        //            }
        //        }

        //        if (!string.IsNullOrEmpty(erro))
        //        {
        //            WriteLog($"{DateTime.Now} - {erro}");
        //            erro = "";
        //            continue;
        //        }
        //    }

        //    if (pecaMolina.OEMS.Count > 0)
        //    {
        //        List<SA_OEM> oemsList = _oemRepo.InsertByOemList(pecaMolina.OEMS, ref erro);

        //        if (!string.IsNullOrEmpty(erro))
        //        {
        //            WriteLog($"{DateTime.Now} - {erro}");
        //            continue;
        //        }

        //        List<SA_REL_PECA_OEM> relPecaOem = _relPecaOemRepo.InsertRelPecaOem(pecaToInsert.ID, oemsList.Select(x => x.ID).ToList(), ref erro);

        //        if (!string.IsNullOrEmpty(erro))
        //        {
        //            WriteLog($"{DateTime.Now} - {erro}");
        //            continue;
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    WriteLog($"{DateTime.Now} - Houve algum problema para ler a peça no site. " + ex.Message);
        //    erro = "";
        //    continue;
        //}
        //    }
        //}

        #endregion

        [NonAction]
        public static string GetHtmlResponseFromUrl(string url, int qtdTentativas)
        {
            string errorMessage = "";
            int tentativaAtual = 0;

            do
            {
                tentativaAtual++;

                try
                {
                    using (WebClient web = new WebClient())
                        return HttpUtility.HtmlDecode(web.DownloadString(url));
                }
                catch (Exception ex)
                {
                    errorMessage += $"Tentativa {tentativaAtual}: {ex.Message}{Environment.NewLine}";
                }

            } while (tentativaAtual < qtdTentativas);

            throw new Exception("Não foi possível obter a página do caminho informado. Segue abaixo o log das tentativas: \n\n" + errorMessage);
        }
        [NonAction]
        private void WriteLog(string log)
        {
#if DEBUG
            System.IO.File.AppendAllText("log.txt", log + Environment.NewLine);
            Console.WriteLine(log);
#endif
        }

    }
}
