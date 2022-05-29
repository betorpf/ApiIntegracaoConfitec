using ApiIntegracaoConfitec.Interfaces.Infrastructure.Connection;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ApiIntegracaoConfitec.Infrastructure.Connection
{
    public class SqlConnectionDefault : ISQLConnectionDefault
    {
        private readonly IConfiguration _config;
        public SqlConnectionDefault(IConfiguration config)
        {
            this._config = config;
        }
        public IDbConnection Connection()
        {
            //TODO: ambiente-config.GetAmbiente();
            var connectionStringConfig = this._config.GetConnectionString("DefaultConnection");
            var connectionString = string.Empty;

            if (connectionStringConfig.Contains("Token"))
            {
                connectionString = connectionStringConfig; // descriptografar a conection string // ConnectionStringTokenDecryptor.DecryptConnectionStringTokenized(connectionStringConfig);
            }
            else
            {
                connectionString = connectionStringConfig;
            }

            return new SqlConnection(connectionString);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
