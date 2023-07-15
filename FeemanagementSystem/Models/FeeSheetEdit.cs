namespace FeemanagementSystem.Models
{
	public partial class FeeSheetEdit
	{
		public int SheetId { get; set; }

		public int StdId { get; set; }

		public int Fid { get; set; }

		public decimal Amount { get; set; }

		public DateTime DueDate { get; set; }

		public int EntryUserId { get; set; }

		public string EntryTime { get; set; } = null!;
		public  List<StudentView> students = new List<StudentView>();
	}
}
