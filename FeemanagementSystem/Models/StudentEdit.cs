namespace FeemanagementSystem.Models
{
    public class StudentEdit
    {
		public int UserId { get; set; }

		public string UserEmail { get; set; } = null!;

		public string Upassword { get; set; } = null!;

		public string FullName { get; set; } = null!;

		public string Phone { get; set; } = null!;

		public string UserAddress { get; set; } = null!;

		public bool? LoginStatus { get; set; }

		public string UserRoleType { get; set; } = null!;

		public int StdId { get; set; }

		public int Cid { get; set; }

		public int? Bid { get; set; }	

		public short RollNo { get; set; }
	}
}
