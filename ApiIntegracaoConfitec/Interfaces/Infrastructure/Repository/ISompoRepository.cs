﻿using ApiIntegracaoConfitec.Models.Confitec;
using ApiIntegracaoConfitec.Models.Domain.Handler;
using ApiIntegracaoConfitec.Models.Entity;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitec.Interfaces.Infrastructure.Repository
{
    public interface ISompoRepository
    {
        // Solicitar inspeção
        Task<DadosInspecao> RetornarDadosInspecao(string pi);
        Task<DadosAutenticacao> RetornarDadosAutenticacao();
        Task<bool> GravarRetornoSolicitarInspecao(ConfitecSolicitarInspecao responseSolicitarInspecao);
        // Gravar laudo
        Task<DadosLaudo> GravarRetornarDadosLaudo(ResultadoInspecaoRequest resultadoInspecao);
        // Cancelar inspeção
        Task<bool> GravarRetornoCancelarInspecao(ResponseCancelarInspecao responseCancelarInspecao);
    }
}
