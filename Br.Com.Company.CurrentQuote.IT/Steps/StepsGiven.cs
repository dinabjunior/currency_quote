using System;
using System.Threading.Tasks;
using Br.Com.Company.CurrencyQuote.Data.Entities;
using Br.Com.Company.CurrentQuote.IT.Models;
using Br.Com.Company.CurrentQuote.IT.Support.Helpers;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Br.Com.Company.CurrentQuote.IT.Steps
{
    public partial class Steps
    {
        [Given(@"as seguintes configurações de taxas para serem cadastradas")]
        public void DadoAsSeguintesConfiguracoesDeTaxasParaSeremCadastradas(Table table)
        {
            _scenarioContext[nameof(QuandoOUsuarioSolicitarAInclusaoDasTaxas)] = table.CreateSet<SegmentRateModel>();
        }

        [Given(@"as seguintes configurações de taxas já cadastradas no sistema")]
        public async Task DadoAsSeguintesConfiguracoesDeTaxasJaCadastradasNoSistema(Table table)
        {
            await DatabaseHelper.InsertTableIntoDatabaseAsync<SegmentRate>(table, "dbo.segment_rate");
        }
    }
}
