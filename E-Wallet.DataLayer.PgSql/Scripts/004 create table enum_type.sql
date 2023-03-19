create table enum_direction_type
(
 id				int not null primary key,
 code			varchar(50) null,
 short_name		varchar(250) not null,
 full_name		varchar(250) not null,
 created_date	timestamp without time zone default now() not null
);

insert into enum_direction_type (id,code, short_name, full_name) 
VALUES (1,'001','Пополнение','Пополнение'),(2,'002','Расход','Расход');