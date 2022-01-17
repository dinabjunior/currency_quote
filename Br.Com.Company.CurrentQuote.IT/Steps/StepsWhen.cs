using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Br.Com.Company.CurrencyQuote.Data.Entities.Enums;
using Br.Com.Company.CurrencyQuote.Domain.Dtos;
using Br.Com.Company.CurrentQuote.IT.Models;
using Br.Com.Company.CurrentQuote.IT.Models.Dtos;
using FluentAssertions;
using Flurl.Http;
using TechTalk.SpecFlow;

namespace Br.Com.Company.CurrentQuote.IT.Steps
{
    public partial class Steps
    {
        [When(@"o usuário solicitar a inclusão das taxas")]
        public async Task QuandoOUsuarioSolicitarAInclusaoDasTaxas()
        {
            var itemsToInsert = _scenarioContext[nameof(QuandoOUsuarioSolicitarAInclusaoDasTaxas)] as IEnumerable<SegmentRateModel>;

            if (itemsToInsert != null)
            {
                foreach (var newItem in itemsToInsert)
                {
                    var flurlClient = new FlurlClient(HttpClient);
                    var response = await flurlClient.Request("/api/quotation")
                                                    .PostJsonAsync(new
                                                    {
                                                        Rate = newItem.Rate,
                                                        Segment = newItem.Segment
                                                    });

                    response.StatusCode.Should().Be((int)HttpStatusCode.OK);
                }
            }
        }

        [When(@"o usuário alterar a taxa de ""(.*)"" para (.*)")]
        public async Task QuandoOUsuarioAlterarATaxaDePara(Guid id, decimal rate)
        {
            var flurlClient = new FlurlClient(HttpClient);
            var response = await flurlClient.Request("/api/quotation")
                                            .PutJsonAsync(new
                                            {
                                                Id = id,
                                                Rate = rate
                                            });

            response.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
        }

        [When(@"o usuário deletar a taxa ""(.*)""")]
        public async Task QuandoOUsuarioDeletarATaxa(Guid id)
        {
            var flurlClient = new FlurlClient(HttpClient);
            var response = await flurlClient.Request($"/api/quotation/{id}").DeleteAsync();

            response.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
        }

        [When(@"o usuário consultar a taxa pelo id ""(.*)""")]
        public async Task QuandoOUsuarioConsultarATaxaPeloId(Guid id)
        {
            var flurlClient = new FlurlClient(HttpClient);
            var response = await flurlClient.Request($"/api/quotation/{id}").GetJsonAsync<TestSegmentRateDto>();

            _scenarioContext[nameof(QuandoOUsuarioConsultarATaxaPeloId)] = response;
            _scenarioContext[nameof(EntaoDeveRetornarOsSeguintesRegistros)] = nameof(QuandoOUsuarioConsultarATaxaPeloId);
        }

        [When(@"o usuário consultar a taxa pelo segmento ""(.*)""")]
        public async Task QuandoOUsuarioConsultarATaxaPeloSegmento(SegmentEnum segment)
        {
            var flurlClient = new FlurlClient(HttpClient);
            var response = await flurlClient.Request($"/api/quotation/search")
                                            .SetQueryParam("segment", segment)
                                            .GetJsonAsync<IEnumerable<TestSegmentRateDto>>();

            _scenarioContext[nameof(QuandoOUsuarioConsultarATaxaPeloSegmento)] = response;
            _scenarioContext[nameof(EntaoDeveRetornarOsSeguintesRegistros)] = nameof(QuandoOUsuarioConsultarATaxaPeloSegmento);
        }

        [When(@"o usuário consultar o valor da taxa pelo segmento ""(.*)""")]
        public async Task QuandoOUsuarioConsultarOValorDaTaxaPeloSegmento(SegmentEnum segment)
        {
            var flurlClient = new FlurlClient(HttpClient);
            var response = await flurlClient.Request($"/api/quotation/searchRate")
                                            .SetQueryParam("segment", segment)
                                            .GetJsonAsync<decimal>();

            _scenarioContext[nameof(QuandoOUsuarioConsultarOValorDaTaxaPeloSegmento)] = response;
            _scenarioContext[nameof(EntaoDeveSerRetornadoOValor)] = nameof(QuandoOUsuarioConsultarOValorDaTaxaPeloSegmento);
        }
    }
}
