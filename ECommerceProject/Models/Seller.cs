using System;
using System.Collections.Generic;

namespace ECommerceProject.Models;

public partial class Seller
{
    public int SellerId { get; set; }

    public string? SellerTitle { get; set; }

    public float? SellerRating { get; set; }

    public int UserUserId { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual User UserUser { get; set; } = null!;
}
