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
        [HttpGet]
        [Route("ExtrairProdutosCarrefourPorNomeDeProduto/{nomeProduto}"), AllowAnonymous]
        public ActionResult<string> ExtrairProdutosCarrefourPorNomeDeProduto(string nomeProduto,
            [FromServices] IMercados _mercadosRepo,
            [FromServices] IProdutos _produtosRepo,
            [FromServices] IRelMercadoProdutoPreco _relMercadoProdutoPreco)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            string erro = "";
            int qtdTentativasPorPeca = 5;

            string[] produtosToSearch = new string[] { "Cerveja", "Biscoito", "Vinho", "Arroz", "Feijão" };

            foreach (string produto in produtosToSearch)
            {

                WriteLog($"{DateTime.Now} - Buscando produtos '{produto}' no site. Link: https://mercado.carrefour.com.br/s/{produto}?map=term");

                try
                {
                    string htmlPage = "";//GetHtmlResponseFromUrl($"https://mercado.carrefour.com.br/s/{produto}?map=term", qtdTentativasPorPeca);

                    List<REL_MERCADO_PRODUTO_PRECO> produtos = LeitorDeProdutos.GetProdutosCarrefourByHtmlString(htmlPage);

                    WriteLog($"{DateTime.Now} - Produtos lido com sucesso. {Environment.NewLine} {JsonConvert.SerializeObject(produtos)}");

                    return Ok(produtos);
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }

            return NotFound();
        }

        [HttpPost]
        [Route("RegistrarProduto"), AllowAnonymous]
        public ActionResult<bool> RegistrarProduto([FromBody] PRODUTOS produtoToInsert,
            [FromServices] IProdutos _produtosRepo)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            PRODUTOS produtoCreated = null;

            try
            {
                _produtosRepo.AdicionarProduto(produtoToInsert.NOME, produtoToInsert.COD_BARRAS);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(true);
        }

        [HttpPost]
        [Route("RegistrarMercado"), AllowAnonymous]
        public ActionResult<bool> RegistrarMercado([FromBody] MERCADOS mercadoToInsert,
            [FromServices] IMercados _mercadosRepo)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                _mercadosRepo.AdicionarMercado(mercadoToInsert.NOME);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(true);
        }

        [HttpPost]
        [Route("RegistrarPrecoDeProduto"), AllowAnonymous]
        public ActionResult<bool> RegistrarPrecoDeProduto([FromBody] REL_MERCADO_PRODUTO_PRECO precoToInsert,
            [FromServices] IRelMercadoProdutoPreco _relMercadoProdutoPreco)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                _relMercadoProdutoPreco.AdicionarNovaRelacaoMercadoProdutoPreco(precoToInsert.ID_MERCADO, precoToInsert.ID_PRODUTO, precoToInsert.PRECO);
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
