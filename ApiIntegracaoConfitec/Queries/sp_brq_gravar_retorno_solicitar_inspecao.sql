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

	SELECT 1
END
GO


