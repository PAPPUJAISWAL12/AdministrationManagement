--create database FeeManagementSystem
--use FeeManagementSystem

create table Class(
CId int primary key identity(1,1),
Cname nvarchar(20) not null
);

create table UserList(
UserId int primary key identity(1,1),
UserEmail nvarchar(30) not null,
UPassword nvarchar(20) not null,
FullName nvarchar(20) not null,
Phone nvarchar(10) not null,
UserAddress Nvarchar(30) not null,
LoginStatus bit default(1) not null,
UserRoleType Nvarchar(30) not null
);

Insert into UserList(UserEmail,UPassword,FullName,Phone,UserAddress,UserRoleType) Values('Jaiswalpappu@gmail.com','pappu@123','Pappu Jaiswal','9815310274','Itahari','Admin')

create table DocumentType(
TypeId int primary key identity(1,1) Not null,
DocumetCat Nvarchar(100) not null
);

create table Document(
DocId int primary key identity(1,1) not null,
UserId int not null foreign key references UserList(UserId)
);
create table UploadFile(
UploadId int primary key identity(1,1) not null,
DocId int not null foreign key references Document(DocId),
TypeId int not null foreign key references DocumentType(TypeId),
docFile nvarchar(max)  null
);

create View UploadFileView as

select UploadId,UploadFile.DocId,UserId,UploadFile.TypeId,docFile,DocumetCat,FullName,UserAddress,Phone,UserEmail from UploadFile join DocumentView on UploadFile.DocId=DocumentView.DocId join DocumentType on UploadFile.TypeId=DocumentType.TypeId


/*
create View DocumentView as
select DocId,Document.UserId,FullName,UserAddress,Phone,UserEmail from Document Join UserList on Document.UserId=UserList.UserId
*/
Create Table BusInfo(
	BId int primary key identity(1,1),	
	DestinationAddress Nvarchar(40) not null,
	BusFee decimal(18,2) not null
 );
 --alter table BusInfo alter Column DestinationAddress Nvarchar(40)
insert into BusInfo(DestinationAddress,BusFee) Values('ith','2000')
create table Student(
StdId int primary key identity(1,1),
Cid int not null foreign key references Class(CId) ,
 BId int foreign key references BusInfo(BId),
UserId int not null foreign key references UserList(UserId),
RollNo smallint not null
);

create view StudentView as
select StdId,Student.Cid,Student.BId,DestinationAddress,BusFee,Student.UserId,FullName,Cname,UserEmail,UserAddress,Phone from Student join class on Student.Cid=class.CId join UserList
on Student.UserId=UserList.UserId join BusInfo on Student.BId = BusInfo.BId

Create table Teacher(
 Tid int primary key identity(1,1) not null,
 UserId int not null foreign key references UserList(UserId) ,
 TPost nvarchar(20) not null
 );

 Create table FeeHeader(
 FId int primary key identity(1,1) not null,
 Title Nvarchar(100) not null,
 EntryUserId int not null foreign key references UserList(UserId),
 CancelledDate date null,
 CancelledUserId int null foreign key references UserList(UserId),
 ReasonForCancelled Nvarchar(max) null
 );
 Create View FeeHeaderView as 
 select FId,Title,EntryUserId,(select FullName from UserList where UserId=FeeHeader.EntryUserId) as EntryBy,CancelledDate,CancelledUserId,(select FullName from UserList where UserId=FeeHeader.CancelledUserId) as CancelledBy,ReasonForCancelled from FeeHeader

 Create table FeeStructure(
 FsId int primary key identity(1,1),
 Cid int foreign key references Class(Cid),
 Fid int foreign key references FeeHeader(FId),
 Amount decimal(18,2) not null,
 DueDate date not null,
 EntryUserId int not null foreign key references UserList(UserId),
 EntryTime Nvarchar(30) not null,
 CancelledDate date null,
 CancelledUserId int null foreign key references UserList(UserId),
 ReasonForCancelled Nvarchar(max) null
 );
 Create View FeeStructureView as
 select FsId,FeeStructure.Cid,CName, FeeStructure.Fid,Title, Amount,DueDate,FeeStructure.EntryUserId,(select FullName from UserList where UserId=FeeStructure.EntryUserId) as EntryBy,EntryTime,FeeStructure.CancelledDate,FeeStructure.CancelledUserId,(select FullName from UserList where UserId=FeeStructure.CancelledUserId) as CancelledBy,
 FeeStructure.ReasonForCancelled from FeeStructure join FeeHeaderView on FeeStructure.Fid=FeeHeaderView.FId Join Class on FeeStructure.Cid=Class.CId

 create table FeeSheet
 (
 SheetId int primary key identity(1,1),
 StdId int not null foreign key references Student(StdId),
 FId int not null foreign key references FeeHeader(FId),
 Amount decimal(18,2) not null,
 DueDate date not null,
 EntryUserId int not null foreign key references UserList(UserId),
 EntryTime Nvarchar(20) not null,
 CancelledDate date null,
 CancelledUserId int null foreign key references UserList(UserId),
 ReasonForCancelled Nvarchar(max) null ,
 FeeSheetStatus Nvarchar(1000) null
 );



 Drop View FeeSheetView
