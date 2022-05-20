IF EXISTS (
		SELECT *
		FROM sys.objects WITH (NOLOCK)
		WHERE name = 'sp_brq_grava_dados_inspecao'
		)
	DROP PROCEDURE sp_brq_grava_dados_inspecao
GO

CREATE PROCEDURE sp_brq_grava_dados_inspecao @NUM_PI INT, @NUM_INSP NUMERIC(7, 0), @CODIGO_PARECER INT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		 codigoResultado BIT
		,mensagemRetorno VARCHAR(100)
		)

	/*VARIÁVEIS*/
	DECLARE @NOSSO_NUMERO_ORIG VARCHAR(20)
	DECLARE @L_COD_PAREC INT
	DECLARE @L_COD_STATUS INT
	DECLARE @L_COD_LIBERA INT
	DECLARE @QTDE INT
	DECLARE @NUM_ITEM NUMERIC(18, 0)
	DECLARE @L_COD_USER_DERIE CHAR(12)
	DECLARE @DATA_INSPECAO BIGINT
	-- DECLARE @NUM_APOL NUMERIC(10, 0)
	-- DECLARE @NUM_PI_ANT INT
	-- DECLARE @NUM_PROP DECIMAL(6, 0)
	-- DECLARE @NUM_LOGR_RISCO DECIMAL(8, 0)
	-- DECLARE @NUM_INSP_ANTERIOR DECIMAL(7, 0)
	-- DECLARE @VALIDADE_INSPECAO INT
	-- DECLARE @NUM_INSP INT
	-- DECLARE @NUM_COTAC VARCHAR(12)
	-- DECLARE @CONDINSPEC NUMERIC(1, 0)

	-- O processo da Confitec irá retornar o laudo automaticamente, acessando o serviço da BRQ pelo NUM_PI 

	-- inspecaoId:					Número da inspeção
	-- tipoVistoria:				Código cadastrado do tipo de inspeção
	-- numeroProposta:				Número da proposta
	-- numeroVersaoProposta:		Versão da proposta
	-- numeroOrcamento:				Número do orçamento
	-- numeroVersaoOrcamento:		Versão do orçamento
	-- numeroSinistro:				Número do sinistro
	-- status:						Atividade atual da inspeção dentro do fluxo
	-- parecerRisco:				Código cadastrado do parecer de risco, quando a inspeção está finalizada
	-- dataAtualizacao:				Data e hora da última atualização do registro
	-- logradouro:					Endereço do objeto segurado
	-- urlLaudo:					URL de consulta pública do iRisk para o laudo da inspeção
	-- codigoEmpresaGrupoSegurador:	Código cadastrado do grupo segurador
	-- dataInspecao:				Data de realização da inspeção, quando já realizada
	-- dataAgendamento:				Data de agendamento da inspeção, quando já agendada
	-- dataSolicitacaoInspecao:		Data da solicitação da inspeção
	-- dataFinalizacaoInspecao:		Data de finalização da inspeção, quando finalizada
	-- nomeEmpresaInspecao:			Nome da empresa atribuída para realizar a inspeção, se já selecionado

	-- CodigoParecer = codigo de retorno que vem do laudo

	-- LIBERAR INSPEÇÃO
	IF (@CODIGO_PARECER = 1)
	BEGIN
		SET @L_COD_PAREC = 5060
		SET @L_COD_STATUS = 4
		SET @L_COD_LIBERA = 1
	END
	-- RECUSAR INSPEÇÃO
	ELSE IF (@CODIGO_PARECER = 2)
	BEGIN
		SET @L_COD_PAREC = 5090
		SET @L_COD_STATUS = 5
		SET @L_COD_LIBERA = 2
	END
	-- CANCELAR INSPEÇÃO
	ELSE IF (@CODIGO_PARECER = 3)
	BEGIN
		SET @L_COD_PAREC = 5040
		SET @L_COD_STATUS = 7
		SET @L_COD_LIBERA = 2
	END
	END

	-- A BRQ deverá atualizar o número da inspeção (NUM_INSP) e o status (COD_STATUS_I) na tabela TAB_TRANS_INSP
	SELECT TOP 1 @NOSSO_NUMERO_ORIG = Nosso_Numero_Original FROM RamosDiversos.dbo.Tab_Ped WITH (NOLOCK) where NUM_PI = @NUM_PI
	UPDATE RamosDiversos.dbo.Tab_Trans_Insp SET NUM_INSP = @NUM_INSP, COD_STATUS_I = @L_COD_STATUS where NOSSO_NUMERO = @NOSSO_NUMERO_ORIG

	-- RECUSADO OU CANCELADO
	IF (@L_COD_LIBERA = 2)
	BEGIN
		-- Se o parecer for Recusado ou Cancelado gerar um registro na ged..tab_prop_mensagem
		-- Validar a existência de registro na tabela Tab_Stat_emis = 45
		SET @QTDE = (SELECT COUNT(*) as Qtd FROM ged.dbo.tab_stat_emis WITH (NOLOCK) WHERE num_pi = @NUM_PI AND status_acao = 45)
		-- Caso não exista realizar o insert:
		IF (@QTDE = 0)
		BEGIN
			INSERT INTO ged.dbo.tab_stat_emis VALUES (@NUM_PI, 45, 0, 0 , FORMAT(GETDATE(), 'yyyymmdd'))
		END

		-- Gravar a msg na tabela do BPM: "Inspeção " & pNumInsp & " - Verificar"
		-- Validar se o registro da msg já existe:
		SET @QTDE = (SELECT COUNT(*) as Qtd FROM ged.dbo.tab_prop_mensagem (nolock) WHERE num_pi = @NUM_PI AND item_pi = @NUM_ITEM AND cod_mensagem = 9593 AND dat_liber = 0 AND hor_liber = 0)
		-- Caso não exista realizar o insert:
		IF (@QTDE = 0)
		BEGIN
			INSERT INTO ged.dbo.tab_prop_mensagem (num_pi, item_pi, cod_mensagem, cod_tip_mensagem, motivo, dat_digit, dat_liber, hor_liber)
			VALUES (@NUM_PI, @NUM_ITEM, 9593, 14, Left('Inspeção ' + @NUM_INSP + ' - Verificar', 60), FORMAT(GETDATE(), 'yyyymmdd'), 0, 0)
		END
		
		-- Atualizar o status de inspeção para cancelado na tabela tab_stat_emis caso exista na tabela:
		-- Consulta para validação
		SET @QTDE = (SELECT * FROM ged.dbo.tab_stat_emis (NOLOCK) WHERE Num_PI = @NUM_PI AND status_resposta = 0 AND status_acao = 55)
		IF (@QTDE > 0)
		BEGIN
			UPDATE ged.dbo.tab_stat_emis SET status_resposta = 2, status_agente = 2 WHERE Num_PI = @NUM_PI AND status_acao = 55
	END
	-- LIBERADO
	ELSE IF (@L_COD_LIBERA = 1)
	BEGIN
		-- Se o parecer for inspeção liberada:
		-- Validar as msg do BPM:
		SET @QTDE = (SELECT * FROM ged.dbo.tab_prop_mensagem (NOLOCK) WHERE Num_PI = @NUM_PI AND dat_liber = 0 AND item_pi NOT IN (@NUM_ITEM , 0) AND cod_mensagem IN 
		(SELECT COD_TIPO FROM ged.dbo.TBS_TIPO (NOLOCK) WHERE COD_EVENTO = 10) AND cod_mensagem NOT IN (5222,5223,5224, 5411))

		-- Caso não exista pendencias de msg, realizar a liberação da inspeção na tabela Tab_Stat_emis:
		IF (@QTDE = 0)
		BEGIN
			UPDATE ged.dbo.tab_stat_emis SET status_resposta = 1 WHERE Num_PI = @NUM_PI AND status_resposta = 0 AND status_acao = 55
			
			-- Liberara as pendencias do BPM caso exista.
			-- Validar se existe pendência:
			SET @QTDE = (SELECT * FROM ged.dbo.tab_prop_mensagem (NOLOCK) WHERE Num_PI = @NUM_PI AND dat_liber = 0 AND item_pi in (@NUM_ITEM , 0) AND cod_mensagem in
			(SELECT COD_TIPO FROM ged.dbo.TBS_TIPO (nolock) WHERE COD_EVENTO = 10))
			-- Caso existir liberar:
			IF (@QTDE = 0)
			BEGIN
				UPDATE ged.dbo.tab_prop_mensagem SET dat_liber = FORMAT(GETDATE(), 'yyyymmdd'), hor_liber =  FORMAT(CONVERT(TIME, GETDATE()), 'hhmmss')
				Where Num_PI = @NUM_PI AND dat_liber = 0 AND item_pi in (@NUM_ITEM, 0)
				AND cod_mensagem IN (SELECT COD_TIPO FROM ged.dbo.TBS_TIPO (NOLOCK) WHERE COD_EVENTO = 10)

				-- Atualizar os dados na tabela Tab_insp:
				UPDATE Inspecao.dbo.TAB_INSP SET
				Cod_Parec_Geral = Trim(@L_COD_PAREC),
				Cod_User_Derie = SUBSTRING(@L_COD_USER_DERIE, 1, 12),
				Cod_Status = @L_COD_STATUS,
				Cod_Liber_Emis = @L_COD_LIBERA,
				Dat_Derie = FORMAT(GETDATE(), 'yyyymmdd'),
				Dat_ret_email = FORMAT(GETDATE(), 'yyyymmdd'),
				hor_ret_email = FORMAT(CONVERT(TIME, GETDATE()), 'hhmm'),
				Dat_Inspec = @DATA_INSPECAO,
				Hor_Derie = FORMAT(CONVERT(TIME, GETDATE()), 'hhmm')
				Where NUM_INSP = @NUM_INSP
			END
		END
	END
	END

	IF (@NUM_PI <> 0)
	BEGIN
		SELECT TOP 1 *
		FROM #temp
	END
END