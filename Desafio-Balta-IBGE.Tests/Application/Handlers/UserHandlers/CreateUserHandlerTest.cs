using Desafio_Balta_IBGE.Application.UseCases.Users.Handler;
using Desafio_Balta_IBGE.Application.UseCases.Users.Request;
using Desafio_Balta_IBGE.Application.UseCases.Users.Response;
using Desafio_Balta_IBGE.Domain.Interfaces.UnitOfWork;
using Desafio_Balta_IBGE.Domain.Interfaces.UserRepository;
using Moq;
using System.Net;

namespace Desafio_Balta_IBGE.Tests.Application.Handlers.UserHandlers
{
    [TestClass]
    public class CreateUserHandlerTest
    {
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IUserRepository> repository;

        public CreateUserHandlerTest()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            repository = new Mock<IUserRepository>();
        }

        [TestMethod]
        [DataRow(null, null, null)]
        public void Deve_retornar_erro_ao_informar_dados_invalidos_na_requisicao(string name, string email, string password)
        {
            #region Arrange

            var request = new CreateUserRequest(name, email, password);

            #endregion

            #region Act

            var response = new CreateUserHandler(repository.Object, unitOfWork.Object).Handle(request, CancellationToken.None).GetAwaiter().GetResult();

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

            #endregion

            #region Act

            var response = new CreateUserHandler(repository.Object, unitOfWork.Object).Handle(request, CancellationToken.None).GetAwaiter().GetResult();

            #endregion

            #region Assert

            Assert.IsInstanceOfType(response, typeof(InvalidRequest));
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.IsTrue(response.Errors.Count() > 0);


            #endregion

        }

        [TestMethod]
        [DataRow("user@example.com")]
        public void Deve_retornar_sucesso_ao_informar_email_valido_na_requisicao(string email)
        {
            #region Arrange

            var request = new CreateUserRequest("Nome Ficticio", email, "@Admin123");

            #endregion

            #region Act

            var response = new CreateUserHandler(repository.Object, unitOfWork.Object).Handle(request, CancellationToken.None).GetAwaiter().GetResult();

            #endregion

            #region Assert

            Assert.IsInstanceOfType(response, typeof(CreatedSuccessfully));
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            #endregion
        }
    }
}
