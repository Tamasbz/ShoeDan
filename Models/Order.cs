using System;
using System.Collections.Generic;

namespace ShoeStore.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public decimal TotalPrice { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User User { get; set; } = null!;
}