/* create View FeeSheetView as
 select SheetId,FeeSheet.StdId,FeeSheet.FId,Title,DestinationAddress,BusFee,FullName,Cname,Cid,Amount,DueDate,FeeSheet.EntryUserId,(select FullName from UserList where UserId=FeeSheet.EntryUserId)as EntryBy,FeeSheet.EntryTime,FeeSheet.CancelledDate,FeeSheet.CancelledUserId,
 (select FullName from UserList where UserId=FeeSheet.CancelledUserId) as CancelledBy,
 FeeSheet.ReasonForCancelled,FeeSheetStatus from FeeSheet join FeeHeaderView on FeeSheet.FId=FeeHeaderView.FId join StudentView on FeeSheet.StdId=StudentView.StdId
 */
 create Table Receipt
 (
	RId int primary key identity(1,1),
	ReceiptDate date not null,
	StdId int foreign key references UserList(UserId),
	ReceiptTime Nvarchar(20) not null,
	TotalAmount decimal(18,2) not null,
	Discount decimal(18,2) null,
	EntryUserId int not null foreign key references UserList(UserId),	
	CancelledDate date null,
	CancelledUserId int null foreign key references UserList(UserId),
	ReasonForCancelled Nvarchar(max) null 
);
select * from Receipt
insert into Receipt(ReceiptDate,StdId,ReceiptTime,TotalAmount,Discount,EntryUserId) Values('2023/7/14',17,'7:19Am','3000','1000',22)
--drop table Receipt
--drop View ReceiptView
/*Create View ReceiptView as
select RId,ReceiptDate,Receipt.StdId,DestinationAddress,BusFee,FullName,UserAddress,Phone,UserEmail,Cname,Cid,ReceiptTime,TotalAmount,Discount,EntryUserId,(select FullName from UserList where UserId=Receipt.EntryUserId)as EntryBy,CancelledUserId,(select FullName from UserList where UserId=Receipt.CancelledUserId)as CancelledBy,CancelledDate,ReasonForCancelled from Receipt join StudentView on Receipt.StdId=StudentView.StdId*/

 create Table ReceiptDetail(
   DetailId int primary key identity(1,1),
   RId int foreign key references Receipt(RId),
   SheetId int foreign key references FeeSheet(SheetId),
   Amount decimal(18,2) not null
 );
 --insert into ReceiptDetail (RId,SheetId,Amount) Values(5,1,'2000')
 create View ReceiptDetailView as
 select FeeSheetView.Title,FeeSheetView.Cname,FeeSheetView.EntryUserId as sheetEntryUserId, FeeSheetView.EntryBy as sheetEntryUser,FeeSheetView.Cid,FeeSheetStatus,FeeSheetView.CancelledDate as SheetCanceledDate,FeeSheetView.FullName,FeeSheetView.StdId,FeeSheetView.SheetId,FeeSheetView.BusFee,FeeSheetView.Amount,FeeSheetView.CancelledUserId as feesheetcancelled,FeeSheetView.CancelledBy as FeeSheetCancelledUser,FeeSheetView.DestinationAddress,DetailId,ReceiptDetail.RId,ReceiptDetail.SheetId as ReceiptFeeSheetId,Discount,TotalAmount,ReceiptTime,ReceiptView.EntryBy
as ReceiptEntryUser,(select COUNT(*) from ReceiptPrint where RId=ReceiptView.RId)as PrintCount, ReceiptDate,ReceiptView.CancelledBy,ReceiptView.ReasonForCancelled,ReceiptView.CancelledDate,ReceiptView.CancelledUserId from FeeSheetView left Join ReceiptDetail on ReceiptDetail.SheetId=FeeSheetView.SheetId left Join ReceiptView on ReceiptDetail.RId=ReceiptView.RId

 create table ReceiptPrint(
 PrintId int primary key identity(1,1),
  RId int foreign key references Receipt(RId),
 PrintTime Nvarchar(30) not null,
 PrintDate Date not null,
 PrintUserId int not null foreign key references UserList(UserId)
 );
 


 --drop table ReceiptPrint


 Create table Reception (
 RId int primary key  identity (1,1) not null,
 PersonName nvarchar(50) not null,
 EntryDate date not null,
 EntryTime nvarchar(20) not null,
 Purpose nvarchar(max) not null,
 PersonAddress nvarchar(30) not null,
 Phone nvarchar(10)not null,
 UserId int foreign key references UserList(UserId) not null, 
 CancelledDate date  null,
 CancelledUserId int  foreign key references UserList(UserId),
 FiscalYear nvarchar(20) not null,
 ResonForCancell  Nvarchar(max) null ,
 ReceptionStatus nvarchar(1000)  null
 );

 Create table ProgramInfo(
 PID int primary key identity(1,1) not null,
 PName nvarchar(100) not null,
 PDescription nvarchar(Max) not null,
 Venue nvarchar(max) not null,
 StartDate date not null,
 StartTime nvarchar(20) not null,
 EndDate date not null,
 EndTime nvarchar(20) not null,
 UserId int not null foreign key references UserList(UserId),
 EntryDate Date not null,
 CancelledUserId int null foreign key references UserList(UserId),
 CancelledDate date  null,
 ReasonForCancell Nvarchar(max)  null,
 );

 Create View PrograInfoView as
 select PID,PName,PDescription,Venue,StartDate,StartTime,EndDate,EndTime,UserId,
 (select FullName from UserList where UserId=ProgramInfo.UserId)as EntryUser,CancelledDate,CancelledUserId,(select FullName from UserList where UserId=ProgramInfo.CancelledUserId)as CancelledBy
 ,ReasonForCancell from ProgramInfo


