using System;
using System.Collections.Generic;

namespace ECommerceProject.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public string CommentTitle { get; set; } = null!;

    public string CommentText { get; set; } = null!;

    public int UserUserId { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual User UserUser { get; set; } = null!;
}
