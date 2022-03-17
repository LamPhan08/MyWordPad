create database Users
use Users

set dateformat DMY

go

create table INFORMATION
(
	USERNAME varchar(50) primary key not null,
	USER_PASSWORD varchar(50) not null,
	PRIORITY int default 0 not null
)
--drop table INFORMATION

create table HISTORY
(
	USERNAME varchar(50) not null,
	TIMESTAMP varchar(50) not null,
	constraint PK_HISTORY primary key (USERNAME, TIMESTAMP)
)

drop table HISTORY

insert into INFORMATION values('user1', 'user1', 0)
insert into INFORMATION values('user2', 'user2', 0)
insert into INFORMATION values('user3', 'user3', 0)
insert into INFORMATION values('user4', 'user4', 0)
insert into INFORMATION values('user5', 'user5', 0)
insert into INFORMATION values('user6', 'user6', 0)
insert into INFORMATION values('admin', 'admin', 1)
--insert into INFORMATION values('user1', 'user1')
--insert into INFORMATION values('user2', 'user2')
--insert into INFORMATION values('user3', 'user3')
--insert into INFORMATION values('user4', 'user4')
--insert into INFORMATION values('user5', 'user5')
--insert into INFORMATION values('user6', 'user6')
--insert into INFORMATION values('admin', 'admin')
select * from INFORMATION

--insert into HISTORY values('a1', '1/10/2020')
select * from HISTORY



