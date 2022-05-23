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
    public class DadosInspecaoSompoRepository : IDadosInspecaoSompoRepository
    {
        private readonly ISQLConnectionDefault _connection;

        public DadosInspecaoSompoRepository(ISQLConnectionDefault connection)
        {
            this._connection = connection;
        }

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
                    throw ex;
                }
            }
        }

        public async Task<DadosCancelarInspecao> RetornarDadosCancelarInspecao(string pi)
        {
            var parameters = new DynamicParameters(new
            {
                NUM_PI = pi
            });

            var sql = $@"EXEC sp_busca_dados_cancelar_pi @NUM_PI";

            using (var connectionDb = this._connection.Connection())
            {
                connectionDb.Open();
                try
                {

                    var result = await connectionDb.QueryAsync<DadosCancelarInspecao>(sql, parameters);

                    return result.ToList().Count > 0 ? result.ToList()[0] : null;
                }
                catch (Exception ex)
                {
                    throw ex;
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
                    throw ex;
                }
            }
        }

        public async Task<bool> GravarRetornoSolicitarInspecao(ResponseSolicitarInspecao responseSolicitarInspecao)
        {

            var parameters = new DynamicParameters(new
            {
                numero_inspecao = responseSolicitarInspecao.numeroInspecao,
                data_processamento = responseSolicitarInspecao.dataProcessamento,
                codigo_resultado = responseSolicitarInspecao.codigoResultado,
                mensagem_retorno = responseSolicitarInspecao.mensagemRetorno,
                protocolo_abertura = responseSolicitarInspecao.protocoloAbertura,
                lista_erros = responseSolicitarInspecao.erros.ToString() //TODO: Implementar o tostring
            }); ;


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
                    throw ex;
                }
            }

        }
    }
}
