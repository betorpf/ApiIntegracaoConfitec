using ApiIntegracaoConfitec.Helpers;
using ApiIntegracaoConfitec.Models.Domain.Handler;
using NUnit.Framework;

namespace ApiIntegracaoConfitecTests.Models.Domain.Handler
{
    [TestFixture]
    public class TestResponse_GravarDadosLaudo
    {
        [Test]
        public void GravarDadosLaudoResponseFalha()
        {
            //Act/Assert
            Assert.Throws(Is.TypeOf<BRQValidationException>()
                          .And.Message.EqualTo("Falha"),
                () => new GravarDadosLaudoResponse(false, "Falha"));
        }
    }
}
