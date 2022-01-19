using System.Collections.Generic;
using System.Threading.Tasks;
using Br.Com.Company.CurrencyQuote.Data.Entities;
using Br.Com.Company.CurrentQuote.IT.Models.Dtos;
using Br.Com.Company.CurrentQuote.IT.Support.Helpers;
using FluentAssertions;
using FluentAssertions.Collections;
using TechTalk.SpecFlow;

namespace Br.Com.Company.CurrentQuote.IT.Steps
{
    public partial class Steps
    {
        [Then(@"deve conter o seguinte registro na tabela SegmentRate")]
        public async Task EntaoDeveConterOSeguinteRegistroNaTabelaSegmentRate(Table table)
        {
            var actualItems = await DatabaseHelper.QueryAsync<SegmentRate>("select * from dbo.segment_rate");
            table.BeEquivalentTo(actualItems);
        }

        [Then(@"deve retornar os seguintes registros")]
        public void EntaoDeveRetornarOsSeguintesRegistros(Table table)
        {
            var scenario = _scenarioContext[nameof(EntaoDeveRetornarOsSeguintesRegistros)];
            if (Equals(scenario, nameof(QuandoOUsuarioConsultarATaxaPeloId)))
            {
                var resultById = _scenarioContext[nameof(QuandoOUsuarioConsultarATaxaPeloId)] as TestSegmentRateDto;
                var actualItems = new List<TestSegmentRateDto>();

                if (resultById != null)
                {
                    actualItems.Add(resultById);
                }

                table.BeEquivalentTo(actualItems);
            }
            else if (Equals(scenario, nameof(QuandoOUsuarioConsultarATaxaPeloSegmento)))
            {
                var resultBySegment = _scenarioContext[nameof(QuandoOUsuarioConsultarATaxaPeloSegmento)] as IEnumerable<TestSegmentRateDto>;

                resultBySegment.Should().NotBeNull();

                table.BeEquivalentTo(resultBySegment);
            }
        }

        [Then(@"deve ser retornado o valor (.*)")]
        public void EntaoDeveSerRetornadoOValor(decimal expectedValue)
        {
            var scenario = _scenarioContext[nameof(EntaoDeveSerRetornadoOValor)];
            if (Equals(scenario, nameof(QuandoOUsuarioConsultarOValorDaTaxaPeloSegmento)))
            {
                var rateValueBySegment = _scenarioContext[nameof(QuandoOUsuarioConsultarOValorDaTaxaPeloSegmento)] as decimal?;

                rateValueBySegment.Should().NotBeNull();
                rateValueBySegment.Should().Be(expectedValue);
            }
            else if (Equals(scenario, nameof(QuandoOUsuarioConsultarOValorDaTaxaPeloId)))
            {
                var rateValueBySegment = _scenarioContext[nameof(QuandoOUsuarioConsultarOValorDaTaxaPeloId)] as decimal?;

                rateValueBySegment.Should().NotBeNull();
                rateValueBySegment.Should().Be(expectedValue);
            }
            else if (Equals(scenario, nameof(QuandoOUsuarioSolicitarOCalculoDeConversaoDaMoedaParaRealParaOSegmentoVarejo)))
            {
                var totalValue = _scenarioContext[nameof(QuandoOUsuarioSolicitarOCalculoDeConversaoDaMoedaParaRealParaOSegmentoVarejo)] as decimal?;

                totalValue.Should().NotBeNull();
                totalValue.Should().Be(expectedValue);
            }
        }
    }
}
