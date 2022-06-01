﻿using ApiIntegracaoConfitec.Helpers;
using ApiIntegracaoConfitec.Interfaces.Infrastructure.Connection;
using ApiIntegracaoConfitec.Interfaces.Infrastructure.Repository;
using ApiIntegracaoConfitec.Models.Confitec;
using ApiIntegracaoConfitec.Models.Entity;
using Dapper;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Infrastructure.Repository
{
    public class SompoRepository : ISompoRepository
    {
        private readonly ISQLConnectionDefault _connection;

        public SompoRepository(ISQLConnectionDefault connection)
        {
            this._connection = connection;
        }

        // Solicitar inspeção
        public async Task<DadosInspecao> RetornarDadosInspecao(string pi)
        {
            var parameters = new DynamicParameters( new {
                NUM_PI = pi
            });

            var sql = $@"EXEC sp_brq_buscar_dados_inspecao @NUM_PI";

            using (var connectionDb = this._connection.Connection())
            {
                connectionDb.Open();
                try
                {
                 
                    var result = await connectionDb.QueryAsync<DadosInspecao>(sql, parameters);
                    
                    return result.ToList().Count > 0 ? result.ToList()[0] : null;
                }
                catch (Exception ex)
                {
                    throw new CommunicationException("Ocorreu um erro ao acessar a base. Método: SompoRepository.RetornarDadosInspecao - Erro: " + ex.Message);
                }
            }
        }

        public async Task<DadosAutenticacao> RetornarDadosAutenticacao()
        {
            var sql = $@"EXEC sp_brq_dados_confitec_autenticacao";

            using (var connectionDb = this._connection.Connection())
            {
                connectionDb.Open();
                try
                {

                    var result = await connectionDb.QueryAsync<DadosAutenticacao>(sql);

                    return result.ToList().Count > 0 ? result.ToList()[0] : null;
                }
                catch (Exception ex)
                {
                    throw new CommunicationException("Ocorreu um erro ao acessar a base. Método: SompoRepository.RetornarDadosAutenticacao - Erro: " + ex.Message);
                }
            }
        }

        public async Task<QueryResult> GravarRetornoSolicitarInspecao(ConfitecSolicitarInspecao responseSolicitarInspecao)
        {

            var parameters = new DynamicParameters(new
            {
                numero_inspecao = responseSolicitarInspecao.numeroInspecao,
                data_processamento = responseSolicitarInspecao.dataProcessamento,
                codigo_resultado = responseSolicitarInspecao.codigoResultado,
                mensagem_retorno = responseSolicitarInspecao.mensagemRetorno,
                protocolo_abertura = responseSolicitarInspecao.protocoloAbertura,
                lista_erros = responseSolicitarInspecao.erros is null ? null : String.Join("|", responseSolicitarInspecao.erros)
            });

            var sql = $@"EXEC sp_brq_gravar_retorno_solicitar_inspecao @numero_inspecao, @data_processamento, @codigo_resultado, @mensagem_retorno, @protocolo_abertura, @lista_erros";

            using (var connectionDb = this._connection.Connection())
            {
                connectionDb.Open();
                try
                {

                    var result = await connectionDb.QueryAsync<QueryResult>(sql, parameters);

                    return result.ToList().Count > 0 ? result.ToList()[0] : null;
                }
                catch (Exception ex)
                {
                    throw new CommunicationException("Ocorreu um erro ao acessar a base. Método: SompoRepository.GravarRetornoSolicitarInspecao - Erro: " + ex.Message);
                }
            }

        }

        // Gravar laudo
        public async Task<QueryResult> GravarRetornarDadosLaudo(ResultadoInspecao resultadoInspecao)
        {
            //TODO: VALIDAR OS CAMPOS QUE DEVEM SER GRAVADOS
            //TODO: CRIAR PROCEDURE COM OS CAMPOS DO RETORNO
            var parameters = new DynamicParameters(new
            {
                Numero_Orcamento = resultadoInspecao.numeroOrcamento,
                Numero_Versao_Orcamento = resultadoInspecao.numeroVersaoOrcamento,
                Numero_Item_Versao_Orcamento = resultadoInspecao.numeroItemVersaoOrcamento,
                Numero_Cnpj_Cpf = resultadoInspecao.numeroCnpjCpf,
                Numero_Solicitacao_Inspecao = resultadoInspecao.numeroSolicitacaoInspecao,
                Flag_Local_Risco_Endereco_Inspecao = resultadoInspecao.flagLocalRiscoEnderecoInspecao,
                Nome_Contato = resultadoInspecao.nomeContato,
                Numero_Telefone_Contato = resultadoInspecao.numeroTelefoneContato,
                Codigo_Tipo_Logradouro = resultadoInspecao.codigoTipoLogradouro,
                Codigo_Logradouro = resultadoInspecao.codigoLogradouro,
                Nome_Logradouro = resultadoInspecao.nomeLogradouro,
                Numero_Logradouro = resultadoInspecao.numeroLogradouro,
                Numero_Cep = resultadoInspecao.numeroCep,
                Nome_Complemento = resultadoInspecao.nomeComplemento,
                Nome_Bairro = resultadoInspecao.nomeBairro,
                Nome_Cidade = resultadoInspecao.nomeCidade,
                Codigo_Unidade_Federacao = resultadoInspecao.codigoUnidadeFederacao,
                Codigo_Pais = resultadoInspecao.codigoPais,
                Texto_Ponto_Referencia = resultadoInspecao.textoPontoReferencia,
                Numero_Latitude = resultadoInspecao.numeroLatitude,
                Numero_Longitude = resultadoInspecao.numeroLongitude,
                Codigo_Status_Solicitacao_Inspecao = resultadoInspecao.codigoStatusSolicitacaoInspecao,
                Codigo_Status_Parecer_Solicitacao_Inspecao = resultadoInspecao.codigoStatusParecerSolicitacaoInspecao,
                Flag_Solicitacao_Inspecao_Automatico = resultadoInspecao.flagSolicitacaoInspecaoAutomatico,
                Data_Solicitacao_Inspecao = resultadoInspecao.dataSolicitacaoInspecao,
                Flag_Inspetor_Confiavel = resultadoInspecao.flagInspetorConfiavel,
                Codigo_Atividade = resultadoInspecao.codigoAtividade,
                Flag_Codigo_Atividade_Alterada = resultadoInspecao.flagCodigoAtividadeAlterada,
                Flag_Endereco_Alterado = resultadoInspecao.flagEnderecoAlterado,
                Flag_Ativo = resultadoInspecao.flagAtivo,
                Motivo_Inspecao = resultadoInspecao.motivoInspecao,
                Data_Agendamento = resultadoInspecao.dataAgendamento,
                Numero_Sinistro = resultadoInspecao.numeroSinistro
            });

            var sql = $@"EXEC sp_brq_grava_dados_laudo   @Numero_Orcamento 
                                                        ,@Numero_Versao_Orcamento 
                                                        ,@Numero_Item_Versao_Orcamento 
                                                        ,@Numero_Cnpj_Cpf 
                                                        ,@Numero_Solicitacao_Inspecao 
                                                        ,@Flag_Local_Risco_Endereco_Inspecao 
                                                        ,@Nome_Contato 
                                                        ,@Numero_Telefone_Contato
                                                        ,@Codigo_Tipo_Logradouro 
                                                        ,@Codigo_Logradouro 
                                                        ,@Nome_Logradouro 
                                                        ,@Numero_Logradouro 
                                                        ,@Numero_Cep 
                                                        ,@Nome_Complemento 
                                                        ,@Nome_Bairro 
                                                        ,@Nome_Cidade 
                                                        ,@Codigo_Unidade_Federacao  
                                                        ,@Codigo_Pais 
                                                        ,@Texto_Ponto_Referencia 
                                                        ,@Numero_Latitude 
                                                        ,@Numero_Longitude 
                                                        ,@Codigo_Status_Solicitacao_Inspecao 
                                                        ,@Codigo_Status_Parecer_Solicitacao_Inspecao 
                                                        ,@Flag_Solicitacao_Inspecao_Automatico 
                                                        ,@Data_Solicitacao_Inspecao 
                                                        ,@Flag_Inspetor_Confiavel 
                                                        ,@Codigo_Atividade 
                                                        ,@Flag_Codigo_Atividade_Alterada 
                                                        ,@Flag_Endereco_Alterado 
                                                        ,@Flag_Ativo 
                                                        ,@Motivo_Inspecao 
                                                        ,@Data_Agendamento 
                                                        ,@Numero_Sinistro ";

            using (var connectionDb = this._connection.Connection())
            {
                connectionDb.Open();
                try
                {
                    var result = await connectionDb.QueryAsync<QueryResult>(sql, parameters);

                    return result.ToList().Count > 0 ? result.ToList()[0] : null;
                }
                catch (Exception ex)
                {
                    throw new CommunicationException("Ocorreu um erro ao acessar a base. Método: SompoRepository.GravarRetornarDadosLaudo - Erro: " + ex.Message);
                }
            }
        }

        // Cancelar inspeção 
        public async Task<QueryResult> GravarRetornoCancelarInspecao(ResponseCancelarInspecao responseCancelarInspecao)
        {
            var parameters = new DynamicParameters(new
            {
                NUM_PI = responseCancelarInspecao.numeroInspecao
            });

            var sql = $@"EXEC sp_busca_dados_cancelar_pi @NUM_PI";

            using (var connectionDb = this._connection.Connection())
            {
                connectionDb.Open();
                try
                {
                    var result = await connectionDb.QueryAsync<QueryResult>(sql, parameters);
                    return result.ToList().Count > 0 ? result.ToList()[0] : null;
                }
                catch (Exception ex)
                {
                    throw new CommunicationException("Ocorreu um erro ao acessar a base. Método: SompoRepository.GravarRetornoCancelarInspecao - Erro: " + ex.Message);
                }
            }

        }
    }
}
