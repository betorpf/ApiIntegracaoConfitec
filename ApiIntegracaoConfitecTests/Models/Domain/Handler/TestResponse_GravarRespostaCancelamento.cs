using ApiIntegracaoConfitec.Helpers;
using ApiIntegracaoConfitec.Models.Domain.Handler;
using NUnit.Framework;

namespace ApiIntegracaoConfitecTests.Models.Domain.Handler
{
    [TestFixture]
    public class TestResponse_GravarRespostaCancelamento
    {
        [Test]
        public void GravarRespostaCancelamentoResponseFalha()
        {
            //Act/Assert
            Assert.Throws(Is.TypeOf<BRQValidationException>()
                          .And.Message.EqualTo("Falha"),
                () => new GravarRespostaCancelamentoResponse(false, "Falha"));
        }
    }
}
