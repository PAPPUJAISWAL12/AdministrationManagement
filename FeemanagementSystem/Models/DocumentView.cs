using System;
using System.Collections.Generic;

namespace FeemanagementSystem.Models;

public partial class DocumentView
{
    public int DocId { get; set; }

    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string UserAddress { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string UserEmail { get; set; } = null!;
}
