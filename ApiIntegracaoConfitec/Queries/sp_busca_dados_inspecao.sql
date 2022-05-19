IF EXISTS (
		SELECT *
		FROM sys.objects WITH (NOLOCK)
		WHERE name = 'sp_busca_dados_inspecao'
		)
	DROP PROCEDURE sp_busca_dados_inspecao
GO

CREATE PROCEDURE sp_busca_dados_inspecao @NUM_PI INT
AS
BEGIN
	SET NOCOUNT ON

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

	/*VARI�VEIS*/
	DECLARE @Ind_statusInspecao INT
	DECLARE @NUM_APOL_ANT NUMERIC(10, 0)
	DECLARE @NUM_APOL NUMERIC(10, 0)
	DECLARE @NUM_PI_ANT INT
	DECLARE @NUM_PROP DECIMAL(6, 0)
	DECLARE @NUM_LOGR_RISCO DECIMAL(8, 0)
	DECLARE @NUM_INSP_ANTERIOR DECIMAL(7, 0)
	DECLARE @VALIDADE_INSPECAO INT
	DECLARE @NUM_INSP INT
	DECLARE @NUM_COTAC VARCHAR(12)
	DECLARE @CONDINSPEC NUMERIC(1, 0)
	SET @NUM_PI = 1800009225

	/* A BRQ ir� receber o campo chave NUM_PI 
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
	se nesse campo estiver com �2� a inspe��o � obrigat�ria. 
	Nesse caso ir� ter inspe��o e a mesma n�o ser� reaproveitada da inspe��o anterior.
	*/
	IF (@Ind_statusInspecao = 2)
	BEGIN
		/* Se inspe��o for obrigat�ria, proceder: 
		- Verifica se tem Siscota, se tem acessar tabela SISCOTA.SISCOTA.TAB_COTACAO e obter o campo CONDINSPEC
		e se for igual a �2� n�o gravar inspe��o.*/

		SELECT TOP 1 @NUM_APOL_ANT = NUM_APOL_ANT
		FROM [SPX21250PSQLNEO].YAS_ND.YAS.TAB_CTRL_EMIS WITH (NOLOCK)
		WHERE NUM_PI = @NUM_PI

		SELECT TOP 1 @NUM_APOL = NUM_APOL
			,@NUM_INSP_ANTERIOR = NUM_INSP
		FROM [SPX21250PSQLNEO].YAS_ND.YAS.TAB_PED_LOC
		WHERE NUM_APOL = @NUM_APOL_ANT
			AND NUM_ENDO = 0

		SELECT @NUM_COTAC = NUM_COTAC FROM TAB_CTRL_EMIS WHERE NUM_APOL = @NUM_APOL
		--NUM_COTAC = 0 => N�O TEM SISCOTA / --NUM_COTAC = 202100170372
		SELECT @CONDINSPEC = CONDINSPEC FROM SISCOTA.TAB_COTACAO WHERE CCOTACAO = @NUM_COTAC
		--CONDINSPEC = 0
		-- Se n�o for Siscota ou se campo CODINSPEC diferente de �2�, incluir inspe��o:
		
		-- VERIFICAR COMO INSERIR E O QUE INSERIR
		/*Insert TAB_INSP
		Insert TAB_DISTRINSP
		Insert TAB_COBERT_INSP*/

