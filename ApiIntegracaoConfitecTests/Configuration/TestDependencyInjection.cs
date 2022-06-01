using ApiIntegracaoConfitec.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiIntegracaoConfitecTests.Configuration
{
    [TestFixture]
    public class TestDependencyInjection
    {
        [Test]
        public void DependencyInjectionTest()
        {
            var services = new Mock<IServiceCollection>();

            DependencyInjection.ConfigurationDependencyInjection(services.Object);
            //TODO: Implementar o Assert do teste
        }
    }
}
