using System;
using System.Collections.Generic;

namespace FeemanagementSystem.Models;

public partial class DocumentType
{
    public int TypeId { get; set; }

    public string DocumetCat { get; set; } = null!;

    public virtual ICollection<UploadFile> UploadFiles { get; set; } = new List<UploadFile>();
}
