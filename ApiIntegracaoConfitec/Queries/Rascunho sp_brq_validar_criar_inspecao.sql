USE RamosDiversos
GO

		IF OBJECT_ID(N'tempdb..#TMP_COMPARAR_TAB_INSP') IS NOT NULL
			DROP TABLE tempdb..#TMP_COMPARAR_TAB_INSP

		IF OBJECT_ID(N'tempdb..#TMP_COMPARAR_TAB_PED_LOC') IS NOT NULL
			DROP TABLE tempdb..#TMP_COMPARAR_TAB_PED_LOC

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
	
	DECLARE @NUM_INSP_ANTERIOR	NUMERIC(7,0) = 537991
	DECLARE @NUM_PROP			NUMERIC(6,0) = 523768
	DECLARE @NOSSO_NUMERO		VARCHAR(20)  = '01930439973450694539'
	DECLARE @NUM_PI DECIMAL(10, 0) = 2020012789

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

		--SELECT * FROM SPX21250PSQLNEO.YAS_ND.YAS.TAB_PED_LOC L WITH(NOLOCK) WHERE num_ped = 523768--@NUM_PROP
		--SELECT * FROM SPX21250PSQLNEO.YAS_ND.YAS.TAB_PED_INSP WITH(NOLOCK) WHERE num_ped = 523768--@NUM_PROP

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
			SELECT 'Há diferenças entre as tabelas Inspecao..Tab_Insp (num_insp = ' + CONVERT(VARCHAR, @NUM_INSP_ANTERIOR) + ') e YAS_ND.YAS.TAB_PED_LOC (NUM_PED = ' + convert(varchar, @NUM_PROP) + ' - NUM_PI = ' + convert(varchar, @NUM_PI) + ') .'
			
		END 
	END

	SELECT * FROM #TMP_COMPARAR_TAB_INSP I	  WITH(NOLOCK)
	SELECT * FROM #TMP_COMPARAR_TAB_PED_LOC P WITH(NOLOCK)	



	----------------------------

	DECLARE @NUM_APOL_ANT	BIGINT = 1800272570
	SELECT * FROM SPX21250PSQLNEO.YAS_ND.YAS.TAB_SAP_SIN_PAG WHERE NUM_APOL = @NUM_APOL_ANT
	SELECT * FROM SPX21250PSQLNEO.YAS_ND.YAS.TAB_SIN_PAG WHERE NUM_APOL = @NUM_APOL_ANT

