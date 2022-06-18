USE RamosDiversos
GO

IF EXISTS (
		SELECT *
		FROM sys.objects WITH (NOLOCK)
		WHERE name = 'sp_brq_grava_dados_laudo'
		)
	DROP PROCEDURE sp_brq_grava_dados_laudo
GO

CREATE PROCEDURE sp_brq_grava_dados_laudo 
@Numero_Orcamento bigint
,@Numero_Versao_Orcamento bigint
,@Numero_Item_Versao_Orcamento int
,@Numero_Cnpj_Cpf bigint
,@Numero_Solicitacao_Inspecao varchar(50)
,@Flag_Local_Risco_Endereco_Inspecao char(1)
,@Nome_Contato varchar(200)
,@Numero_Telefone_Contato varchar(20)
,@Codigo_Tipo_Logradouro int
,@Codigo_Logradouro int
,@Nome_Logradouro varchar(200)
,@Numero_Logradouro varchar(200)
,@Numero_Cep bigint
,@Nome_Complemento varchar(200)
,@Nome_Bairro varchar(200)
,@Nome_Cidade varchar(200)
,@Codigo_Unidade_Federacao  int
,@Codigo_Pais int
,@Texto_Ponto_Referencia varchar(200)
,@Numero_Latitude decimal(10,2)
,@Numero_Longitude decimal(10,2
,@Codigo_Status_Solicitacao_Inspecao varchar(200)
,@Codigo_Status_Parecer_Solicitacao_Inspecao varchar(200)
,@Flag_Solicitacao_Inspecao_Automatico varchar(200)
,@Data_Solicitacao_Inspecao datetime
,@Flag_Inspetor_Confiavel char(1)
,@Codigo_Atividade varchar(200)
,@Flag_Codigo_Atividade_Alterada char(1)
,@Flag_Endereco_Alterado char(1)
,@Flag_Ativo char(1)
,@Motivo_Inspecao int
,@Data_Agendamento datetime
,@Numero_Sinistro int
AS
BEGIN
	SET NOCOUNT ON

	select @Numero_Solicitacao_Inspecao
	
END