using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FeemanagementSystem.Models
{
    public partial class UploadFileEdit
    {
		public int UploadId { get; set; }

		public int DocId { get; set; }

		public int TypeId { get; set; }

		public string? DocFile { get; set; }

		[NotMapped]
		[Required(ErrorMessage = "Please select file")]
		[DataType(DataType.Upload)]
		public IFormFile? FileUpload { get; set; }

		public int UserId { get; set; }
	}
}
