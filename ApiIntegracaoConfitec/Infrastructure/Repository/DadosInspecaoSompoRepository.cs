using ApiIntegracaoConfitec.Interfaces.Infrastructure.Connection;
using ApiIntegracaoConfitec.Interfaces.Infrastructure.Repository;
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

        public async Task<DadosInspecao> RetornaDadosInspecao(string pi)
        {
            //TODO: trocar para sql parameter
            var sql = $@"EXEC sp_brq_buscar_dados_inspecao {pi}";

            using (var connectionDb = this._connection.Connection())
            {
                connectionDb.Open();
                try
                {
                 
                    var result = await connectionDb.QueryAsync<DadosInspecao>(sql);
                    
                    return result.ToList().Count > 0 ? result.ToList()[0] : null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<DadosCancelarInspecao> RetornaDadosCancelarInspecao(string pi)
        {
            //TODO: trocar para sql parameter
            var sql = $@"EXEC sp_busca_dados_cancelar_pi {pi}";

            using (var connectionDb = this._connection.Connection())
            {
                connectionDb.Open();
                try
                {

                    var result = await connectionDb.QueryAsync<DadosCancelarInspecao>(sql);

                    return result.ToList().Count > 0 ? result.ToList()[0] : null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<DadosAutenticacao> RetornaDadosAutenticacao()
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
    }
}
