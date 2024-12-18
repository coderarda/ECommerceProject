using System;
using System.Collections.Generic;

namespace ECommerceProject.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string UserBirthDate { get; set; } = null!;

    public sbyte UserGender { get; set; }

    public string UserPassword { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<PaymentInfo> PaymentInfos { get; set; } = new List<PaymentInfo>();

    public virtual ICollection<Seller> Sellers { get; set; } = new List<Seller>();
}
