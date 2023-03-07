create database db_EmployeeHiring
use db_EmployeeHiring

create table tbl_personal_info(
member_id int not null primary key identity(1,1),
firsst_name varchar(30) not null,
last_name varchar(30) not null,
UserName varchar(30) not null,
email nvarchar(255) not null,
cnt_no nvarchar(11) not null,
h_add nvarchar(255) not null,
Pass nvarchar(50) not null,
cnfpass nvarchar(50) not null
)

drop database db_EmployeeHiring
select * from tbl_personal_info
create table tbl_education(
edu_id int not null primary key identity(1,1),
member_id int not null foreign key references tbl_personal_info(member_id),
Degree varchar(30) not null,
college varchar(300) not null,
start_year date not null,
end_year date,
mark_cgpa nvarchar(5) not null
)
insert into tbl_education values(1,'Matric','Fazaia','2016-01-01','2018-01-01','72'),(1,'Inter','Fazaia','2018-01-01','2020-01-01','69'),
							(1,'BSCS','Fazaia','2020-01-01',NULL,'3.4'),(2,'Matric','Bahria','2016-01-01','2018-01-01','69'),
							(2,'Inter','Bahria','2018-01-01','2020-01-01','74'),(2,'BSCS','Bahria','2020-01-01',NULL,'3.4')	
select * from tbl_education



create table tbl_experience(
exper_id int not null primary key identity(1,1),
member_id int not null foreign key references tbl_personal_info(member_id),
company varchar(200) not null,
Starts_date date not null,
end_date date,
experience_time nvarchar(30) not null,
position varchar(300) not null
)




insert into tbl_experience values(1,'Netsol','2021-08-01','2021-12-29','1 year','Intern'),(1,'10pearl','2022-08-01','2022-12-29','6 months','Intern'),
			(2,'Netsol','2022-01-01','2022-04-12','2','Intern'),(2,'10pearl','2022-08-01','2022-12-29','1 month','Intern')
select * from tbl_experience


create table tbl_projects(
proj_id int not null primary key identity(1,1),
member_id int not null foreign key references tbl_personal_info(member_id),
proj_name varchar(100) not null,
languages nvarchar(200) not null,
image_path varchar(max) not null
)


--create table tbl_skill(
--skill_id int not null primary key identity(1,1),
--member_id int not null foreign key references tbl_personal_info(member_id),
--skill_name varchar(100) not null
--)

--select * from tbl_skill

--insert into tbl_skill values(1,'.net developer'),(1,'web developer'),(2,'database designer')

insert into tbl_projects values(1,'Portfolio','ASP .NET,C#,HTML/CSS'),(1,'Gym website','HTML/CSS')
insert into tbl_projects values(2,'Cafe mangament system','C#'),(2,'property website','HTML/CSS, BOOTSTRAP')
select * from tbl_personal_info
--create table tbl_language(
--lang_id int primary key not null identity(1,1),
--proj_id int foreign key references tbl_projects(proj_id),
--lang_name varchar(100) not null
--)
--drop table tbl_language
--insert into tbl_language values(1,'C#'),(1,'ASP .NET'),(1,'hmtl/css'),(2,'html/css'),(3,'C#'),(4,'hmtl/css')


select * from tbl_personal_info inner join tbl_education on tbl_personal_info.member_id=tbl_education.member_id
			inner join tbl_experience on tbl_personal_info.member_id=tbl_experience.member_id
			inner join tbl_projects on tbl_personal_info.member_id=tbl_projects.member_id
			inner join tbl_skill on tbl_personal_info.member_id=tbl_skill.member_id
			where tbl_personal_info.member_id=1


select * from tbl_personal_info
select * from tbl_projects

delete from tbl_personal_info where member_id=5



--create table tbl_images(
--img_id int not null primary key identity(1,1),
--member_id int not null foreign key references tbl_personal_info(member_id),
--img_path varchar(max) not null
--)

--select * from tbl_images

create table tbl_category(
id int not null primary key identity(1,1),
member_id int not null foreign key references tbl_personal_info(member_id),
cat_name varchar(500) not null
)

select * from tbl_personal_info