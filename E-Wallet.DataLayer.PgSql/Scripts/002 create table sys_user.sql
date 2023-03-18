create table sys_user
(
	id						serial not null primary key,
	user_name				varchar(250) not null,
	password_hash			varchar(250) NOT NULL,
	password_salt			varchar(250) NOT NULL,
	email					varchar(250),
	phone_number			varchar(50),
 	shortname      			varchar(260) NOT NULL,
 	fullname       			varchar(500) NOT NULL,
	last_access_time		timestamp without time zone null,
	state_id				int not null,
    is_identificate         boolean not null default false,
	created_date			timestamp without time zone default now() not null,
	created_user_id			int null,
	modified_date			timestamp without time zone,
	modified_user_id		int,

	constraint fk_state_id					foreign key ( state_id )				references enum_state ( id )
);

create unique index sys_user_unique_index_user_name on sys_user (user_name);

create unique index sys_user_unique_index_phone_number on sys_user (phone_number)
  where phone_number is not null;
