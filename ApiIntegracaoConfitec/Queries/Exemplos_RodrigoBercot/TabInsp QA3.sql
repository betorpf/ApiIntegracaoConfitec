use RamosDiversos
go

--Se tipo 200 e 300
-- Buscando dados da apolice anterior atraves do PI da proposta a ser emitida
select NUM_PI,NUM_PROP,NUM_APOL_ANT,cod_iden_stat_acao,* from RamosDiversos.DBO.TAB_CTRL_EMIS where num_pi = 2020012789 -- PI recebido como entrada   nosso_numero--02011146886996308707
select nosso_numero from RamosDiversos.DBO.TAB_CTRL_EMIS where num_apol = 1800272570 and num_endo = 0-- Apolice anterior
SELECT * FROM RamosDiversos..Tab_Ped_Num_Copia WHERE NOSSO_NUMERO_NEW = '01930439973450694539' -- Nosso_numero da apolice anterior
SELECT Num_Insp,* FROM RamosDiversos..Tab_Trans_Insp WHERE NOSSO_NUMERO = '01930438336582851658' -- NOSSO_NUMERO_Old da tabela Tab_Ped_Num_Copia
select * from Inspecao..tab_insp where num_insp = 537991 -- Inspeção da apolice anterior



-- Buscando dados da proposta a ser emitida
select NUM_PI,NUM_PROP,NUM_APOL_ANT,num_apol,dat_emis,cod_iden_stat_acao,* from SPX21250PSQLNEO.YAS_ND.YAS.TAB_CTRL_EMIS where NUM_PI = 2020012789
select * from SPX21250PSQLNEO.YAS_ND.YAS.TAB_PED_LOC where num_ped = 523768




--Comparar os dados da TAB_PED_LOC(Proposta a ser emitida) com a tab_insp (Apolice anterior)
--caso a apolice anterior não tenha inspeção gerada, gerar um nova.
--caso algum dado divergente, gerar um nova.
--se estiver ok e dentro do prazo não gerar um nova e reaproveitar a inspeção da apolice.

/*
Validar Dat_cad
Para o Residencial são 5 anos e demais 3 anos
*/

--Comparar CEP, Número do local de Risco, CFP-CNPJ, Classificação e Complemento
select * from Inspecao..tab_insp where num_insp = 537991 -- Inspeção da apolice anterior
select * from SPX21250PSQLNEO.YAS_ND.YAS.TAB_PED_LOC where num_ped = 523768

--Comparar Coberturas
select * /*Cod_Cobert	Lmg_Cobert*/ from inspecao..Tab_Cobert_Insp where Num_Insp = 537991
select * /*COD_COBERT	VAL_IS*/ from SPX21250PSQLNEO.YAS_ND.YAS.TAB_PED_COBERT where num_ped = 523768


--Validar se a apolice teve sinistro
/*(Aguardando validação dessa query*/
SELECT * FROM SPX21250PSQLNEO.YAS_ND.YAS.TAB_SAP_SIN_PAG WHERE NUM_APOL =1800272570
SELECT * FROM SPX21250PSQLNEO.YAS_ND.YAS.TAB_SIN_PAG WHERE NUM_APOL = 1800272570


/**
SE ESTIVER NO PRAZO E FOR TUDO IGUAL, VAI REAPROVEITAR E NÃO VAI MANDAR PARA A CONFITEC
*/

/*
--Tipo do retorno da Inspeção - Reaproveitamento
- 1 - Sucesso (Enviado para a Confitec)
- 2 - Sucesso (Reaproveitamento)
- 100 - Erro (banco de dados)
- 101 - erro de confitec
*/



/**
SE NÃO ESTIVER NO PRAZO OU NÃO FOR TUDO IGUAL, VAI MANDAR PARA A CONFITEC
*/

/*
Verifica se tem Siscota, se tem acessar tabela SISCOTA.SISCOTA.TAB_COTACAO e obter o campo CODINSPEC 
e se for igual a ‘2’ não gravar inspeção.
   - Inclui TAB_INSP
   - Inclui TAB_DISTRINSP
   - Inclui TAB_COBERT_INSP
*/

/*
Se o COD_RAMO = 114 proceder:
Obter o COD_ESTIP da tabela ramosdiversos..TAB_PED com o NUM_PI e verificar se o COD_ESTIP é diferente de 11 e 13 (Condomínio Amplo), se sim desprezar.
*/





--Recuperar nosso numero
SELECT NOSSO_NUMERO_OLD FROM RamosDiversos..Tab_Ctrl_Emis a (nolock)
inner join RamosDiversos..Tab_Ped_Num_Copia b (nolock)
on b.NOSSO_NUMERO_NEW = a.nosso_numero
WHERE a.num_pi = 2020012789

--112/113/114
update RamosDiversos..Tab_Trans_Insp SET NUM_INSP = "numeroInspecao", COD_STATUS_I = 2 WHERE nosso_numero = NOSSO_NUMERO_ORIG
update INSPECAO..Tab_Insp SET NUM_INSP = "numeroInspecao" WHERE nosso_numero = NOSSO_NUMERO_ORIG
--710/620/300
update RamosDiversos..Tab_Trans_Insp_Equipamentos SET NUM_INSP = "numeroInspecao", COD_STATUS_I = 2 WHERE nosso_numero = NOSSO_NUMERO_ORIG
update INSPECAO..Tab_Insp_Equip_Compl SET NUM_INSP = "numeroInspecao" WHERE nosso_numero = NOSSO_NUMERO_ORIG

/*Fazer os inserts
	TAB_INSP
	TAB_DISTRINSP
	TAB_COBERT_INSP
*/


