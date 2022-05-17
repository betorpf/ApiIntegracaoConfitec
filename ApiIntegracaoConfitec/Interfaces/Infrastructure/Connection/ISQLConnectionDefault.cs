using System;
using System.Data;

namespace ApiIntegracaoConfitec.Interfaces.Infrastructure.Connection
{
    public interface ISQLConnectionDefault : IDisposable
    {
        IDbConnection Connection();
    }
}
