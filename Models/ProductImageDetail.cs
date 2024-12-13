using System;
using System.Collections.Generic;

namespace ShoeStore.Models;

public partial class ProductImageDetail
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string Image { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
