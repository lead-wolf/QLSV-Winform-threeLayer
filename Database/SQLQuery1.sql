create database QLSV_BTB5_Winform
go

use QLSV_BTB5_Winform
go

create table Faculty
(
	FacultyID int not null,
	FacultyName nvarchar(255) not null,

	Primary key (FacultyID)
);

create table Major
(
	MajorID int primary key,
	MajorName nvarchar(255) not null,
	FacultyID int not null,

	Foreign key (FacultyID) REFERENCES Faculty (FacultyID)
);
go

--create table Major_Faculty
--(
--	FacultyID int not null,
--	MajorID int not null,

--	Primary key (FacultyID, MajorID),
--	Foreign key (FacultyID) REFERENCES Faculty (FacultyID) ON DELETE CASCADE ON UPDATE CASCADE,
--	Foreign key (MajorID) REFERENCES Major (MajorID) ON DELETE CASCADE ON UPDATE CASCADE
--); 
--go

create table Student
(
	StudentID nvarchar(10) not null,
	FullName nvarchar(255) not null,
	AverageScore float not null,
	FacultyID int null,
	MajorID int null,
	avater nvarchar(255) null,

	Primary key (StudentID),
	Foreign key (FacultyID) REFERENCES Faculty (FacultyID) ON DELETE CASCADE ON UPDATE CASCADE,
	Foreign key (MajorID) REFERENCES Major (MajorID) ON DELETE CASCADE ON UPDATE CASCADE
);
go


--insert data
insert into Faculty values
	(1, N'Công nghệ thông tin'),
	(2, N'Ngôn ngữ Anh'),
	(3, N'Quản trị kinh doanh')
go

insert into Major values 
	(1,N'Công nghệ phần mềm',1),
	(2,N'Hệ thống thông tin',1),
	(3,N'An toàn thông tin',1),
	(4,N'Tiếng Anh thương mại',2),
	(5,N'Tiếng Anh truyền thông',2)
go

insert into Student values
	('1234567890', N'Nguyễn Văn B', 7.5, 1, 1, null),
	('1234567891', N' Nguyễn Văn A', 4.5, 1, 2, null),
	('1234567892', N'Nguyễn Văn C', 10,2, null, null)
go
