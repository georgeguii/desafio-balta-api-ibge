using AutoFixture;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Handler;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Request;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Response;
using Desafio_Balta_IBGE.Domain.Interfaces.IBGE;
using Desafio_Balta_IBGE.Domain.Interfaces.UnitOfWork;
using Desafio_Balta_IBGE.Domain.Models;
using Moq;
using System.Net;

namespace Desafio_Balta_IBGE.Tests.Application.IbgeHandlers
{
    [TestClass]
    public class CreateLocalityHandlerTests
    {
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IIbgeRepository> repository;
        private CreateLocalityRequest request;
        private Fixture fixture;

        public CreateLocalityHandlerTests()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            repository = new Mock<IIbgeRepository>();

            fixture = new Fixture();
        }

        [TestMethod]
        public void Deve_retornar_InvalidRequest_com_status_code_400_ao_informar_ibgeId_invalido()
        {
            #region Arrange

            var random = new Random();
            int ibgeId = random.Next(1000000);

            request = new CreateLocalityRequest(ibgeId.ToString(), "Cidade Fake", "RJ");

            #endregion

            #region Act

            var response = new CreateLocalityHandler(unitOfWork.Object, repository.Object).Handle(request, CancellationToken.None).GetAwaiter().GetResult();

            #endregion

            #region Assert

            Assert.IsInstanceOfType(response, typeof(LocalityInvalidRequest));
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("Requisição inválida. Por favor, valide os dados informados.", response.Message);
            Assert.IsTrue(response.Errors.Count() > 0);

            #endregion

        }

        [TestMethod]
        public void Deve_retornar_InvalidRequest_com_status_code_400_ao_informar_cidade_nula()
        {
            #region Arrange

            var random = new Random();
            int ibgeId = random.Next(1000000, 1000000);

            request = new CreateLocalityRequest(ibgeId.ToString(), null, "RJ");

            #endregion

            #region Act

            var response = new CreateLocalityHandler(unitOfWork.Object, repository.Object).Handle(request, CancellationToken.None).GetAwaiter().GetResult();

            #endregion

            #region Assert

            Assert.IsInstanceOfType(response, typeof(LocalityInvalidRequest));
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("Requisição inválida. Por favor, valide os dados informados.", response.Message);
            Assert.IsTrue(response.Errors.Count() > 0);

            #endregion

        }

        [TestMethod]
        public void Deve_retornar_InvalidRequest_com_status_code_400_ao_informar_estado_nulo()
        {
            #region Arrange

            var random = new Random();
            int ibgeId = random.Next(1000000, 1000000);

            request = new CreateLocalityRequest(ibgeId.ToString(), "Cidade Fake", null);

            #endregion

            #region Act

            var response = new CreateLocalityHandler(unitOfWork.Object, repository.Object).Handle(request, CancellationToken.None).GetAwaiter().GetResult();

            #endregion

            #region Assert

            Assert.IsInstanceOfType(response, typeof(LocalityInvalidRequest));
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("Requisição inválida. Por favor, valide os dados informados.", response.Message);
            Assert.IsTrue(response.Errors.Count() > 0);

            #endregion

        }

        [TestMethod]
        public void Deve_retornar_CodeAlreadyRegistered_com_status_code_409_ao_informar_ibgeId_ja_cadastrado()
        {
            #region Arrange

            var random = new Random();
            int ibgeId = random.Next(1000000, 1000000);

            request = new CreateLocalityRequest(ibgeId.ToString(), "Cidade Fake", "RJ");

            repository.Setup(x => x.IsIbgeCodeRegisteredAsync(request.IbgeId)).ReturnsAsync(true);

            #endregion

            #region Act

            var response = new CreateLocalityHandler(unitOfWork.Object, repository.Object).Handle(request, CancellationToken.None).GetAwaiter().GetResult();

            #endregion

            #region Assert

            Assert.IsInstanceOfType(response, typeof(CodeAlreadyRegistered));
            Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode);
            Assert.AreEqual("Já existe uma localidade com este código do IBGE cadastrado.", response.Message);

            #endregion

        }

        [TestMethod]
        public void Deve_retornar_CreatedSuccessfully_com_status_code_200_ao_cadastrar_localidade_com_sucesso()
        {
            #region Arrange

            var random = new Random();
            int ibgeId = random.Next(1000000, 1000000);

            request = new CreateLocalityRequest(ibgeId.ToString(), "Cidade Fake", "RJ");

            repository.Setup(x => x.IsIbgeCodeRegisteredAsync(request.IbgeId)).ReturnsAsync(false);
            repository.Setup(x => x.AddAsync(It.IsAny<Ibge>())).Returns(Task.CompletedTask);

            #endregion

            #region Act

            var response = new CreateLocalityHandler(unitOfWork.Object, repository.Object).Handle(request, CancellationToken.None).GetAwaiter().GetResult();

            #endregion

            #region Assert

            Assert.IsInstanceOfType(response, typeof(CreatedLocalitySuccessfully));
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual("Localidade criada com sucesso.", response.Message);

            #endregion

        }
    }
}
