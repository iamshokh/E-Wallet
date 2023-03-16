create table enum_type
(
 id				int not null primary key,
 code			varchar(50) null,
 short_name		varchar(250) not null,
 full_name		varchar(250) not null,
 created_date	timestamp without time zone default now() not null
);

insert into enum_type (id, short_name, full_name) 
VALUES (1,'Replenishment','Replenishment'),(2,'Withdrawal','Withdrawal');