using Desafio_Balta_IBGE.Application.UseCases.Locality.Handler;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Request;
using Desafio_Balta_IBGE.Domain.Interfaces.IBGE;
using Desafio_Balta_IBGE.Domain.Interfaces.UnitOfWork;
using Moq;

namespace Desafio_Balta_IBGE.Tests
{
    [TestClass]
    public class CreateLocalityTests
    {
        private Mock<IIbgeRepository> _ibgeRepository;
        private Mock<IUnitOfWork> _unitOfWork;

        public CreateLocalityTests(Mock<IIbgeRepository> ibgeRepository, Mock<IUnitOfWork> unitOfWork)
        {
            _ibgeRepository = ibgeRepository;
            _unitOfWork = unitOfWork;
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Deve_retornar_erro_ao_informar_estado_com_letras_e_mais_de_2_caracteres()
        {
            var request = new CreateLocalityRequest("1234567", "Cidade Fake", "Estado Fake");

            var response = new CreateLocalityHandler(_unitOfWork.Object, _ibgeRepository.Object).Handle(request, CancellationToken.None).GetAwaiter().GetResult();
        }
    }
}
