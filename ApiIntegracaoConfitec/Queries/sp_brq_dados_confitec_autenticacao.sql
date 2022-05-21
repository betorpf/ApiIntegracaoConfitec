IF EXISTS (
		SELECT *
		FROM sys.objects WITH (NOLOCK)
		WHERE name = 'sp_brq_dados_confitec_autenticacao'
		)
	DROP PROCEDURE sp_brq_dados_confitec_autenticacao
GO

CREATE PROCEDURE sp_brq_dados_confitec_autenticacao
AS
BEGIN
	SET NOCOUNT ON
	
	CREATE TABLE #temp (
		 sompo_username	VARCHAR(50)
		,sompo_password	VARCHAR(50)
		)

	insert into #temp
	values(
		'sompo_username'
		,'sompo_password'
		)

	SELECT TOP 1 *
	FROM #temp
END
GO


