using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FeemanagementSystem.Models;

public partial class FeeManagementSystemContext : DbContext
{
    public FeeManagementSystemContext()
    {
    }

    public FeeManagementSystemContext(DbContextOptions<FeeManagementSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BusInfo> BusInfos { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<DocumentType> DocumentTypes { get; set; }

    public virtual DbSet<DocumentView> DocumentViews { get; set; }

    public virtual DbSet<FeeHeader> FeeHeaders { get; set; }

    public virtual DbSet<FeeHeaderView> FeeHeaderViews { get; set; }

    public virtual DbSet<FeeSheet> FeeSheets { get; set; }

    public virtual DbSet<FeeSheetView> FeeSheetViews { get; set; }

    public virtual DbSet<FeeStructure> FeeStructures { get; set; }

    public virtual DbSet<FeeStructureView> FeeStructureViews { get; set; }

    public virtual DbSet<PrograInfoView> PrograInfoViews { get; set; }

    public virtual DbSet<ProgramInfo> ProgramInfos { get; set; }

    public virtual DbSet<Receipt> Receipts { get; set; }

    public virtual DbSet<ReceiptDetail> ReceiptDetails { get; set; }

    public virtual DbSet<ReceiptPrint> ReceiptPrints { get; set; }

    public virtual DbSet<ReceiptView> ReceiptViews { get; set; }

    public virtual DbSet<Reception> Receptions { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentView> StudentViews { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<UploadFile> UploadFiles { get; set; }

    public virtual DbSet<UploadFileView> UploadFileViews { get; set; }

    public virtual DbSet<UserList> UserLists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=conn");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BusInfo>(entity =>
        {
            entity.HasKey(e => e.Bid).HasName("PK__BusInfo__C6DE0CC1F5966C3D");

            entity.ToTable("BusInfo");

            entity.Property(e => e.Bid).HasColumnName("BId");
            entity.Property(e => e.BusFee).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DestinationAddress).HasMaxLength(1);
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Cid).HasName("PK__Class__C1F8DC395F3A1864");

            entity.ToTable("Class");

            entity.Property(e => e.Cid).HasColumnName("CId");
            entity.Property(e => e.Cname).HasMaxLength(20);
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.DocId).HasName("PK__Document__3EF188AD9FBB617E");

            entity.ToTable("Document");

            entity.HasOne(d => d.User).WithMany(p => p.Documents)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Document__UserId__5165187F");
        });

        modelBuilder.Entity<DocumentType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__Document__516F03B5A2F40FCB");

            entity.ToTable("DocumentType");

