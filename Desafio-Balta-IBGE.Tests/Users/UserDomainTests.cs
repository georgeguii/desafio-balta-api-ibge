using Desafio_Balta_IBGE.Domain.Models;
using Desafio_Balta_IBGE.Domain.ValueObjects;
using Desafio_Balta_IBGE.Shared.Exceptions;

namespace Desafio_Balta_IBGE.Tests.Users
{
    [TestClass]
    public class UserDomainTests
    {
        private User __user;
        public UserDomainTests()
        {
            
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Deve_retornar_NullReferenceException_ao_informar_usuario_nulo()
        {
            #region Arrange

            __user = null;

            #endregion

            #region Act & Assert

            var name = __user.Name;

            #endregion
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParametersException))]
        public void Deve_retornar_InvalidParametersException_ao_informar_dados_nulos()
        {
            #region Arrange

            __user = new User(name: null, password: null, email: null);

            #endregion

            #region Act & Assert



            #endregion
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParametersException))]
        public void Deve_retornar_InvalidParametersException_ao_informar_senha_nula()
        {
            #region Arrange

            __user = new User(name: "Nome", password: null, email: new Email("Email"));

            #endregion

            #region Act & Assert



            #endregion
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParametersException))]
        public void Deve_retornar_InvalidParametersException_ao_informar_email_nulo()
        {
            #region Arrange

            __user = new User(name: "Nome", password: new Password("Senha"), email: null);

            #endregion

            #region Act & Assert

           

            #endregion
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParametersException))]
        public void Deve_retornar_InvalidParametersException_ao_informar_email_com_parametro_nulo()
        {
            #region Arrange

            __user = new User(name: "Nome", password: new Password("Senha"), email: new Email(null));

            #endregion

            #region Act & Assert



            #endregion
        }
    }
}
