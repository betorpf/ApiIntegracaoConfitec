IF EXISTS (
		SELECT *
		FROM sys.objects WITH (NOLOCK)
		WHERE name = 'sp_brq_gravar_retorno_solicitar_inspecao'
		)
	DROP PROCEDURE sp_brq_gravar_retorno_solicitar_inspecao
GO

CREATE PROCEDURE sp_brq_gravar_retorno_solicitar_inspecao @numero_inspecao VARCHAR(10)
	,@data_processamento DATETIME
	,@codigo_resultado VARCHAR(10)
	,@mensagem_retorno VARCHAR(max)
	,@protocolo_abertura VARCHAR(50)
	,@lista_erros VARCHAR(max)
AS
BEGIN
	SET NOCOUNT ON

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


END
GO


