using ApiIntegracaoConfitec.Interfaces.Infrastructure.Connection;
using ApiIntegracaoConfitec.Interfaces.Infrastructure.Repository;
using ApiIntegracaoConfitec.Models.Entity;
using Dapper;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Infrastructure.Repository
{
    public class DadosLaudoSompoRepository : IDadosLaudoSompoRepository
    {
        private readonly ISQLConnectionDefault _connection;

        public DadosLaudoSompoRepository(ISQLConnectionDefault connection)
        {
            this._connection = connection;
        }
        public async Task<DadosLaudo> RetornarDadosLaudo(string pi)
        {
            var parameters = new DynamicParameters(new
            {
                NUM_PI = pi
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
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
