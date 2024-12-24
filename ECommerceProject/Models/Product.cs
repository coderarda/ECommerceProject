using System;
using System.Collections.Generic;

namespace ECommerceProject.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductTitle { get; set; } = null!;

    public int ProductPrice { get; set; }

    public string? ProductCategory { get; set; }

    public byte[] ProductImage { get; set; } = null!;

    public string? ProductDescription { get; set; }

    public int CommentCommentId { get; set; }

    public int SellerSellerId { get; set; }

    public virtual Comment CommentComment { get; set; } = null!;

    public virtual Seller SellerSeller { get; set; } = null!;
}
