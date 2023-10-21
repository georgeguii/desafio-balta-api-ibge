using Desafio_Balta_IBGE.Application.UseCases.Locality.Handler;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Request;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Response;
using Desafio_Balta_IBGE.Domain.Interfaces.IBGE;
using Desafio_Balta_IBGE.Domain.Interfaces.UnitOfWork;
using Moq;
using System.Net;

namespace Desafio_Balta_IBGE.Tests.Application.IbgeHandlers
{
    [TestClass]
    public class DeleteLocalityHandlerTests
    {
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IIbgeRepository> repository;
        private CreateLocalityRequest request;
        private const string characters = "abcdefghijklmnopqrstuvwxyz0123456789";

        public DeleteLocalityHandlerTests()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            repository = new Mock<IIbgeRepository>();
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

            Assert.IsInstanceOfType(response, typeof(InvalidRequest));
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

            Assert.IsInstanceOfType(response, typeof(InvalidRequest));
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("Requisição inválida. Por favor, valide os dados informados.", response.Message);
            Assert.IsTrue(response.Errors.Count() > 0);

            #endregion

        }

        [TestMethod]
        [DataRow("11")]
        [DataRow("32")]
        [DataRow("1C")]
        [DataRow("A")]
        [DataRow("K9")]
        [DataRow(null)]
        [DataRow("1A")]
        [DataRow("A@")]
        [DataRow("A!")]
        [DataRow("A.")]
        [DataRow("A?")]
        [DataRow("A.")]
        [DataRow("A_")]
        [DataRow("A-")]
        [DataRow("A#")]
        [DataRow("A*")]
        [DataRow("A+")]
        [DataRow("A=")]
        [DataRow("A'")]
        [DataRow("A\"")]
        public void Deve_retornar_InvalidRequest_com_status_code_400_ao_informar_estado_invalido(string state)
        {
            #region Arrange

            var random = new Random();
            int ibgeId = random.Next(1000000, 1000000);

            request = new CreateLocalityRequest(ibgeId.ToString(), "Cidade Fake", state);

            #endregion

            #region Act

            var response = new CreateLocalityHandler(unitOfWork.Object, repository.Object).Handle(request, CancellationToken.None).GetAwaiter().GetResult();

            #endregion

            #region Assert

            Assert.IsInstanceOfType(response, typeof(InvalidRequest));
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("Requisição inválida. Por favor, valide os dados informados.", response.Message);
            Assert.IsTrue(response.Errors.Count() > 0);

            #endregion

        }

        [TestMethod]
        [DataRow("1")]
        [DataRow("2")]
        [DataRow("3")]
        [DataRow("4")]
        [DataRow("5")]
        [DataRow("6")]
        [DataRow("7")]
        [DataRow("8")]
        [DataRow("9")]
        [DataRow("0")]
        [DataRow("111")]
        [DataRow("222")]
        [DataRow("333")]
        [DataRow("444")]
        [DataRow("555")]
        [DataRow("666")]
        [DataRow("777")]
        [DataRow("888")]
        [DataRow("999")]
        [DataRow("000")]
        public void Deve_retornar_InvalidRequest_com_status_code_400_ao_informar_estado_com_numeros_e_menores_e_maiores_que_2_digitos(string state)
        {
            #region Arrange

            var random = new Random();
            int ibgeId = random.Next(1000000, 1000000);

            request = new CreateLocalityRequest(ibgeId.ToString(), "Cidade Fake", state);

            #endregion

            #region Act

            var response = new CreateLocalityHandler(unitOfWork.Object, repository.Object).Handle(request, CancellationToken.None).GetAwaiter().GetResult();

            #endregion

            #region Assert

            Assert.IsInstanceOfType(response, typeof(InvalidRequest));
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("Requisição inválida. Por favor, valide os dados informados.", response.Message);
            Assert.IsTrue(response.Errors.Count() > 0);

            #endregion

        }

        [TestMethod]
        public void Deve_retornar_InvalidRequest_com_status_code_400_ao_informar_estado_com_letras_e_mais_de_2_digitos()
        {
            #region Arrange

            var random = new Random();
            int ibgeId = random.Next(1, 1000000);
            var tamanhoMax = 10;
            char[] resultado = new char[tamanhoMax];

            for (int i = 0; i < tamanhoMax; i++)
            {
                resultado[i] = characters[random.Next(characters.Length)];
            }

            request = new CreateLocalityRequest(ibgeId.ToString(), "Cidade Fake", resultado.ToString()!);

            #endregion

            #region Act

            var response = new CreateLocalityHandler(unitOfWork.Object, repository.Object).Handle(request, CancellationToken.None).GetAwaiter().GetResult();

            #endregion

            #region Assert

            Assert.IsInstanceOfType(response, typeof(InvalidRequest));
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("Requisição inválida. Por favor, valide os dados informados.", response.Message);
            Assert.IsTrue(response.Errors.Count() > 0);

            #endregion

        }

       
    }
}
