using ApiIntegracaoConfitec.Helpers;
using ApiIntegracaoConfitec.Models.Domain.Handler;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitecTests.Models.Domain.Handler
{
    [TestFixture]
    public class TestRequest_BuscarDadosSolicitarInspecao
    {

        [Test]
        public void BuscarDadosSolicitarInspecaoComErros()
        {
            //Arrange/Act/Assert
            Assert.Throws(Is.TypeOf<BRQValidationException>()
                          .And.Message.EqualTo("Validação de Número do PI informado."),
                () => new BuscarDadosSolicitarInspecaoRequest(0));
        }

    }
}
