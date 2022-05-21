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
		SompoUsername VARCHAR(50)
		,SompoPassword VARCHAR(50)
		)

	INSERT INTO #temp
	VALUES (
		'SompoUsername'
		,'SompoPassword'
		)

	SELECT TOP 1 *
	FROM #temp
END
GO


