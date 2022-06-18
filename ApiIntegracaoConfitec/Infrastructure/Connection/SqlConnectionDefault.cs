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
        private readonly String _selectedConnectionString;
        public SqlConnectionDefault(IConfiguration config)
        {
            this._config = config;
            this._selectedConnectionString = this._config.GetValue<string>("SelectedConnectionString").ToString();
        }
        public IDbConnection Connection()
        {
            var connectionStringConfig = this._config.GetConnectionString(this._selectedConnectionString);
            string connectionString;
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
