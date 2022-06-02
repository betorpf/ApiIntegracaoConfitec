--Entradas
DECLARE @NUM_ITEM INT
DECLARE @NUM_PI INT

--Descobrir o cód ramo
select COD_RAMO
from tab_ped with(nolock)
where num_pi = 2120010069

/*Tipo Emissão: 
	100 = Seguro Novo
	200 = Renovação Sompo
	300 = Endosso
	101 = Renovação Congenere
Ramos RE:
	112 = Empresarial
	113 = Residencial
	114 = Condominio
*/
select * from tab_ped a (nolock)
inner join Tab_Ctrl_Emis b (nolock)
on b.num_pi = a.num_pi
inner join Tab_ped_loc c (nolock)
on c.num_pi = b.num_pi
inner join Tab_Trans_Insp d (nolock)
on d.NOSSO_NUMERO = c.NOSSO_NUMERO
and d.NUM_ITEM = c.NUM_ITEM
inner join TAB_PED_INSP e (nolock)
on e.NOSSO_NUMERO = d.NOSSO_NUMERO
and e.NUM_ITEM = d.NUM_ITEM
where a.num_pi = 2120010069 and c.num_item = 1

/*
Tipo Emissão: 
	100 = Seguro Novo
	200 = Renovação Sompo
	300 = Endosso
	101 = Renovação Congenere
Ramos RD:
	300
	620
	710
*/
select * from tab_ped a (nolock)
inner join Tab_Ctrl_Emis b (nolock)
on b.num_pi = a.num_pi
inner join Tab_ped_loc c (nolock)
on c.num_pi = b.num_pi
inner join Tab_Trans_Insp_Equipamentos d (nolock)
on d.NOSSO_NUMERO = c.NOSSO_NUMERO
and d.NUM_ITEM = c.NUM_ITEM
inner join TAB_PED_INSP e (nolock)
on e.NOSSO_NUMERO = d.NOSSO_NUMERO
and e.NUM_ITEM = d.NUM_ITEM
where a.num_pi = 2120010069 and c.num_item = 1



--listaCoberturaInspecao:
select * from TAB_TRANS_COBERT_INSP with(nolock)
where NOSSO_NUMERO = '02209738537470898157' --tab_ped.NOSSO_NUMERO
and NUM_ITEM = 1

--listaTelefoneContato
select * from TAB_PED_INSP_COMPL with(nolock)
where NOSSO_NUMERO = '02207755358996309133' and NUM_ITEM = 2



--listaSinistro
--Enviar somente no caso em tipo de emissão = 300 - Endosso
SELECT top 10 * FROM SPX21250PSQLNEO.YAS_ND.YAS.TAB_SAP_SIN_PAG where num_apol = 1600026060 and num_item = 1

--listaCamposVariaveis
--Relação de Protecionais da proposta
select * from TAB_PED_FAT_RISCO where NOSSO_NUMERO = '02200544192996300373' and NUM_ITEM = 1