/*Se o COD_RAMO = 114 proceder:
Obter o COD_ESTIP da tabela ramosdiversos..TAB_PED com o NUM_PI e verificar se o COD_ESTIP � diferente de 11 e 13 (Condom�nio Amplo), se sim desprezar.
use RamosDiversos
use RamosDiversos
select COD_ESTIP from TAB_PED where NUM_PI = 2220554189
--COD_ESTIP = 11

Caso n�o seja desprezado, ir� incluir fixo as coberturas abaixo na tabela TAB_COBERT_INSP


USE Inspecao
INSERT INTO Tab_Cobert_Insp VALUES ("N�MERO INSPE��O", 1103, 999999, 0)
INSERT INTO Tab_Cobert_Insp VALUES ("N�MERO INSPE��O", 1107, 999999, 0)
INSERT INTO Tab_Cobert_Insp VALUES ("N�MERO INSPE��O", 1108, 999999, 0)
INSERT INTO Tab_Cobert_Insp VALUES ("N�MERO INSPE��O", 1110, 999999, 0)
INSERT INTO Tab_Cobert_Insp VALUES ("N�MERO INSPE��O", 1112, 999999, 0)

No final atualizar o flag na tabela TAB_TRANS_INSP.- MARCAR COMO INSPE��O GERADA E N�MERO
use RamosDiversos
update TAB_TRANS_INSP set num_insp =   "numeroInspecao", COD_STATUS_I = 2 where nosso_numero = NOSSO_NUMERO_ORIG

Ap�s as verifica��es acima, se n�o for inspe��o do RE (COD_RAMO = 112, 113 E 114), ir� verificar se � de Equipamentos (COD_RAMO = 300, 620 E 710): 
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
	ELSE
	BEGIN
		/*
		Se a inspe��o n�o for obrigat�ria, diferente de �2�, 	
		vai pesquisar a tabela YAS.TAB_PED_LOC com o n�mero da ap�lice anterior, 
		endosso = 0 e o n�mero do item,
		para obter os dados do local de risco
		*/
		
		-- SELECT * FROM OPENQUERY([SPX21250PSQLNEO], 'select * FROM YAS_ND.YAS.TAB_CTRL_EMIS)')
		
		/*declare @NUM_APOL_ANT [numeric](10, 0)
		declare @NUM_PI int
		declare @Ind_statusInspecao int
		set @NUM_PI = 1800009225*/

		SELECT TOP 1 @Ind_statusInspecao = Ind_statusInspecao
		FROM RamosDiversos..TAB_PED_LOC_COMPL WITH (NOLOCK)
		WHERE NUM_PI = @NUM_PI

		SELECT TOP 1 @NUM_APOL_ANT = NUM_APOL_ANT
		FROM [SPX21250PSQLNEO].YAS_ND.YAS.TAB_CTRL_EMIS WITH (NOLOCK)
		WHERE NUM_PI = @NUM_PI

		/*DECLARE @TSQL varchar(8000)

		SELECT  @TSQL = 'SELECT TOP 1 ' + @NUM_APOL_ANT + ' = NUM_APOL_ANT
		FROM OPENQUERY([SPX21250PSQLNEO],''select NUM_APOL_ANT FROM YAS_ND.YAS.TAB_CTRL_EMIS WITH (NOLOCK)
		WHERE NUM_PI = WHERE NUM_PI = ''''' + @NUM_PI + ''''''')'
		EXEC (@TSQL)

		print @Ind_statusInspecao
		print @NUM_APOL_ANT*/

		SELECT TOP 1 @NUM_APOL = NUM_APOL
			,@NUM_INSP_ANTERIOR = NUM_INSP
		FROM [SPX21250PSQLNEO].YAS_ND.YAS.TAB_PED_LOC
		WHERE NUM_APOL = @NUM_APOL_ANT
			AND NUM_ENDO = 0

		/*
		Pesquisar a tabela YAS.TAB_CTRL_EMIS com o n�mero da ap�lice anterior e obter   
		n�mero da proposta e o numero do PI  
		e com esses dados acessar a tabela YAS.TAB_PED_INSP 
		e obter o n�mero do Local de Risco 
		(Lembrando que essa pesquisa � feita por item)
		*/
		SELECT TOP 1 @NUM_PI_ANT = NUM_PI
			,@NUM_PROP = NUM_PROP
		FROM RamosDiversos..TAB_CTRL_EMIS WITH (NOLOCK)
		WHERE NUM_APOL = @NUM_APOL

		SELECT TOP 1 @NUM_LOGR_RISCO = NUM_LOGR_RISCO
		FROM [SPX21250PSQLNEO].YAS_ND.YAS.TAB_PED_INSP WITH (NOLOCK)
		WHERE NUM_PI = @NUM_PI_ANT
			AND NUM_PED = @NUM_PROP

		/*
		Se o n�mero da inspe��o na YAS.TAB_PED_LOC for diferente de zeros, 
		*/
		IF (@NUM_INSP_ANTERIOR <> 0)
		BEGIN
			/*
			pesquisar a inspe��o anterior na tabela Inspe��o..TAB_INSP,  
			*/
			/*TODO: VALIDAR CAMPOS E VARI�VEIS*/
			SELECT @NUM_INSP = num_insp
			FROM inspecao..tab_insp
			WHERE num_apol = @NUM_APOL_ANT

			IF (@NUM_INSP <> 0)
			BEGIN 
			/*e se o n�mero da Inspe��o na TAB_INSP for diferente de zeros */
				/*TODO: ENTENDER*/
				/*
				ir� compara os dados da proposta com os dados da inspe��o que j� existe, 
				ir� comparar CEP, N�mero do local de Risco, CFP-CNPJ, Classifica��o e Complemento, 
				se os dados forem iguais, ir� buscar as Coberturas na tabela  
				TAB_COBERT_INSP e comparar se as coberturas s�o iguais
				
				Se coberturas tamb�m forem iguais, ir� verificar a validade da Inspe��o:
				Para o Residencial (COD_RAMO = 113) s�o 5 anos e demais 3 anos

				*/
				SET @NUM_INSP = 1 -- fica reclamando de if vazio
				
				
			END
			ELSE IF (@NUM_INSP = 0)
			BEGIN
				SET @NUM_INSP = 0 -- fica reclamando de if vazio
			/*Se o n�mero da Inspe��o na TAB_INSP for igual zeros, */
				/*
				
				ir� comparar os dados da ap�lice atual com os dados da ap�lice anterior 
				e caso seja iguais ir� pegar as coberturas da tabela YAS.TAB_PED_COBERT 
				e ir� efetuar as compara��es dos dados de  CEP, N�mero do local de Risco, CFP-CNPJ, Classifica��o e Complemento, 
				se os dados forem iguais, ir� buscar as Coberturas na tabela  TAB_COBERT_INSP e comparar se as coberturas s�o iguais. 
				
				Se as coberturas tamb�m forem iguais, ir� verificar a validade da Inspe��o:
				Para o Residencial (COD_RAMO = 113) s�o 5 anos e demais 3 anos
			
				*/
			END 
			
			/*
			Verificar se as os campos s�o iguais
			
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
	
			/*
			TODO: apagar se existir
			#TMP_YAS_ND_TAB_PED_COBERT
			#TMP_INSPECAO_TAB_COBERT_INSP
			*/
			
			SELECT cod_cobert, val_is
			into #TMP_YAS_ND_TAB_PED_COBERT
			FROM [SPX21250PSQLNEO].YAS_ND.YAS.TAB_PED_COBERT WHERE NUM_APOL = @NUM_APOL --n�mero ap�lice

			SELECT @NUM_INSP = NUM_INSP FROM Inspecao..TAB_INSP WHERE NUM_APOL = @NUM_APOL

			SELECT Cod_Cobert, Lmg_Cobert	
			into #TMP_INSPECAO_TAB_COBERT_INSP
			FROM Inspecao..TAB_COBERT_INSP WHERE NUM_INSP = @NUM_INSP	
   
			/*
			Se as coberturas tamb�m forem iguais, ir� verificar a validade da Inspe��o:
			
			*/
			declare @COBERT_COUNT int 
			select @COBERT_COUNT = count(1)
				from #TMP_YAS_ND_TAB_PED_COBERT Y
					left join #TMP_INSPECAO_TAB_COBERT_INSP I
						on Y.cod_cobert = I.Cod_Cobert
						and Y.val_is = I.Lmg_Cobert
				where Y.cod_cobert is null
				or I.Cod_Cobert is null

			if( @COBERT_COUNT = 0) --Mesmos campos em ambas tabelas.
			begin
				/*Para o Residencial (COD_RAMO = 113) s�o 5 anos e demais 3 anos*/
				set @VALIDADE_INSPECAO = 5
				set @VALIDADE_INSPECAO = 3
			end

		END
		
		/*Ap�s ir� verificar se h� algum aviso de sinistro:
		Ir� verificar se h� ap�lice nas tabelas YAS.TAB_SAP_SIN_PAG ou YAS.TAB_SIN_PAG */
		
		SELECT * FROM [SPX21250PSQLNEO].YAS_ND.YAS.TAB_SIN_PAG WHERE NUM_APOL = @NUM_APOL
		SELECT * FROM [SPX21250PSQLNEO].YAS_ND.YAS.TAB_SAP_SIN_PAG WHERE NUM_APOL = @NUM_APOL
		/*
		Se houve sinistro, ir� carregar os valores pagos para as coberturas sinistradas e verifica 
		se os valores da IS das coberturas s�o iguais aos valores da IS da Inspe��o, 
		se forem diferentes dever� ter outra inspe��o e a anterior n�o ser� reaproveitada
		*/
		
		-- FIM da N�O OBRIGAT�RIA --
		
	END

	IF (@NUM_PI <> 0)
	BEGIN
		SELECT TOP 1 *
		FROM #temp
	END
END
GO


