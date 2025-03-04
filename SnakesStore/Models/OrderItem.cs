using System;
using System.Collections.Generic;

namespace SnakesStore.Models;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public int? OrderId { get; set; }

    public int? SnackId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Snack? Snack { get; set; }
}
