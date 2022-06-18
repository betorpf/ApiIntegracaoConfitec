using ApiIntegracaoConfitec.Helpers;
using ApiIntegracaoConfitec.Models.Domain.Handler;
using NUnit.Framework;

namespace ApiIntegracaoConfitecTests.Models.Domain.Handler
{
    [TestFixture]
    public class TestResponse_GravarRespostaInspecao
    {
        [Test]
        public void GravarRespostaInspecaoResponseFalha()
        {
            //Act/Assert
            Assert.Throws(Is.TypeOf<CommunicationException>()
                          .And.Message.EqualTo("Falha"),
                () => new GravarRespostaInspecaoResponse(false, "Falha"));
        }
    }
}
