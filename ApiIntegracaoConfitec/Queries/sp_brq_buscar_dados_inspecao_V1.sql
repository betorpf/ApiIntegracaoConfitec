USE RamosDiversos
GO

IF EXISTS (
		SELECT *
		FROM sys.objects WITH (NOLOCK)
		WHERE name = 'sp_brq_buscar_dados_inspecao'
		)
	DROP PROCEDURE sp_brq_buscar_dados_inspecao
GO

CREATE PROCEDURE sp_brq_buscar_dados_inspecao @NUM_PI INT
AS
BEGIN
	SET NOCOUNT ON
	SET @NUM_PI = 1800009225

	CREATE TABLE #temp (
		codigoRamo DECIMAL(3, 0)
		,codigoModalidade DECIMAL(10, 0)
		,nomeContato CHAR(40)
		,telefoneNumeroContato CHAR(14)
		,codigoCorretorPrincipal BIGINT
		,numeroProposta VARCHAR(6)
		,numeroObjetoSegurado VARCHAR(130)
		,numeroApolice DECIMAL(10, 0)
		,numeroEndosso DECIMAL(6, 0)
		,codigoUf VARCHAR(2)
		,nomeMunicipio VARCHAR(30)
		,numeroCep VARCHAR(8)
		,nomeBairro VARCHAR(30)
		,nomeLogradouro VARCHAR(50)
		,numeroLogradouro VARCHAR(8)
		,nomeComplemento VARCHAR(20)
		,codigoTipoLogradouro VARCHAR(3)
		,codigoTipoPessoa VARCHAR(1)
		,nomeSegurado VARCHAR(60)
		,numeroCpfCnpjSegurado VARCHAR(14)
		,dataPedidoInspecao DECIMAL(8, 0)
		,observacoes VARCHAR(250)
		,descricaoObjetoSegurado VARCHAR(130)
		,codigoCobertura NUMERIC(4, 0)
		,valorLmi DECIMAL(15, 2)
		,nomeContato1 VARCHAR(50)
		,telefoneNumeroContato1 VARCHAR(50)
		,nomeContato2 VARCHAR(50)
		,telefoneNumeroContato2 VARCHAR(50)
		,numeroSinistro DECIMAL(7, 0)
		,causaGeradora VARCHAR(50)
		,dataOcorrencia VARCHAR(50)
		,valorSinistro DECIMAL(15, 2)
		,descricaoCampo VARCHAR(50)
		,conteudoCampo VARCHAR(50)
		)

	/*VARIÁVEIS*/
	DECLARE @Ind_statusInspecao INT
	DECLARE @NUM_APOL_ANT NUMERIC(10, 0)
	DECLARE @NUM_APOL NUMERIC(10, 0)
	DECLARE @NUM_PI_ANT INT
	DECLARE @NUM_PROP_ANT DECIMAL(6, 0)
	DECLARE @NUM_LOGR_RISCO DECIMAL(8, 0)
	DECLARE @NUM_INSP_ANTERIOR DECIMAL(7, 0)
	DECLARE @VALIDADE_INSPECAO INT
	DECLARE @NUM_INSP INT
	DECLARE @NUM_COTAC VARCHAR(12)
	DECLARE @CONDINSPEC NUMERIC(1, 0)
	DECLARE @COD_RAMO INT
	DECLARE @APOL_QUANT_DIFERENCAS INT
	DECLARE @COBERT_QUANT_DIFERENCAS INT

	/* A BRQ irá receber o campo chave NUM_PI 
	pesquisar a tabela ramosdiversos..TAB_PED_LOC_COMPL e 
	obter o campo Ind_statusInspecao, 
	*/
	SELECT TOP 1 @Ind_statusInspecao = Ind_statusInspecao
	FROM RamosDiversos..TAB_PED_LOC_COMPL WITH (NOLOCK)
	WHERE NUM_PI = @NUM_PI

	/*print (@Ind_statusInspecao)
	END*/
	--Ind_statusInspecao = 0
	/*
	se nesse campo estiver com ‘2’ a inspeção é obrigatória. 
	Nesse caso irá ter inspeção e a mesma não será reaproveitada da inspeção anterior.
	*/
	IF (@Ind_statusInspecao != 2)
	BEGIN
		/*
		Se a inspeção não for obrigatória, diferente de ‘2’, 	
		vai pesquisar a tabela YAS.TAB_PED_LOC com o número da apólice anterior, 
		endosso = 0 e o número do item,
		para obter os dados do local de risco
		*/
		PRINT 'Buscando num_apol_ant pelo pi ' + convert(VARCHAR, @NUM_PI)

		SELECT TOP 1 @NUM_APOL_ANT = NUM_APOL_ANT
		FROM [SPX21250PSQLNEO].YAS_ND.YAS.TAB_CTRL_EMIS WITH (NOLOCK)
		WHERE NUM_PI = @NUM_PI

		--ORDER BY
		PRINT '@NUM_APOL_ANT: ' + convert(VARCHAR, @NUM_APOL_ANT)

		SELECT TOP 1 @NUM_INSP_ANTERIOR = NUM_INSP
		FROM [SPX21250PSQLNEO].YAS_ND.YAS.TAB_PED_LOC
		WHERE NUM_APOL = @NUM_APOL_ANT
			AND NUM_ENDO = 0

		PRINT '@NUM_INSP_ANTERIOR: ' + convert(VARCHAR, @NUM_INSP_ANTERIOR)

		/*
		Pesquisar a tabela YAS.TAB_CTRL_EMIS com o número da apólice anterior e obter   
		número da proposta e o numero do PI  
		e com esses dados acessar a tabela YAS.TAB_PED_INSP 
		e obter o número do Local de Risco 
		(Lembrando que essa pesquisa é feita por item)
		*/
		SELECT TOP 1 @NUM_PI_ANT = NUM_PI
			,@NUM_PROP_ANT = NUM_PROP
		FROM RamosDiversos..TAB_CTRL_EMIS WITH (NOLOCK)
		WHERE NUM_APOL = @NUM_APOL_ANT

		PRINT '@NUM_PI_ANT: ' + convert(VARCHAR, @NUM_PI_ANT)

		SELECT TOP 1 @NUM_LOGR_RISCO = NUM_LOGR_RISCO
		FROM [SPX21250PSQLNEO].YAS_ND.YAS.TAB_PED_INSP WITH (NOLOCK)
		WHERE NUM_PI = @NUM_PI_ANT
			AND NUM_PED = @NUM_PROP_ANT

		PRINT '@NUM_LOGR_RISCO: ' + convert(VARCHAR, @NUM_LOGR_RISCO)

		/*
		Se o número da inspeção na YAS.TAB_PED_LOC for diferente de zeros, 
		*/
		IF (@NUM_INSP_ANTERIOR <> 0)
		BEGIN
			PRINT '@NUM_INSP_ANTERIOR <> 0'

			/*
			pesquisar a inspeção anterior na tabela Inspeção..TAB_INSP,  
			*/
			/*TODO: VALIDAR CAMPOS E VARIÁVEIS*/
			SELECT @NUM_INSP_ANTERIOR = num_insp
				,@cod_ramo = cod_ramo
			FROM inspecao..tab_insp
			WHERE num_apol = @NUM_APOL_ANT

			PRINT '@NUM_INSP_ANTERIOR: ' + convert(VARCHAR, @NUM_INSP_ANTERIOR)

			IF (@NUM_INSP_ANTERIOR <> 0)
			BEGIN /*e se o número da Inspeção na TAB_INSP for diferente de zeros */
				/*
				irá comparar os dados da proposta com os dados da inspeção que já existe, 
				irá comparar CEP, Número do local de Risco, CFP-CNPJ, Classificação e Complemento, 
				se os dados forem iguais, irá buscar as Coberturas na tabela  
				TAB_COBERT_INSP e comparar se as coberturas são iguais
				
				Se coberturas também forem iguais, irá verificar a validade da Inspeção:
				Para o Residencial (COD_RAMO = 113) são 5 anos e demais 3 anos

				*/
				IF OBJECT_ID(N'tempdb..#TMP_TAB_PED_LOC_ANT') IS NOT NULL
				BEGIN
					DROP TABLE tempdb..#TMP_TAB_PED_LOC_ANT
				END

				IF OBJECT_ID(N'tempdb..#TMP_TAB_INSP_ANT') IS NOT NULL
				BEGIN
					DROP TABLE tempdb..#TMP_TAB_INSP_ANT
				END

				SELECT COD_CLASF AS cod_clasf
					,DSC_COMPL_RISCO AS dsc_compl
					,NUM_CEP_RISCO AS num_cep
					,'' AS num_cnpj_cpf
					,'' AS Num_Logr
				INTO #TMP_TAB_PED_LOC_ANT
				FROM [SPX21250PSQLNEO].YAS_ND.YAS.TAB_PED_LOC
				WHERE NUM_APOL = @NUM_APOL_ANT

				SELECT num_cnpj_cpf AS num_cnpj_cpf
					,num_cep AS num_cep
					,dsc_compl AS dsc_compl
					,cod_clasf AS cod_clasf
					,Num_Logr AS Num_Logr
				INTO #TMP_TAB_INSP_ANT
				FROM inspecao..TAB_INSP
				WHERE num_insp = @NUM_INSP_ANTERIOR

				SELECT @NUM_APOL_ANT AS 'NUM_APOL_ANT'
					,*
				FROM #TMP_TAB_PED_LOC_ANT

				SELECT @NUM_INSP_ANTERIOR AS 'NUM_INSP_ANTERIOR'
					,*
				FROM #TMP_TAB_INSP_ANT

				SELECT @APOL_QUANT_DIFERENCAS = count(1)
				FROM #TMP_TAB_INSP_ANT I
				LEFT JOIN #TMP_TAB_PED_LOC_ANT P ON I.num_cnpj_cpf = P.num_cnpj_cpf
					AND I.num_cep = P.num_cep
					AND I.dsc_compl = P.dsc_compl
					AND I.cod_clasf = P.cod_clasf
					AND I.Num_Logr = P.Num_Logr
				WHERE Y.num_cnpj_cpf IS NULL
					OR I.num_cnpj_cpf IS NULL

				PRINT '@APOL_QUANT_DIFERENCAS: ' + convert(VARCHAR, @APOL_QUANT_DIFERENCAS)
			END
			ELSE IF (@NUM_INSP = 0)
			BEGIN
				/*Se o número da Inspeção na TAB_INSP for igual zeros, */
				/*
				
				irá comparar os dados da apólice atual com os dados da apólice anterior 
				e caso seja iguais irá pegar as coberturas da tabela YAS.TAB_PED_COBERT 
				e irá efetuar as comparações dos dados de  CEP, Número do local de Risco, CFP-CNPJ, Classificação e Complemento, 
				se os dados forem iguais, irá buscar as Coberturas na tabela  TAB_COBERT_INSP e comparar se as coberturas são iguais. 
				
				Se as coberturas também forem iguais, irá verificar a validade da Inspeção:
				Para o Residencial (COD_RAMO = 113) são 5 anos e demais 3 anos
			
				*/
				IF OBJECT_ID(N'tempdb..#TMP_TAB_PED_LOC') IS NOT NULL
				BEGIN
					DROP TABLE tempdb..#TMP_TAB_PED_LOC
				END

				IF OBJECT_ID(N'tempdb..#TMP_TAB_INSP') IS NOT NULL
				BEGIN
					DROP TABLE tempdb..#TMP_TAB_INSP
				END

				SELECT COD_CLASF AS cod_clasf
					,DSC_COMPL_RISCO AS dsc_compl
					,NUM_CEP_RISCO AS num_cep
					,'' AS num_cnpj_cpf
					,'' AS Num_Logr
				INTO #TMP_TAB_PED_LOC
				FROM [SPX21250PSQLNEO].YAS_ND.YAS.TAB_PED_LOC
				WHERE NUM_APOL = @NUM_APOL_ANT

				SELECT num_cnpj_cpf AS num_cnpj_cpf
					,num_cep AS num_cep
					,dsc_compl AS dsc_compl
					,cod_clasf AS cod_clasf
					,Num_Logr AS Num_Logr
				INTO #TMP_TAB_INSP
				FROM inspecao..TAB_INSP
				WHERE num_apol = @NUM_APOL_ANT

				SELECT @NUM_APOL_ANT AS 'NUM_APOL_ANT'
					,*
				FROM #TMP_TAB_PED_LOC

				SELECT @NUM_APOL_ANT AS 'NUM_APOL_ANT'
					,*
				FROM #TMP_TAB_INSP

				SELECT @APOL_QUANT_DIFERENCAS = count(1)
				FROM #TMP_TAB_INSP I
				LEFT JOIN #TMP_TAB_PED_LOC P ON I.num_cnpj_cpf = P.num_cnpj_cpf
					AND I.num_cep = P.num_cep
					AND I.dsc_compl = P.dsc_compl
					AND I.cod_clasf = P.cod_clasf
					AND I.Num_Logr = P.Num_Logr
				WHERE Y.num_cnpj_cpf IS NULL
					OR I.num_cnpj_cpf IS NULL

				PRINT '@APOL_QUANT_DIFERENCAS: ' + convert(VARCHAR, @APOL_QUANT_DIFERENCAS)
			END

			/*
			Verificar se as os campos são iguais
			
			COD_COBERT	VAL_IS
			1			3221020.00
			1103		40000.00
			1131		48000.00
			1189		60000.00

			Num_Insp	Cod_Cobert 	Lmg_Cobert	
			705214		1			3221020.00	
			705214		1103		40000.00	
			705214		1131		48000.00	
			705214		1189		60000.00

			
			*/
			IF (@APOL_QUANT_DIFERENCAS = 0)
			BEGIN
				IF OBJECT_ID(N'tempdb..#TMP_YAS_ND_TAB_PED_COBERT') IS NOT NULL
				BEGIN
					DROP TABLE tempdb..#TMP_YAS_ND_TAB_PED_COBERT
				END

				IF OBJECT_ID(N'tempdb..#TMP_INSPECAO_TAB_COBERT_INSP') IS NOT NULL
				BEGIN
					DROP TABLE tempdb..#TMP_INSPECAO_TAB_COBERT_INSP
				END

				SELECT cod_cobert
					,val_is
				INTO #TMP_YAS_ND_TAB_PED_COBERT
				FROM [SPX21250PSQLNEO].YAS_ND.YAS.TAB_PED_COBERT
				WHERE NUM_APOL = @NUM_APOL --número apólice

				SELECT @NUM_INSP = NUM_INSP
				FROM Inspecao..TAB_INSP
				WHERE NUM_APOL = @NUM_APOL

				SELECT Cod_Cobert
					,Lmg_Cobert
				INTO #TMP_INSPECAO_TAB_COBERT_INSP
				FROM Inspecao..TAB_COBERT_INSP
				WHERE NUM_INSP = @NUM_INSP

				/*
				Se as coberturas também forem iguais, irá verificar a validade da Inspeção:
				*/
				SELECT @COBERT_QUANT_DIFERENCAS = count(1)
				FROM #TMP_YAS_ND_TAB_PED_COBERT Y
				LEFT JOIN #TMP_INSPECAO_TAB_COBERT_INSP I ON Y.cod_cobert = I.Cod_Cobert
					AND Y.val_is = I.Lmg_Cobert
				WHERE Y.cod_cobert IS NULL
					OR I.Cod_Cobert IS NULL

				IF (@COBERT_QUANT_DIFERENCAS = 0) --Mesmos campos em ambas tabelas.
				BEGIN
					IF (@COD_RAMO = 113) --Para o Residencial (COD_RAMO = 113) são 5 anos
					BEGIN
						SET @VALIDADE_INSPECAO = 5
					END
					ELSE --e demais 3 anos
					BEGIN
						SET @VALIDADE_INSPECAO = 3
					END
				END
			END
		END

		/*Após irá verificar se há algum aviso de sinistro:
		Irá verificar se há apólice nas tabelas YAS.TAB_SAP_SIN_PAG ou YAS.TAB_SIN_PAG */
		SELECT *
		FROM [SPX21250PSQLNEO].YAS_ND.YAS.TAB_SIN_PAG
		WHERE NUM_APOL = @NUM_APOL

		SELECT *
		FROM [SPX21250PSQLNEO].YAS_ND.YAS.TAB_SAP_SIN_PAG
		WHERE NUM_APOL = @NUM_APOL
			/*
		Se houve sinistro, irá carregar os valores pagos para as coberturas sinistradas e verifica 
		se os valores da IS das coberturas são iguais aos valores da IS da Inspeção, 
		se forem diferentes deverá ter outra inspeção e a anterior não será reaproveitada
		*/
			-- FIM da NÃO OBRIGATÓRIA --
	END
	ELSE
	BEGIN
		/* Se inspeção for obrigatória, proceder: 
		- Verifica se tem Siscota, se tem acessar tabela SISCOTA.SISCOTA.TAB_COTACAO e obter o campo CONDINSPEC
		e se for igual a ‘2’ não gravar inspeção.*/
		SELECT TOP 1 @NUM_APOL_ANT = NUM_APOL_ANT
		FROM [SPX21250PSQLNEO].YAS_ND.YAS.TAB_CTRL_EMIS WITH (NOLOCK)
		WHERE NUM_PI = @NUM_PI

		SELECT TOP 1 @NUM_APOL = NUM_APOL
			,@NUM_INSP_ANTERIOR = NUM_INSP
		FROM [SPX21250PSQLNEO].YAS_ND.YAS.TAB_PED_LOC
		WHERE NUM_APOL = @NUM_APOL_ANT
			AND NUM_ENDO = 0

		SELECT @NUM_COTAC = NUM_COTAC
		FROM TAB_CTRL_EMIS
		WHERE NUM_APOL = @NUM_APOL

		--NUM_COTAC = 0 => NÃO TEM SISCOTA / --NUM_COTAC = 202100170372
		SELECT @CONDINSPEC = CONDINSPEC
		FROM SISCOTA.TAB_COTACAO
		WHERE CCOTACAO = @NUM_COTAC
			--CONDINSPEC = 0
			-- Se não for Siscota ou se campo CODINSPEC diferente de ‘2’, incluir inspeção:
			-- VERIFICAR COMO INSERIR E O QUE INSERIR
			/*Insert TAB_INSP
		Insert TAB_DISTRINSP
		Insert TAB_COBERT_INSP*/
			/*Se o COD_RAMO = 114 proceder:
Obter o COD_ESTIP da tabela ramosdiversos..TAB_PED com o NUM_PI e verificar se o COD_ESTIP é diferente de 11 e 13 (Condomínio Amplo), se sim desprezar.
use RamosDiversos
use RamosDiversos
select COD_ESTIP from TAB_PED where NUM_PI = 2220554189
--COD_ESTIP = 11

Caso não seja desprezado, irá incluir fixo as coberturas abaixo na tabela TAB_COBERT_INSP


USE Inspecao
INSERT INTO Tab_Cobert_Insp VALUES ("NÚMERO INSPEÇÃO", 1103, 999999, 0)
INSERT INTO Tab_Cobert_Insp VALUES ("NÚMERO INSPEÇÃO", 1107, 999999, 0)
INSERT INTO Tab_Cobert_Insp VALUES ("NÚMERO INSPEÇÃO", 1108, 999999, 0)
INSERT INTO Tab_Cobert_Insp VALUES ("NÚMERO INSPEÇÃO", 1110, 999999, 0)
INSERT INTO Tab_Cobert_Insp VALUES ("NÚMERO INSPEÇÃO", 1112, 999999, 0)

No final atualizar o flag na tabela TAB_TRANS_INSP.- MARCAR COMO INSPEÇÃO GERADA E NÚMERO
use RamosDiversos
update TAB_TRANS_INSP set num_insp =   "numeroInspecao", COD_STATUS_I = 2 where nosso_numero = NOSSO_NUMERO_ORIG

Após as verificações acima, se não for inspeção do RE (COD_RAMO = 112, 113 E 114), irá verificar se é de Equipamentos (COD_RAMO = 300, 620 E 710): 
Acessa a tabela ramosdiversos..TAB_CTRL_EMIS para obter os campos COD_CORR e DAT_REC_CIA
use RamosDiversos
select COD_CORR, DAT_REC_CIA from TAB_CTRL_EMIS where NUM_PI = 2220443496
--COD_CORR	= 912714
--DAT_REC_CIA = 20220412

Inclui TAB_INSP
Inclui TAB_DISTRINSP
Atualiza Flag da tabela TAB_TRANS_INSP_EQUIPAMENTOS
use RamosDiversos
update Tab_Trans_Insp_Equipamentos SET NUM_INSP = "numeroInspecao",  COD_STATUS_I = 2 WHERE nosso_numero = NOSSO_NUMERO_ORIG

Atualiza tabela TAB_INSP_EQUIP_COMPL

use Inspecao
update Tab_Insp_Equip_Compl SET NUM_INSP =  "numeroInspecao" WHERE nosso_numero = NOSSO_NUMERO_ORIG
		*/
	END

	IF (@NUM_PI <> 0)
	BEGIN
		SELECT TOP 1 *
		FROM #temp
	END
END
GO


