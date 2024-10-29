using System;
using System.Collections.Generic;

namespace ArticleGeneration.Models;

public partial class Article
{
    public int ArticleId { get; set; }

    public string? Title { get; set; }

    public string? ShortDescription { get; set; }

    public string? FullDescription { get; set; }

    public int TransactionId { get; set; }

    public virtual Transaction? Transaction { get; set; }
}
