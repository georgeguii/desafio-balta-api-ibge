using Desafio_Balta_IBGE.Domain.Models;
using Desafio_Balta_IBGE.Domain.ValueObjects;

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

            string? name = __user.Name;

            #endregion
        }

        [TestMethod]
        public void Deve_retornar_erro_ao_informar_usuario_nome_nulo()
        {
            #region Arrange

            __user = new User(null, new Password("@Admin123"), new Email("email@email.com"), "Admin");

            #endregion

            #region Act



            #endregion

            #region Assert

            Assert.IsFalse(__user.IsValid);

            #endregion
        }

        [TestMethod]
        public void Deve_retornar_erro_ao_informar_value_object_password_nulo()
        {
            #region Arrange

            __user = new User("Teste", null, new Email("email@email.com"), "Admin");

            #endregion

            #region Act



            #endregion

            #region Assert

            Assert.Fail();

            #endregion
        }

        [TestMethod]
        public void Deve_retornar_erro_ao_informar_value_object_email_nulo()
        {
            #region Arrange

            __user = new User("Teste", new Password("@Admin123"), null, "Admin");

            #endregion

            #region Act



            #endregion

            #region Assert

            Assert.Fail();

            #endregion
        }

        [TestMethod]
        public void Deve_retornar_erro_ao_informar_role_nulo()
        {
            #region Arrange

            __user = new User("Teste", new Password("@Admin123"), new Email("email@email.com"), null);

            #endregion

            #region Act



            #endregion

            #region Assert

            Assert.Fail();

            #endregion
        }
    }
}
