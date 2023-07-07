using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FeemanagementSystem.Models;

public partial class UserList
{
    public int UserId { get; set; }

    public string UserEmail { get; set; } = null!;
    
    [DataType(DataType.Password)]
    public string Upassword { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string UserAddress { get; set; } = null!;

    public bool? LoginStatus { get; set; }

    public string UserRoleType { get; set; } = null!;

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual ICollection<FeeHeader> FeeHeaderCancelledUsers { get; set; } = new List<FeeHeader>();

    public virtual ICollection<FeeHeader> FeeHeaderEntryUsers { get; set; } = new List<FeeHeader>();

    public virtual ICollection<FeeSheet> FeeSheetCancelledUsers { get; set; } = new List<FeeSheet>();

    public virtual ICollection<FeeSheet> FeeSheetEntryUsers { get; set; } = new List<FeeSheet>();

    public virtual ICollection<FeeStructure> FeeStructureCancelledUsers { get; set; } = new List<FeeStructure>();

    public virtual ICollection<FeeStructure> FeeStructureEntryUsers { get; set; } = new List<FeeStructure>();

    public virtual ICollection<ProgramInfo> ProgramInfoCancelledUsers { get; set; } = new List<ProgramInfo>();

    public virtual ICollection<ProgramInfo> ProgramInfoUsers { get; set; } = new List<ProgramInfo>();

    public virtual ICollection<Receipt> ReceiptCancelledUsers { get; set; } = new List<Receipt>();

    public virtual ICollection<Receipt> ReceiptEntryUsers { get; set; } = new List<Receipt>();

    public virtual ICollection<ReceiptPrint> ReceiptPrints { get; set; } = new List<ReceiptPrint>();

    public virtual ICollection<Receipt> ReceiptStds { get; set; } = new List<Receipt>();

    public virtual ICollection<Reception> ReceptionCancelledUsers { get; set; } = new List<Reception>();

    public virtual ICollection<Reception> ReceptionUsers { get; set; } = new List<Reception>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
