create table tb_inspecao(
	id_pi int,
	cod_pi varchar(10) 
)

insert into tb_inspecao (id_pi, cod_pi) values(1,'1')
insert into tb_inspecao (id_pi, cod_pi) values(2,'2')
insert into tb_inspecao (id_pi, cod_pi) values(3,'3')
insert into tb_inspecao (id_pi, cod_pi) values(4,'4')
insert into tb_inspecao (id_pi, cod_pi) values(5,'5')

alter procedure sp_busca_dados_pi 
	@pi int
as

	set nocount on
	select id_pi, cod_pi
	from tb_inspecao with(nolock)
	where id_pi = @pi


go



exec sp_busca_dados_pi 1
