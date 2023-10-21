using Desafio_Balta_IBGE.Domain.Models;
using Desafio_Balta_IBGE.Domain.ValueObjects;

namespace Desafio_Balta_IBGE.Tests.IbgeModel
{
    [TestClass]
    public class IbgeDomainTests
    {
        private Ibge _ibge;

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Deve_retornar_NullReferenceException_ao_informar_ibgeId_nulo()
        {
            #region Arrange

            _ibge = null;

            #endregion

            #region Act & Assert

            string? id = _ibge.IbgeId;

            #endregion
        }

        [TestMethod]
        public void Deve_retornar_erro_ao_informar_ibgeId_nulo()
        {
            #region Arrange

            _ibge = new Ibge(null, "Cidade Teste", "RJ");

            #endregion

            #region Act



            #endregion

            #region Assert

            Assert.IsFalse(_ibge.IsValid);

            #endregion
        }

        [TestMethod]
        public void Deve_retornar_erro_ao_informar_cidade_nula()
        {
            #region Arrange

            _ibge = new Ibge("1234567", null, "RJ");

            #endregion

            #region Act



            #endregion

            #region Assert

            Assert.IsFalse(_ibge.IsValid);

            #endregion
        }

        [TestMethod]
        public void Deve_retornar_erro_ao_informar_state_nulo()
        {
            #region Arrange

            _ibge = new Ibge("1234567", "Cidade Teste", null);

            #endregion

            #region Act



            #endregion

            #region Assert

            Assert.IsFalse(_ibge.IsValid);

            #endregion
        }

        [TestMethod]
        public void Deve_retornar_sucesso_ao_instanciar_ibge_corretamente()
        {
            #region Arrange

            _ibge = new Ibge("1234567", "Cidade Teste", "RJ");

            #endregion

            #region Act



            #endregion

            #region Assert

            Assert.IsTrue(_ibge.IsValid);

            #endregion
        }
    }
}
