USE RamosDiversos
GO

IF EXISTS (
		SELECT *
		FROM sys.objects WITH (NOLOCK)
		WHERE name = 'sp_brq_buscar_dados_inspecao_teste'
		)
	DROP PROCEDURE sp_brq_buscar_dados_inspecao_teste
GO

CREATE PROCEDURE sp_brq_buscar_dados_inspecao_teste @NUM_PI DECIMAL(10, 0) ,  @NUM_ITEM DECIMAL(5, 0), @TIP_EMISSAO DECIMAL(4, 0)
AS
BEGIN

	--BEGIN /*TESTES*/
	--	SET @NUM_PI = 2120010069
	--	SET @NUM_ITEM = 1
	--	SET @TIP_EMISSAO = 100
	--END

	BEGIN /*Variáveis*/
		DECLARE @COD_RAMO DECIMAL(3, 0)
		DECLARE @NOSSO_NUMERO_OLD VARCHAR(20)
		DECLARE @NOSSO_NUMERO_NEW VARCHAR(20)
		DECLARE @NUM_APOL DECIMAL(10, 0)
		DECLARE	@CRIAR_NOVA BIT = 1
		DECLARE @MENSAGEM_RETORNO VARCHAR(200)
	END

	BEGIN /*DROP TEMP TABLES*/
		IF OBJECT_ID(N'tempdb..#TMP_RESULTADO') IS NOT NULL
			DROP TABLE tempdb..#TMP_RESULTADO

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
	END

	BEGIN /*Criação da tabela de resultado*/
		CREATE TABLE #TMP_RESULTADO (
			codigoResultado INT
			,descricaoResultado VARCHAR(200)
			,solicitarInspecao BIT
		)

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
			,nossoNumeroOld varchar(20)
			,nossoNumeroNew varchar(20)
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

	BEGIN /*Descobrir o cód ramo*/
		SELECT TOP 1 @COD_RAMO = COD_RAMO
		FROM tab_ped WITH (NOLOCK)
		WHERE num_pi = @NUM_PI
	END

	IF(@TIP_EMISSAO IN (200,300))
	BEGIN/*Se Tipo da Emissão:
			200 - Renovação Sompo
			300 - Endosso
			Validar se precisa solicitar nova inspeção
		*/
		EXEC sp_brq_validar_criar_inspecao_teste @NUM_PI, @NUM_ITEM, @TIP_EMISSAO, @COD_RAMO, @CRIAR_NOVA OUTPUT, @MENSAGEM_RETORNO OUTPUT
	END

	INSERT INTO #TMP_RESULTADO (codigoResultado, descricaoResultado, solicitarInspecao)
	VALUES (0, @MENSAGEM_RETORNO, @CRIAR_NOVA)

	IF(@CRIAR_NOVA=1) /*RECUPERAR DADOS PARA CRIAR UMA NOVA INSPEÇÃO*/
	BEGIN 
		IF (@COD_RAMO IN (112,113,114))
		BEGIN
			/*
			Tipo Emissão: 
				100 = Seguro Novo
				200 = Renovação Sompo
				300 = Endosso
				101 = Renovação Congenere
			Ramos RE (Elemtares):
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
				,descricaoObjetoSegurado
				,nossoNumeroOld
				,nossoNumeroNew)
			SELECT 
				 P.COD_RAMO					--codigoRamo
				,P.COD_ESTIP				--codigoModalidade 
				,ISNULL(PIN.PESS_CONT,'NomeContato')--nomeContato 
				,PIN.NUM_TEL				--telefoneNumeroContato 
				,CE.cod_corr				--codigoCorretorPrincipal 
				,TI.NUM_PED					--numeroProposta 
				,PL.DSC_CLASF				--numeroObjetoSegurado 
				,P.NUM_APOL					--numeroApolice 
				,P.NUM_ENDO					--numeroEndosso 
				,TI.SIG_UF_RISCO			--codigoUf 
				,TI.NOM_CID_RISCO			--nomeMunicipio 
				,TI.NUM_CEP_RISCO			--numeroCep 
				,TI.NOM_BAIRRO_RISCO		--nomeBairro
				,TI.NOM_LOGR_RISCO			--nomeLogradouro 
				,TI.NUM_RISCO				--numeroLogradouro
				,TI.DSC_COMPL_RISCO			--nomeComplemento
				,TI.COD_TIP_LOGR_RISCO		--codigoTipoLogradouro
				,TI.COD_IDEN_PESSOA			--codigoTipoPessoa 
				,TI.NOM_SEGURADO			--nomeSegurado
				,TI.NUM_CGC_CPF				--numeroCpfCnpjSegurado
				,PIN.DAT_CADASTRO			--dataPedidoInspecao
				,''							--observacoes
				,PL.DSC_CLASF   			--descricaoObjetoSegurado
				,PNC.NOSSO_NUMERO_OLD
				,PNC.NOSSO_NUMERO_NEW
			FROM Tab_Ped P(NOLOCK)
			INNER JOIN Tab_Ctrl_Emis CE WITH (NOLOCK) ON CE.num_pi = P.num_pi
			INNER JOIN Tab_ped_loc PL WITH (NOLOCK) ON PL.num_pi = CE.num_pi
			INNER JOIN TAB_PED_INSP PIN WITH (NOLOCK) ON PIN.NOSSO_NUMERO = PL.NOSSO_NUMERO
				AND PIN.NUM_ITEM = PL.NUM_ITEM
			INNER JOIN Tab_Ped_Num_Copia PNC WITH (NOLOCK) ON PNC.NOSSO_NUMERO_NEW = PL.NOSSO_NUMERO
			INNER JOIN Tab_Trans_Insp TI WITH (NOLOCK) ON TI.NOSSO_NUMERO = PNC.NOSSO_NUMERO_OLD
				AND TI.NUM_ITEM = PL.NUM_ITEM
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
			Ramos RD (Diversos):
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
				,descricaoObjetoSegurado
				,nossoNumeroOld
				,nossoNumeroNew)
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
				,PNC.NOSSO_NUMERO_OLD
				,PNC.NOSSO_NUMERO_NEW
			FROM Tab_Ped P(NOLOCK)
			INNER JOIN Tab_Ctrl_Emis CE WITH (NOLOCK) ON CE.num_pi = P.num_pi
			INNER JOIN Tab_ped_loc PL WITH (NOLOCK) ON PL.num_pi = CE.num_pi
			INNER JOIN TAB_PED_INSP PIN WITH (NOLOCK) ON PIN.NOSSO_NUMERO = PL.NOSSO_NUMERO
				AND PIN.NUM_ITEM = PL.NUM_ITEM
			INNER JOIN Tab_Ped_Num_Copia PNC WITH (NOLOCK) ON PNC.NOSSO_NUMERO_NEW = PL.NOSSO_NUMERO
			INNER JOIN Tab_Trans_Insp_Equipamentos TI WITH (NOLOCK) ON TI.NOSSO_NUMERO = PNC.NOSSO_NUMERO_OLD
				AND TI.NUM_ITEM = PL.NUM_ITEM
			WHERE P.num_pi = @NUM_PI
				AND PL.num_item = @NUM_ITEM
		END

		BEGIN /*Recupera o Nosso Número*/
			SELECT TOP 1 @NOSSO_NUMERO_OLD = nossoNumeroOld
			,@NOSSO_NUMERO_NEW = nossoNumeroNew --tab_ped.NOSSO_NUMERO
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
			WHERE NOSSO_NUMERO = @NOSSO_NUMERO_OLD
				AND NUM_ITEM = @NUM_ITEM
		END

		BEGIN /*Lista de Contatos*/
			--listaTelefoneContato
			--Ex. '02207755358996309133'
			INSERT INTO #TMP_TAB_CONTATO
			SELECT
				 PESS_CONTATO_2
				,NUM_TEL_2
			FROM TAB_PED_INSP_COMPL WITH (NOLOCK)
			WHERE NOSSO_NUMERO = @NOSSO_NUMERO_NEW
				AND NUM_ITEM = @NUM_ITEM
				AND ISNULL(PESS_CONTATO_2,'') <>''

			INSERT INTO #TMP_TAB_CONTATO
			SELECT
				 PESS_CONTATO_3
				,NUM_TEL_3
			FROM TAB_PED_INSP_COMPL WITH (NOLOCK)
			WHERE NOSSO_NUMERO = @NOSSO_NUMERO_NEW
				AND NUM_ITEM = @NUM_ITEM
				AND ISNULL(PESS_CONTATO_3,'') <>''

		END

		BEGIN /*Lista de Sinistros*/
			--listaSinistro
			--Consultar somente no caso em tipo de emissão for
				--300 = Endosso
				--200 = Renovação Sompo
			IF (@TIP_EMISSAO in (200,300))
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
				WHERE SP.NUM_APOL = @NUM_APOL
					AND SP.NUM_ITEM = @NUM_ITEM
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
			WHERE NOSSO_NUMERO = @NOSSO_NUMERO_NEW
				AND NUM_ITEM = @NUM_ITEM
		END

	END
	
	SELECT * FROM #TMP_RESULTADO
	SELECT * FROM #TMP_TAB_INSPECAO
	SELECT * FROM #TMP_TAB_COBERTURA
	SELECT * FROM #TMP_TAB_CONTATO
	SELECT * FROM #TMP_TAB_SINISTRO
	SELECT * FROM #TMP_TAB_CAMPOS_VARIAVEIS



END