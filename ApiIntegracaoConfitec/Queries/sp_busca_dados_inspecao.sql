

IF exists(select * from sys.objects where name = 'sp_busca_dados_pi')
	drop procedure sp_busca_dados_pi
go

create procedure sp_busca_dados_inspecao 
	@pi int
as

	set nocount on

	
	create table #temp
	(
		codigoRamo			 		decimal(3,0) 				
		,codigoModalidade			 decimal(10,0) 		
		,nomeContato			 		char(40) 
	,telefoneNumeroContato	char(14)
	,codigoCorretorPrincipal	bigint
	,numeroProposta			varchar(6)
	,numeroObjetoSegurado	varchar(130) 
	,numeroApolice				decimal(10,0)
	,numeroEndosso				decimal(6,0)
	,codigoUf					varchar(2)	 	
	,nomeMunicipio			varchar(30)	
	,numeroCep				varchar(8) 	
	,nomeBairro			 varchar(30)	
	,nomeLogradouro				varchar(50)
	,numeroLogradouro		varchar(8)
	,nomeComplemento				varchar(20)
	,codigoTipoLogradouro		varchar(3)
	,codigoTipoPessoa		varchar(1)
	,nomeSegurado			varchar(60)
	,numeroCpfCnpjSegurado	varchar(14)
	,dataPedidoInspecao			decimal(8,0) 
	,observacoes			 	varchar(250) 
	,descricaoObjetoSegurado	varchar(130) 
	,codigoCobertura			numeric(4,0)
	,valorLmi			 	decimal(15,2)
	,nomeContato1			 	varchar(50)
	,telefoneNumeroContato1	varchar(50)
	,nomeContato2			 	varchar(50)
	,telefoneNumeroContato2	varchar(50)
	,numeroSinistro			decimal(7,0) 	
	,causaGeradora			varchar(50)
	,dataOcorrencia			varchar(50)
	,valorSinistro			decimal(15,2)
	,descricaoCampo			varchar(50)
	,conteudoCampo			varchar(50)
	)

	declare @Ind_statusInspecao int
	declare @NUM_APOL_ANT int
	declare @NUM_APOL  int

	declare @NUM_PI int
	declare @NUM_PROP int 
	declare @NUM_LOGR_RISCO int 
	select top 1 @Ind_statusInspecao = Ind_statusInspecao from TAB_PED_LOC_COMPL with(nolock) WHERE NUM_PI = @pi
	if(@Ind_statusInspecao = 2)
	begin

	end
	else
	begin

		select top 1 @NUM_APOL_ANT = NUM_APOL_ANT from TAB_CTRL_EMIS with(nolock) WHERE NUM_PI = @pi

		select top 1 @NUM_APOL = NUM_APOL from YAS.TAB_PED_LOC with(nolock) where NUM_APOL = @NUM_APOL_ANT and NUM_ENDO = 0

		select top 1 @NUM_PI= NUM_PI, @NUM_PROP = NUM_PROP from TAB_CTRL_EMIS with(nolock) WHERE NUM_APOL = @NUM_APOL

		select top 1 @NUM_LOGR_RISCO = NUM_LOGR_RISCO from TAB_PED_INSP with(nolock) where NUM_PI = @NUM_PI AND NUM_PED = @NUM_PROP

		if(@NUM_PI <> 0)
			

	end



	select top 1 * 
	from #temp	
	
go