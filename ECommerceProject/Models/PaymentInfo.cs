using System;
using System.Collections.Generic;

namespace ECommerceProject.Models;

public partial class PaymentInfo
{
    public int PaymentInfoId { get; set; }

    public int? PaymentInfoCreditCardNumber { get; set; }

    public string? PaymentInfoCvv { get; set; }

    public string? PaymentInfoExpirationDate { get; set; }

    public string? PaymentInfoCreditCardUserName { get; set; }

    public string? PaymentInfoTitle { get; set; }

    public int UserUserId { get; set; }

    public virtual User UserUser { get; set; } = null!;
}
