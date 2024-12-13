using System;
using System.Collections.Generic;

namespace ShoeStore.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? Name { get; set; }

    public string? Color { get; set; }

    public string? Size { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string? Image { get; set; }

    public int? Stock { get; set; }

    public int? CategoryId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
