USE RamosDiversos
GO

IF EXISTS (
		SELECT *
		FROM sys.objects WITH (NOLOCK)
		WHERE name = 'sp_brq_validar_criar_inspecao_teste'
		)
	DROP PROCEDURE sp_brq_validar_criar_inspecao_teste
GO

CREATE PROCEDURE sp_brq_validar_criar_inspecao_teste 
	@NUM_PI DECIMAL(10, 0),  
	@NUM_ITEM DECIMAL(5, 0), 
	@TIP_EMISSAO DECIMAL(4, 0), 
	@COD_RAMO DECIMAL(3, 0),
	@CRIAR_NOVA BIT OUTPUT,
	@MENSAGEM_RETORNO VARCHAR(200) OUTPUT
AS
BEGIN

	BEGIN /*Inicializando Outputs*/
		SET @CRIAR_NOVA = 0
		SET @MENSAGEM_RETORNO = ''
	END

	--BEGIN /*TESTES*/
	--	SET @NUM_PI = 2020012789
	--	SET @NUM_ITEM = 1
	--	SET @TIP_EMISSAO = 100
	--END

	BEGIN /*Variáveis*/
		DECLARE @NOSSO_NUMERO	VARCHAR(20)
		DECLARE @NOSSO_NUMERO_OLD VARCHAR(20)
		DECLARE @NUM_APOL_ANT	BIGINT
		DECLARE @NUM_INSP_ANTERIOR NUMERIC(7,0)
		DECLARE @DAT_CAD_INSP_ANTERIOR DATETIME
		DECLARE @VALIDADE_ANOS SMALLINT = 3
		DECLARE @NUM_PROP NUMERIC(6,0)
	END

	BEGIN /*DROP TEMP TABLES*/
		IF OBJECT_ID(N'tempdb..#TMP_COMPARAR_TAB_INSP') IS NOT NULL
			DROP TABLE tempdb..#TMP_COMPARAR_TAB_INSP

		IF OBJECT_ID(N'tempdb..#TMP_COMPARAR_TAB_PED_LOC') IS NOT NULL
			DROP TABLE tempdb..#TMP_COMPARAR_TAB_PED_LOC
			
		IF OBJECT_ID(N'tempdb..#TMP_COMPARAR_Tab_Cobert_Insp') IS NOT NULL
			DROP TABLE tempdb..#TMP_COMPARAR_Tab_Cobert_Insp
			
		IF OBJECT_ID(N'tempdb..#TMP_COMPARAR_TAB_PED_COBERT') IS NOT NULL
			DROP TABLE tempdb..#TMP_COMPARAR_TAB_PED_COBERT

	END

	BEGIN /*Criação da tabela de resultado*/
		CREATE TABLE #TMP_COMPARAR_TAB_INSP (
			 num_insp NUMERIC(7,0)
			,num_cnpj_cpf BIGINT
			,num_cep BIGINT
			,num_logr INT
			,cod_clasf CHAR(4)
			,cod_compl_clasf CHAR(2)
		)

		CREATE TABLE #TMP_COMPARAR_TAB_PED_LOC (
			 num_ped NUMERIC(6,0)
			,num_cnpj_cpf BIGINT
			,num_cep BIGINT
			,num_logr INT
			,cod_clasf CHAR(4)
			,cod_compl_clasf CHAR(2)
		)

		CREATE TABLE #TMP_COMPARAR_Tab_Cobert_Insp (
			 num_insp NUMERIC(7,0)
			,Cod_Cobert	NUMERIC(4,0)  
			,Lmg_Cobert	DECIMAL(15,2)
		)
		CREATE TABLE #TMP_COMPARAR_TAB_PED_COBERT (
			 num_ped NUMERIC(6,0)
			,COD_COBERT	NUMERIC(4,0)  
			,VAL_IS	DECIMAL(15,2)
		)
	END

	--Se tipo 200 e 300
	-- Buscando dados da apolice anterior atraves do PI da proposta a ser emitida
	select 
		@NUM_APOL_ANT = NUM_APOL_ANT
		--,@NOSSO_NUMERO = nosso_numero
		--,NUM_PI,NUM_PROP,NUM_APOL_ANT,cod_iden_stat_acao
	from RamosDiversos.DBO.TAB_CTRL_EMIS WITH(NOLOCK)
	where num_pi = @NUM_PI
	-- PI recebido como entrada   nosso_numero--02011146886996308707

	select 
		@NOSSO_NUMERO = nosso_numero 
	from RamosDiversos.DBO.TAB_CTRL_EMIS WITH(NOLOCK)
	where num_apol = @NUM_APOL_ANT 
	AND num_endo = 0 -- Apolice anterior

	SELECT 
		@NOSSO_NUMERO_OLD = NOSSO_NUMERO_OLD 
	FROM RamosDiversos..Tab_Ped_Num_Copia WITH(NOLOCK)
	WHERE NOSSO_NUMERO_NEW = @NOSSO_NUMERO -- Nosso_numero da apolice anterior

	SELECT 
		@NUM_INSP_ANTERIOR = Num_Insp 
	FROM RamosDiversos..Tab_Trans_Insp WITH(NOLOCK)
	WHERE NOSSO_NUMERO = @NOSSO_NUMERO_OLD -- NOSSO_NUMERO_Old da tabela Tab_Ped_Num_Copia

	SELECT @DAT_CAD_INSP_ANTERIOR = CONVERT(datetime, convert(char(10),dat_cad,120))
	FROM Inspecao..tab_insp WITH(NOLOCK)
	WHERE num_insp = @NUM_INSP_ANTERIOR --Inspeção da apolice anterior
	

	--Para o Residencial (COD_RAMO = 113) são 5 anos e demais 3 anos
	IF(@COD_RAMO = 113) 
		SET @VALIDADE_ANOS = 5

	--VALIDAR DATA DE VALIDADE, SE FOR MENOR UTILIZAR A MESMA INSPEÇÃO
	IF(DATEDIFF(YEAR, @DAT_CAD_INSP_ANTERIOR, GETDATE()) < @VALIDADE_ANOS)
	BEGIN
		SET @CRIAR_NOVA = 0
		SET @MENSAGEM_RETORNO = 'Utilizar a inspeção anterior (' + CONVERT(VARCHAR, @NUM_INSP_ANTERIOR) + ') pois foi criada em ' + convert(varchar(10), @DAT_CAD_INSP_ANTERIOR, 103) + ' e está dentro da validade de ' + convert(varchar, @VALIDADE_ANOS) + ' anos.'
		RETURN
	END
	
	BEGIN /*Validar se a apolice teve sinistro
			Se tiver sinistro já manda pra confitech direto*/
	
		IF EXISTS(SELECT 1 FROM SPX21250PSQLNEO.YAS_ND.YAS.TAB_SAP_SIN_PAG WHERE NUM_APOL = @NUM_APOL_ANT)
		BEGIN
			SET @CRIAR_NOVA = 1
			SET @MENSAGEM_RETORNO = 'Há registros de Sinistro na Tabela SPX21250PSQLNEO.YAS_ND.YAS.TAB_SAP_SIN_PAG onde NUM_APOL = (' + CONVERT(VARCHAR, @NUM_APOL_ANT) + ').'
			--PRINT @MENSAGEM_RETORNO
			RETURN
		END
		
		IF EXISTS(SELECT 1 FROM SPX21250PSQLNEO.YAS_ND.YAS.TAB_SIN_PAG WHERE NUM_APOL = @NUM_APOL_ANT)
		BEGIN
			SET @CRIAR_NOVA = 1
			SET @MENSAGEM_RETORNO = 'Há registros de Sinistro na Tabela SPX21250PSQLNEO.YAS_ND.YAS.TAB_SIN_PAG onde NUM_APOL = (' + CONVERT(VARCHAR, @NUM_APOL_ANT) + ').'
			--PRINT @MENSAGEM_RETORNO
			RETURN
		END
	END
	
	SELECT @NUM_PROP = NUM_PROP
	FROM SPX21250PSQLNEO.YAS_ND.YAS.TAB_CTRL_EMIS WITH(NOLOCK)
	WHERE NUM_PI = @NUM_PI

	--select * from SPX21250PSQLNEO.YAS_ND.YAS.TAB_PED_LOC where num_ped = 523768

	BEGIN /*COMPARAR AS TABELAS INSPECAO..TAB_INSP E YAS_ND.YAS.TAB_PED_LOC */
		INSERT INTO #TMP_COMPARAR_TAB_INSP 
			  (num_insp, num_cnpj_cpf, num_cep, num_Logr, cod_clasf, cod_compl_clasf)
		SELECT num_insp, num_cnpj_cpf, num_cep, Num_Logr, cod_clasf, cod_compl_clasf  
		FROM Inspecao..tab_insp WITH(NOLOCK)
		WHERE num_insp = @NUM_INSP_ANTERIOR 
	
		INSERT INTO #TMP_COMPARAR_TAB_PED_LOC 
			  (num_ped, num_cnpj_cpf,	num_cep,		num_Logr,	cod_clasf,	cod_compl_clasf) --falta cpf/num_logr
		SELECT L.num_ped, 0,				L.NUM_CEP_RISCO,	I.NUM_LOGR_RISCO,			L.COD_CLASF,	L.COD_COMPL_CLASF
		FROM SPX21250PSQLNEO.YAS_ND.YAS.TAB_PED_LOC L WITH(NOLOCK) 
			INNER JOIN SPX21250PSQLNEO.YAS_ND.YAS.TAB_PED_INSP I WITH(NOLOCK)
				ON  L.NUM_PED	= I.NUM_PED	
				AND L.NUM_ITEM 	= I.NUM_ITEM 
		WHERE L.num_ped = @NUM_PROP 
		AND I.NUM_PI = @NUM_PI

		IF(NOT EXISTS (
			SELECT 1
			FROM #TMP_COMPARAR_TAB_INSP I
				INNER JOIN #TMP_COMPARAR_TAB_PED_LOC P
					ON  I.num_cnpj_cpf		= P.num_cnpj_cpf
					AND I.num_cep			= P.num_cep
					AND I.num_Logr			= P.num_Logr
					AND I.cod_clasf			= P.cod_clasf
					AND I.cod_compl_clasf	= P.cod_compl_clasf
			))
		BEGIN 
			SET @CRIAR_NOVA = 1
			SET @MENSAGEM_RETORNO = 'Há diferenças entre as tabelas Inspecao..Tab_Insp (num_insp = ' + CONVERT(VARCHAR, @NUM_INSP_ANTERIOR) + ') e YAS_ND.YAS.TAB_PED_LOC (NUM_PED = ' + convert(varchar, @NUM_PROP) + ' - NUM_PI = ' + convert(varchar, @NUM_PI) + ') .'
			
			--SELECT * FROM #TMP_COMPARAR_TAB_INSP
			--SELECT * FROM #TMP_COMPARAR_TAB_PED_LOC
			--PRINT @MENSAGEM_RETORNO
			RETURN
		END 
	END

	BEGIN /*COMPARAR AS TABELAS INSPECAO..TAB_INSP E YAS_ND.YAS.TAB_PED_LOC */

		INSERT #TMP_COMPARAR_Tab_Cobert_Insp 
			  (num_insp, Cod_Cobert, Lmg_Cobert)
		SELECT Num_Insp, Cod_Cobert, Lmg_Cobert
		FROM inspecao..Tab_Cobert_Insp WITH(NOLOCK)
		WHERE Num_Insp = @NUM_INSP_ANTERIOR
	
		INSERT INTO #TMP_COMPARAR_TAB_PED_COBERT 
			  (num_ped, COD_COBERT, VAL_IS)
		SELECT NUM_PED, COD_COBERT, VAL_IS
		FROM SPX21250PSQLNEO.YAS_ND.YAS.TAB_PED_COBERT WITH(NOLOCK)
		WHERE num_ped = @NUM_PROP

		IF(NOT EXISTS (
			SELECT 1
			FROM #TMP_COMPARAR_Tab_Cobert_Insp C WITH(NOLOCK)
			INNER JOIN #TMP_COMPARAR_TAB_PED_COBERT P
				ON  C.Cod_Cobert = P.COD_COBERT
				AND C.Lmg_Cobert = P.VAL_IS
			))
		BEGIN 
			SET @CRIAR_NOVA = 1
			SET @MENSAGEM_RETORNO = 'Há diferenças entre as tabelas Inspecao..Tab_Cobert_Insp (' + CONVERT(VARCHAR, @NUM_INSP_ANTERIOR) + ') e YAS_ND.YAS.TAB_PED_COBERT (' + convert(varchar, @NUM_PROP) + ').'
			--PRINT @MENSAGEM_RETORNO 
			RETURN
		END 
	END

	RETURN

END