            entity.Property(e => e.DocumetCat).HasMaxLength(100);
        });

        modelBuilder.Entity<DocumentView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("DocumentView");

            entity.Property(e => e.FullName).HasMaxLength(20);
            entity.Property(e => e.Phone).HasMaxLength(10);
            entity.Property(e => e.UserAddress).HasMaxLength(30);
            entity.Property(e => e.UserEmail).HasMaxLength(30);
        });

        modelBuilder.Entity<FeeHeader>(entity =>
        {
            entity.HasKey(e => e.Fid).HasName("PK__FeeHeade__C1BEAA4257DE92B8");

            entity.ToTable("FeeHeader");

            entity.Property(e => e.Fid).HasColumnName("FId");
            entity.Property(e => e.CancelledDate).HasColumnType("date");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.CancelledUser).WithMany(p => p.FeeHeaderCancelledUsers)
                .HasForeignKey(d => d.CancelledUserId)
                .HasConstraintName("FK__FeeHeader__Cance__628FA481");

            entity.HasOne(d => d.EntryUser).WithMany(p => p.FeeHeaderEntryUsers)
                .HasForeignKey(d => d.EntryUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FeeHeader__Entry__619B8048");
        });

        modelBuilder.Entity<FeeHeaderView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("FeeHeaderView");

            entity.Property(e => e.CancelledBy).HasMaxLength(20);
            entity.Property(e => e.CancelledDate).HasColumnType("date");
            entity.Property(e => e.EntryBy).HasMaxLength(20);
            entity.Property(e => e.Fid)
                .ValueGeneratedOnAdd()
                .HasColumnName("FId");
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<FeeSheet>(entity =>
        {
            entity.HasKey(e => e.SheetId).HasName("PK__FeeSheet__30B273E8DB379C62");

            entity.ToTable("FeeSheet");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CancelledDate).HasColumnType("date");
            entity.Property(e => e.DueDate).HasColumnType("date");
            entity.Property(e => e.EntryTime).HasMaxLength(20);
            entity.Property(e => e.FeeSheetStatus).HasMaxLength(1000);
            entity.Property(e => e.Fid).HasColumnName("FId");

            entity.HasOne(d => d.CancelledUser).WithMany(p => p.FeeSheetCancelledUsers)
                .HasForeignKey(d => d.CancelledUserId)
                .HasConstraintName("FK__FeeSheet__Cancel__6E01572D");

            entity.HasOne(d => d.EntryUser).WithMany(p => p.FeeSheetEntryUsers)
                .HasForeignKey(d => d.EntryUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FeeSheet__EntryU__6D0D32F4");

            entity.HasOne(d => d.FidNavigation).WithMany(p => p.FeeSheets)
                .HasForeignKey(d => d.Fid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FeeSheet__FId__6C190EBB");

            entity.HasOne(d => d.Std).WithMany(p => p.FeeSheets)
                .HasForeignKey(d => d.StdId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FeeSheet__StdId__6B24EA82");
        });

        modelBuilder.Entity<FeeSheetView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("FeeSheetView");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BusFee).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CancelledBy).HasMaxLength(20);
            entity.Property(e => e.CancelledDate).HasColumnType("date");
            entity.Property(e => e.Cname).HasMaxLength(20);
            entity.Property(e => e.DestinationAddress).HasMaxLength(1);
            entity.Property(e => e.DueDate).HasColumnType("date");
            entity.Property(e => e.EntryBy).HasMaxLength(20);
            entity.Property(e => e.EntryTime).HasMaxLength(20);
            entity.Property(e => e.FeeSheetStatus).HasMaxLength(1000);
            entity.Property(e => e.Fid).HasColumnName("FId");
            entity.Property(e => e.FullName).HasMaxLength(20);
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<FeeStructure>(entity =>
        {
            entity.HasKey(e => e.FsId).HasName("PK__FeeStruc__334C6DECD839BF00");

            entity.ToTable("FeeStructure");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CancelledDate).HasColumnType("date");
            entity.Property(e => e.DueDate).HasColumnType("date");
            entity.Property(e => e.EntryTime).HasMaxLength(30);

            entity.HasOne(d => d.CancelledUser).WithMany(p => p.FeeStructureCancelledUsers)
                .HasForeignKey(d => d.CancelledUserId)
                .HasConstraintName("FK__FeeStruct__Cance__68487DD7");

            entity.HasOne(d => d.CidNavigation).WithMany(p => p.FeeStructures)
                .HasForeignKey(d => d.Cid)
                .HasConstraintName("FK__FeeStructur__Cid__656C112C");

            entity.HasOne(d => d.EntryUser).WithMany(p => p.FeeStructureEntryUsers)
                .HasForeignKey(d => d.EntryUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FeeStruct__Entry__6754599E");

            entity.HasOne(d => d.FidNavigation).WithMany(p => p.FeeStructures)
                .HasForeignKey(d => d.Fid)
                .HasConstraintName("FK__FeeStructur__Fid__66603565");
        });

        modelBuilder.Entity<FeeStructureView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("FeeStructureView");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CancelledBy).HasMaxLength(20);
            entity.Property(e => e.CancelledDate).HasColumnType("date");
            entity.Property(e => e.Cname)
                .HasMaxLength(20)
                .HasColumnName("CName");
            entity.Property(e => e.DueDate).HasColumnType("date");
            entity.Property(e => e.EntryBy).HasMaxLength(20);
            entity.Property(e => e.EntryTime).HasMaxLength(30);
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<PrograInfoView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("PrograInfoView");

            entity.Property(e => e.CancelledBy).HasMaxLength(20);
            entity.Property(e => e.CancelledDate).HasColumnType("date");
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.EndTime).HasMaxLength(20);
            entity.Property(e => e.EntryUser).HasMaxLength(20);
            entity.Property(e => e.Pdescription).HasColumnName("PDescription");
            entity.Property(e => e.Pid)
                .ValueGeneratedOnAdd()
                .HasColumnName("PID");
            entity.Property(e => e.Pname)
                .HasMaxLength(100)
                .HasColumnName("PName");
            entity.Property(e => e.StartDate).HasColumnType("date");
            entity.Property(e => e.StartTime).HasMaxLength(20);
        });

        modelBuilder.Entity<ProgramInfo>(entity =>
        {
            entity.HasKey(e => e.Pid).HasName("PK__ProgramI__C57755206D4CDA7C");

            entity.ToTable("ProgramInfo");

            entity.Property(e => e.Pid).HasColumnName("PID");
            entity.Property(e => e.CancelledDate).HasColumnType("date");
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.EndTime).HasMaxLength(20);
            entity.Property(e => e.EntryDate).HasColumnType("date");
            entity.Property(e => e.Pdescription).HasColumnName("PDescription");
            entity.Property(e => e.Pname)
                .HasMaxLength(100)
                .HasColumnName("PName");
            entity.Property(e => e.StartDate).HasColumnType("date");
            entity.Property(e => e.StartTime).HasMaxLength(20);

            entity.HasOne(d => d.CancelledUser).WithMany(p => p.ProgramInfoCancelledUsers)
                .HasForeignKey(d => d.CancelledUserId)
                .HasConstraintName("FK__ProgramIn__Cance__40F9A68C");

            entity.HasOne(d => d.User).WithMany(p => p.ProgramInfoUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProgramIn__UserI__40058253");
        });

        modelBuilder.Entity<Receipt>(entity =>
        {
            entity.HasKey(e => e.Rid).HasName("PK__Receipt__CAFF40D2FED50BE1");

            entity.ToTable("Receipt");

            entity.Property(e => e.Rid).HasColumnName("RId");
            entity.Property(e => e.CancelledDate).HasColumnType("date");
            entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ReceiptDate).HasColumnType("date");
            entity.Property(e => e.ReceiptTime).HasMaxLength(20);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.CancelledUser).WithMany(p => p.ReceiptCancelledUsers)
                .HasForeignKey(d => d.CancelledUserId)
                .HasConstraintName("FK__Receipt__Cancell__72C60C4A");

            entity.HasOne(d => d.EntryUser).WithMany(p => p.ReceiptEntryUsers)
                .HasForeignKey(d => d.EntryUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Receipt__EntryUs__71D1E811");

            entity.HasOne(d => d.Std).WithMany(p => p.ReceiptStds)
                .HasForeignKey(d => d.StdId)
                .HasConstraintName("FK__Receipt__StdId__70DDC3D8");
        });

        modelBuilder.Entity<ReceiptDetail>(entity =>
        {
            entity.HasKey(e => e.DetailId).HasName("PK__ReceiptD__135C316DA52E0F7E");

            entity.ToTable("ReceiptDetail");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Rid).HasColumnName("RId");

            entity.HasOne(d => d.RidNavigation).WithMany(p => p.ReceiptDetails)
                .HasForeignKey(d => d.Rid)
                .HasConstraintName("FK__ReceiptDeta__RId__75A278F5");

            entity.HasOne(d => d.Sheet).WithMany(p => p.ReceiptDetails)
                .HasForeignKey(d => d.SheetId)
                .HasConstraintName("FK__ReceiptDe__Sheet__76969D2E");
        });

        modelBuilder.Entity<ReceiptPrint>(entity =>
        {
            entity.HasKey(e => e.PrintId).HasName("PK__ReceiptP__26C7BA7DB610C170");

            entity.ToTable("ReceiptPrint");

            entity.Property(e => e.PrintDate).HasColumnType("date");
            entity.Property(e => e.PrintTime).HasMaxLength(30);
            entity.Property(e => e.Rid).HasColumnName("RId");

            entity.HasOne(d => d.PrintUser).WithMany(p => p.ReceiptPrints)
                .HasForeignKey(d => d.PrintUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReceiptPr__Print__3D2915A8");

            entity.HasOne(d => d.RidNavigation).WithMany(p => p.ReceiptPrints)
                .HasForeignKey(d => d.Rid)
                .HasConstraintName("FK__ReceiptPrin__RId__3C34F16F");
        });

        modelBuilder.Entity<ReceiptView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ReceiptView");

            entity.Property(e => e.BusFee).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CancelledBy).HasMaxLength(20);
            entity.Property(e => e.CancelledDate).HasColumnType("date");
            entity.Property(e => e.Cname).HasMaxLength(20);
            entity.Property(e => e.DestinationAddress).HasMaxLength(1);
            entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.EntryBy).HasMaxLength(20);
            entity.Property(e => e.FullName).HasMaxLength(20);
            entity.Property(e => e.Phone).HasMaxLength(10);
            entity.Property(e => e.ReceiptDate).HasColumnType("date");
            entity.Property(e => e.ReceiptTime).HasMaxLength(20);
            entity.Property(e => e.Rid).HasColumnName("RId");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserAddress).HasMaxLength(30);
            entity.Property(e => e.UserEmail).HasMaxLength(30);
        });

        modelBuilder.Entity<Reception>(entity =>
        {
            entity.HasKey(e => e.Rid).HasName("PK__Receptio__CAFF40D2C55E98DB");

            entity.ToTable("Reception");

            entity.Property(e => e.Rid).HasColumnName("RId");
            entity.Property(e => e.CancelledDate).HasColumnType("date");
            entity.Property(e => e.EntryDate).HasColumnType("date");
            entity.Property(e => e.EntryTime).HasMaxLength(20);
            entity.Property(e => e.FiscalYear).HasMaxLength(20);
            entity.Property(e => e.PersonAddress).HasMaxLength(30);
            entity.Property(e => e.PersonName).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(10);
            entity.Property(e => e.ReceptionStatus).HasMaxLength(1000);

            entity.HasOne(d => d.CancelledUser).WithMany(p => p.ReceptionCancelledUsers)
                .HasForeignKey(d => d.CancelledUserId)
                .HasConstraintName("FK__Reception__Cance__489AC854");

            entity.HasOne(d => d.User).WithMany(p => p.ReceptionUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reception__UserI__47A6A41B");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StdId).HasName("PK__Student__55DCAE1FB3248D94");

            entity.ToTable("Student");

            entity.Property(e => e.Bid).HasColumnName("BId");

            entity.HasOne(d => d.BidNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.Bid)
                .HasConstraintName("FK__Student__BId__5AEE82B9");

            entity.HasOne(d => d.CidNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.Cid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Student__Cid__59FA5E80");

            entity.HasOne(d => d.User).WithMany(p => p.Students)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Student__UserId__5BE2A6F2");
        });

        modelBuilder.Entity<StudentView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("StudentView");

            entity.Property(e => e.Bid).HasColumnName("BId");
            entity.Property(e => e.BusFee).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Cname).HasMaxLength(20);
            entity.Property(e => e.DestinationAddress).HasMaxLength(1);
            entity.Property(e => e.FullName).HasMaxLength(20);
            entity.Property(e => e.Phone).HasMaxLength(10);
            entity.Property(e => e.UserAddress).HasMaxLength(30);
            entity.Property(e => e.UserEmail).HasMaxLength(30);
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.Tid).HasName("PK__Teacher__C451DB31AC9EE311");

            entity.ToTable("Teacher");

            entity.Property(e => e.Tpost)
                .HasMaxLength(20)
                .HasColumnName("TPost");

            entity.HasOne(d => d.User).WithMany(p => p.Teachers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Teacher__UserId__5EBF139D");
        });

        modelBuilder.Entity<UploadFile>(entity =>
        {
            entity.HasKey(e => e.UploadId).HasName("PK__UploadFi__6D16C84D97AE326A");

            entity.ToTable("UploadFile");

            entity.Property(e => e.DocFile).HasColumnName("docFile");

            entity.HasOne(d => d.Doc).WithMany(p => p.UploadFiles)
                .HasForeignKey(d => d.DocId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UploadFil__DocId__5441852A");

            entity.HasOne(d => d.Type).WithMany(p => p.UploadFiles)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UploadFil__TypeI__5535A963");
        });

        modelBuilder.Entity<UploadFileView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("UploadFileView");

            entity.Property(e => e.DocFile).HasColumnName("docFile");
            entity.Property(e => e.DocumetCat).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(20);
            entity.Property(e => e.Phone).HasMaxLength(10);
            entity.Property(e => e.UserAddress).HasMaxLength(30);
            entity.Property(e => e.UserEmail).HasMaxLength(30);
        });

        modelBuilder.Entity<UserList>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserList__1788CC4CCE4727DE");

            entity.ToTable("UserList");

            entity.Property(e => e.FullName).HasMaxLength(20);
            entity.Property(e => e.LoginStatus)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Phone).HasMaxLength(10);
            entity.Property(e => e.Upassword)
                .HasMaxLength(20)
                .HasColumnName("UPassword");
            entity.Property(e => e.UserAddress).HasMaxLength(30);
            entity.Property(e => e.UserEmail).HasMaxLength(30);
            entity.Property(e => e.UserRoleType).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
