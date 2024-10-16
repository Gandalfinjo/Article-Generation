using System;
using System.Collections.Generic;

namespace ArticleGeneration.Models;

public partial class MarketType
{
    public int MarketTypeId { get; set; }

    public string MarketTypeName { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
