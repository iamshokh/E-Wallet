create table e_wallet_transaction
(
    id						serial not null primary key,
	e_wallet_id			    int not null,
	amount		            decimal(18,2) not null,
	direction_type_id       int not null,
	state_id				int not null,             

	created_date			timestamp without time zone default now() not null,
	created_user_id			int null,
	modified_date			timestamp without time zone,
	modified_user_id		int,

	constraint fk_state_id					foreign key ( state_id )				references enum_state ( id ),
	constraint fk_e_wallet_id				foreign key ( e_wallet_id )				references e_wallet ( id ),
	constraint fk_direction_type_id		    foreign key ( direction_type_id )		references enum_direction_type ( id )
)