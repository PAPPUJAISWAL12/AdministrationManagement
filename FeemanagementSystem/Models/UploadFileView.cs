using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FeemanagementSystem.Models;

public partial class UploadFileView
{
    public int UploadId { get; set; }

    public int DocId { get; set; }

    public int TypeId { get; set; }

    public string? DocFile { get; set; }

	[NotMapped]
	[Required(ErrorMessage = "Please select file")]
	[DataType(DataType.Upload)]
	public IFormFile? FileUpload { get; set; }

	public string DocumetCat { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string UserAddress { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public int UserId { get; set; }

	public string UserEmail { get; set; } = null!;
}
