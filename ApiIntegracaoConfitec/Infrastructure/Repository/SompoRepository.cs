using ApiIntegracaoConfitec.Helpers;
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

        public async Task<bool> GravarRetornoSolicitarInspecao(ConfitecSolicitarInspecao responseSolicitarInspecao)
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

                    var result = await connectionDb.QueryAsync<DadosAutenticacao>(sql, parameters);

                    return true;
                }
                catch (Exception ex)
                {
                    throw new CommunicationException("Ocorreu um erro ao acessar a base. Método: SompoRepository.GravarRetornoSolicitarInspecao - Erro: " + ex.Message);
                }
            }

        }

        // Gravar laudo
        public async Task<DadosLaudo> GravarRetornarDadosLaudo(ResultadoInspecaoRequest resultadoInspecao)
        {
            //TODO: VALIDAR OS CAMPOS QUE DEVEM SER GRAVADOS
            //TODO: CRIAR PROCEDURE COM OS CAMPOS DO RETORNO
            var parameters = new DynamicParameters(new
            {
                NUM_PI = resultadoInspecao.numeroSolicitacaoInspecao
            });

            var sql = $@"EXEC sp_brq_grava_dados_laudo @NUM_PI";

            using (var connectionDb = this._connection.Connection())
            {
                connectionDb.Open();
                try
                {

                    var result = await connectionDb.QueryAsync<DadosLaudo>(sql, parameters);

                    return result.ToList().Count > 0 ? result.ToList()[0] : null;
                }
                catch (Exception ex)
                {
                    throw new CommunicationException("Ocorreu um erro ao acessar a base. Método: SompoRepository.GravarRetornarDadosLaudo - Erro: " + ex.Message);
                }
            }
        }

        // Cancelar inspeção 
        public async Task<bool> GravarRetornoCancelarInspecao(ResponseCancelarInspecao responseCancelarInspecao)
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
                    var result = await connectionDb.QueryAsync<DadosAutenticacao>(sql, parameters);

                    return true;
                }
                catch (Exception ex)
                {
                    throw new CommunicationException("Ocorreu um erro ao acessar a base. Método: SompoRepository.GravarRetornoCancelarInspecao - Erro: " + ex.Message);
                }
            }

        }
    }
}
