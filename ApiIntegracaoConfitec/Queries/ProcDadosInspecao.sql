
IF EXISTS (
		SELECT *
		FROM sys.objects WITH (NOLOCK)
		WHERE name = 'sp_brq_buscar_dados_inspecao_teste'
		)
	DROP PROCEDURE sp_brq_buscar_dados_inspecao_teste
GO

CREATE PROCEDURE sp_brq_buscar_dados_inspecao_teste @NUM_PI DECIMAL(10, 0) 
AS
BEGIN

	BEGIN /*Variáveis*/
		SET @NUM_PI = 2120010069
		DECLARE @NUM_ITEM DECIMAL(5, 0) = 1
		DECLARE @TIP_EMISSAO DECIMAL(4, 0) = 100
		DECLARE @COD_RAMO DECIMAL(3, 0)
		DECLARE @NOSSO_NUMERO VARCHAR(20)
		DECLARE @NUM_APOL DECIMAL(10, 0)
	END

	/*DROP TEMP TABLES*/

		IF OBJECT_ID(N'tempdb..#TMP_TAB_INSPECAO') IS NOT NULL
			DROP TABLE tempdb..#TMP_TAB_INSPECAO
		
		IF OBJECT_ID(N'tempdb..#TMP_TAB_COBERTURA') IS NOT NULL
			DROP TABLE tempdb..#TMP_TAB_COBERTURA

		IF OBJECT_ID(N'tempdb..#TMP_TAB_CONTATO') IS NOT NULL
			DROP TABLE tempdb..#TMP_TAB_CONTATO

		IF OBJECT_ID(N'tempdb..#TMP_TAB_SINISTRO') IS NOT NULL
			DROP TABLE tempdb..#TMP_TAB_SINISTRO

		IF OBJECT_ID(N'tempdb..#TMP_TAB_CAMPOS_VARIAVEIS') IS NOT NULL
			DROP TABLE tempdb..#TMP_TAB_CAMPOS_VARIAVEIS


	BEGIN /*Criação da tabela de resultado*/
			CREATE TABLE #TMP_TAB_INSPECAO (
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
			)

			CREATE TABLE #TMP_TAB_COBERTURA (
				 codigoCobertura NUMERIC(4, 0)
				,valorLmi DECIMAL(15, 2)
			)

			CREATE TABLE #TMP_TAB_CONTATO (
				 nomeContato VARCHAR(50)
				,telefoneNumeroContato VARCHAR(50)
			)

			CREATE TABLE #TMP_TAB_SINISTRO (
				 numeroSinistro DECIMAL(7, 0)
				,causaGeradora VARCHAR(50)
				,dataOcorrencia VARCHAR(50)
				,valorSinistro DECIMAL(15, 2)
			)

			CREATE TABLE #TMP_TAB_CAMPOS_VARIAVEIS(
				 descricaoCampo VARCHAR(50)
				,conteudoCampo VARCHAR(50)
			)

	END

	--Descobrir o cód ramo
	SELECT TOP 1 @COD_RAMO = COD_RAMO
	FROM tab_ped WITH (NOLOCK)
	WHERE num_pi = @NUM_PI

	IF (@COD_RAMO IN (112,113,114))
	BEGIN
		/*
		Tipo Emissão: 
			100 = Seguro Novo
			200 = Renovação Sompo
			300 = Endosso
			101 = Renovação Congenere
		Ramos RE:
			112 = Empresarial
			113 = Residencial
			114 = Condominio
		*/
		INSERT INTO #TMP_TAB_INSPECAO(
			codigoRamo
			,codigoModalidade 
			,nomeContato 
			,telefoneNumeroContato 
			,codigoCorretorPrincipal 
			,numeroProposta 
			,numeroObjetoSegurado 
			,numeroApolice 
			,numeroEndosso 
			,codigoUf 
			,nomeMunicipio 
			,numeroCep 
			,nomeBairro
			,nomeLogradouro 
			,numeroLogradouro
			,nomeComplemento
			,codigoTipoLogradouro
			,codigoTipoPessoa 
			,nomeSegurado
			,numeroCpfCnpjSegurado
			,dataPedidoInspecao
			,observacoes
			,descricaoObjetoSegurado)
		SELECT 
			 P.COD_RAMO
			,P.COD_ESTIP
			,PIN.PESS_CONT
			,PIN.NUM_TEL
			,CE.cod_corr
			,TI.NUM_PED
			,PL.DSC_CLASF--TI.NOME_EQUIP (RD) //
			,P.NUM_APOL
			,P.NUM_ENDO
			,TI.SIG_UF_RISCO
			,TI.NOM_CID_RISCO
			,TI.NUM_CEP_RISCO
			,TI.NOM_BAIRRO_RISCO
			,TI.NOM_LOGR_RISCO
			,TI.NUM_RISCO
			,TI.DSC_COMPL_RISCO
			,TI.COD_TIP_LOGR_RISCO
			,TI.COD_IDEN_PESSOA
			,TI.NOM_SEGURADO
			,TI.NUM_CGC_CPF
			,PIN.DAT_CADASTRO
			,''				--TI.OBSERVACOES (RD)
			,PL.DSC_CLASF   --TI.NOME_EQUIP (RD) //                   
		FROM Tab_Ped P(NOLOCK)
		INNER JOIN Tab_Ctrl_Emis CE WITH (NOLOCK) ON CE.num_pi = P.num_pi
		INNER JOIN Tab_ped_loc PL WITH (NOLOCK) ON PL.num_pi = CE.num_pi
		INNER JOIN Tab_Trans_Insp TI WITH (NOLOCK) ON TI.NOSSO_NUMERO = PL.NOSSO_NUMERO
			AND TI.NUM_ITEM = PL.NUM_ITEM
		INNER JOIN TAB_PED_INSP PIN WITH (NOLOCK) ON PIN.NOSSO_NUMERO = TI.NOSSO_NUMERO
			AND PIN.NUM_ITEM = TI.NUM_ITEM
		WHERE P.num_pi = @NUM_PI
			AND PL.num_item = @NUM_ITEM
	END
	ELSE IF (@COD_RAMO IN (300,620,710))
	BEGIN
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


		INSERT INTO #TMP_TAB_INSPECAO(
			codigoRamo
			,codigoModalidade 
			,nomeContato 
			,telefoneNumeroContato 
			,codigoCorretorPrincipal 
			,numeroProposta 
			,numeroObjetoSegurado 
			,numeroApolice 
			,numeroEndosso 
			,codigoUf 
			,nomeMunicipio 
			,numeroCep 
			,nomeBairro
			,nomeLogradouro 
			,numeroLogradouro
			,nomeComplemento
			,codigoTipoLogradouro
			,codigoTipoPessoa 
			,nomeSegurado
			,numeroCpfCnpjSegurado
			,dataPedidoInspecao
			,observacoes
			,descricaoObjetoSegurado)
		SELECT 
			 P.COD_RAMO
			,P.COD_ESTIP
			,PIN.PESS_CONT
			,PIN.NUM_TEL
			,CE.cod_corr
			,TI.NUM_PED
			,TI.NOME_EQUIP
			,P.NUM_APOL
			,P.NUM_ENDO
			,TI.UF
			,TI.CIDADE
			,TI.CEP
			,TI.BAIRRO
			,TI.LOGRADOURO
			,TI.LOGR_NUMERO
			,TI.LOGR_COMPL
			,''
			,TI.COD_IDEN_PESSOA
			,TI.NOM_SEGURADO
			,TI.NUM_CGC_CPF
			,PIN.DAT_CADASTRO
			,TI.OBSERVACOES
			,TI.NOME_EQUIP
		FROM Tab_Ped P(NOLOCK)
		INNER JOIN Tab_Ctrl_Emis CE WITH (NOLOCK) ON CE.num_pi = P.num_pi
		INNER JOIN Tab_ped_loc PL WITH (NOLOCK) ON PL.num_pi = CE.num_pi
		INNER JOIN Tab_Trans_Insp_Equipamentos TI WITH (NOLOCK) ON TI.NOSSO_NUMERO = PL.NOSSO_NUMERO
			AND TI.NUM_ITEM = PL.NUM_ITEM
		INNER JOIN TAB_PED_INSP PIN WITH (NOLOCK) ON PIN.NOSSO_NUMERO = TI.NOSSO_NUMERO
			AND PIN.NUM_ITEM = TI.NUM_ITEM
		WHERE P.num_pi = @NUM_PI
			AND PL.num_item = @NUM_ITEM
	END

	BEGIN /*Recupera o Nosso Número*/
		SELECT TOP 1 @NOSSO_NUMERO = @NOSSO_NUMERO --tab_ped.NOSSO_NUMERO
		FROM #TMP_TAB_INSPECAO WITH (NOLOCK)
	END

	BEGIN /*Lista de Coberturas*/
		--listaCoberturaInspecao:
		--Ex. '02209738537470898157'
		INSERT INTO #TMP_TAB_COBERTURA
		SELECT
			 COD_COBERT
			,LMG_COBERT
		FROM TAB_TRANS_COBERT_INSP WITH (NOLOCK)
		WHERE NOSSO_NUMERO = '02209738537470898157'
			AND NUM_ITEM = 1
	END

	BEGIN /*Lista de Contatos*/
		--listaTelefoneContato
		--Ex. '02207755358996309133'
		INSERT INTO #TMP_TAB_CONTATO
		SELECT
			 PESS_CONTATO_2
			,NUM_TEL_2
		FROM TAB_PED_INSP_COMPL WITH (NOLOCK)
		WHERE NOSSO_NUMERO = '02207755358996309133'
			AND NUM_ITEM = 2
			AND ISNULL(PESS_CONTATO_2,'') <>''

		INSERT INTO #TMP_TAB_CONTATO
		SELECT
			 PESS_CONTATO_3
			,NUM_TEL_3
		FROM TAB_PED_INSP_COMPL WITH (NOLOCK)
		WHERE NOSSO_NUMERO = '02207755358996309133'
			AND NUM_ITEM = 2
			AND ISNULL(PESS_CONTATO_3,'') <>''

	END

	BEGIN /*Lista de Sinistros*/
		--listaSinistro
		--Enviar somente no caso em tipo de emissão = 300 - Endosso
		--IF (@TIP_EMISSAO = 300)
		BEGIN
			INSERT INTO #TMP_TAB_SINISTRO
			SELECT distinct
				 SP.NUM_SIN
				,S.DSC_CAUSA_SIN
				,S.DAT_OCOR_SIN
				,SP.VAL_PAG_SIN
			FROM SPX21250PSQLNEO.YAS_ND.YAS.TAB_SAP_SIN S
				INNER JOIN SPX21250PSQLNEO.YAS_ND.YAS.TAB_SAP_SIN_PAG SP
				ON S.NUM_SIN = SP.NUM_SIN
			WHERE SP.NUM_APOL = 1600026060
				AND SP.NUM_ITEM = 1
		END
	END

	BEGIN /*Lista de Protecionais da Proposta*/
		--listaCamposVariaveis
		--Relação de Protecionais da proposta
		INSERT INTO #TMP_TAB_CAMPOS_VARIAVEIS
		SELECT
			 COD_FATOR
			,DSC_COMPL_FATOR
		FROM TAB_PED_FAT_RISCO
		WHERE NOSSO_NUMERO = '02200544192996300373'
			AND NUM_ITEM = 1
	END


	SELECT * FROM #TMP_TAB_INSPECAO
	SELECT * FROM #TMP_TAB_COBERTURA
	SELECT * FROM #TMP_TAB_CONTATO
	SELECT * FROM #TMP_TAB_SINISTRO
	SELECT * FROM #TMP_TAB_CAMPOS_VARIAVEIS



END