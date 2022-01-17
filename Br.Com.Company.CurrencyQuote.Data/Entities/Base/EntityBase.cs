using System;

namespace Br.Com.Company.CurrencyQuote.Data.Entities.Base
{
    /// <summary>
    /// Entity base of system
    /// </summary>
    public class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
