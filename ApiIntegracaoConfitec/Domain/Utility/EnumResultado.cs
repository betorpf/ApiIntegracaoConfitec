using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Domain.Utility
{
    public enum EnumResultado
    {
        Sucesso_EnviadoConfitec = 1,
        Sucesso_ReutilizarInspecao = 2,
        Falha_RecuperarInspecao = 100,
        Falha_SolicitacaoConfitec = 101,
    }
}
