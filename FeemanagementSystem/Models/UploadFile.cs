using System;
using System.Collections.Generic;

namespace FeemanagementSystem.Models;

public partial class UploadFile
{
    public int UploadId { get; set; }

    public int DocId { get; set; }

    public int TypeId { get; set; }

    public string? DocFile { get; set; }

    public virtual Document Doc { get; set; } = null!;

    public virtual DocumentType Type { get; set; } = null!;
}
