using Desafio_Balta_IBGE.Application.UseCases.Users.Handler;
using Desafio_Balta_IBGE.Application.UseCases.Users.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_Balta_IBGE.Tests.Users
{
    [TestClass]
    public class CreateUserHandlerTest
    {
        [TestMethod]
        [DataRow((string)null, (string)null, (string)null)]
        public void Deve_retornar_erro_ao_informar_dados_invalidos_na_requisicao(string name, string email, string password)
        {
            #region Arrange

            var request = new CreateUserRequest(name, email, password);
            var handler = new CreateUserHandler();

            #endregion

            #region Act

            var response = handler.Handle(request, CancellationToken.None).GetAwaiter().GetResult();

            #endregion

            #region Assert
            
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.IsTrue(response.Errors.Count > 0);

            #endregion

        }

        [TestMethod]
        [DataRow("some string")]
        [DataRow("somestring")]
        [DataRow("somestring.com")]
        [DataRow(" ")]
        [DataRow("   ")]
        [DataRow("  some string  ")]
        [DataRow("@!!!@!")]
        [DataRow("user@com")]
        [DataRow("user@.com")]
        [DataRow("user@com.")]
        [DataRow("user@com..")]
        [DataRow("user@com...")]
        [DataRow("user@com#")]
        [DataRow("user@com$")]
        [DataRow("user@com%")]
        [DataRow("user@com^")]
        [DataRow("user@com&")]
        [DataRow("user@com*")]
        [DataRow("user@com()")]
        [DataRow("user@com{}")]
        [DataRow("user@com[]")]
        [DataRow("user@com<>")]
        [DataRow("user@com;")]
        [DataRow("user@com:")]
        [DataRow("user@com'")]
        [DataRow("user@com\"")]
        [DataRow("user@com/")]
        [DataRow("user@com\\")]
        [DataRow("user@com|")]
        [DataRow("user@com`")]
        [DataRow("user@com?")]
        [DataRow("user@com!")]
        [DataRow("user@com=")]
        [DataRow("user@com+")]
        [DataRow("user@com_")]
        [DataRow("user@com~")]
        [DataRow("user@com$example")]
        [DataRow("user@com@example")]
        [DataRow("user@com@example.")]
        [DataRow("user@com@")]
        [DataRow("user@com.")]
        [DataRow("user@.com")]
        [DataRow("user.com")]
        [DataRow("user@.com.")]
        public void Deve_retornar_erro_ao_informar_email_invalido_na_requisicao(string email)
        {
            #region Arrange

            var request = new CreateUserRequest("Nome Ficticio", email, "@Admin123");
            var handler = new CreateUserHandler();

            #endregion

            #region Act

            var response = handler.Handle(request, CancellationToken.None).GetAwaiter().GetResult();

            #endregion

            #region Assert

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.IsTrue(response.Errors.Count > 0);

            #endregion

        }

        [TestMethod]
        [DataRow("user@example.com")]
        [DataRow("user@example.com.br")]
        [DataRow("user@example.net")]
        [DataRow("user@example.edu")]
        [DataRow("user@example.gov")]
        [DataRow("user@example.int")]
        [DataRow("user@example.country")]
        [DataRow("user123@gmail.com")]
        [DataRow("user.name@example.co")]
        [DataRow("user_name123@my-domain.net")]
        [DataRow("user+label@example.org")]
        [DataRow("user_name@subdomain.example.com")]
        [DataRow("user@domain-with-hyphen.com")]
        public void Deve_retornar_sucesso_ao_informar_email_valido_na_requisicao(string email)
        {
            #region Arrange

            var request = new CreateUserRequest("Nome Ficticio", email, "@Admin123");
            var handler = new CreateUserHandler();

            #endregion

            #region Act

            var response = handler.Handle(request, CancellationToken.None).GetAwaiter().GetResult();

            #endregion

            #region Assert

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.Errors.Count == 0);

            #endregion
        }
    }
}
