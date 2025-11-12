using oficina.Models;
using oficina.Models.Repositories;

namespace Oficina.Teste
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void IncluirCliente()
        {
            repositoryOficina _Rep = new repositoryOficina();

            Cliente oCli = new Cliente();

            oCli.Nome = "Pedro Lopes Saturnino";
            oCli.Telefone = "99999-9999";
            oCli.Email = "pedricks123@gmail.com";
            _Rep.Incluir(oCli);
            Assert.Pass();
        }

        [Test]
        public void AlterarCliente()
        {
            repositoryOficina _Rep = new repositoryOficina();
            Cliente oCli = new Cliente();
            oCli.ClienteId = 13;
            oCli.Nome = "Christian Matheus Silva";
            oCli.Telefone = "98888-8888";
            oCli.Email = "teste341@gmail.com";
            _Rep.Alterar(oCli);
            Assert.Pass();
        }

        [Test]
        public void ExcluirCliente()
        {
            repositoryOficina _Rep = new repositoryOficina();
            Cliente oCli = new Cliente();
            oCli.ClienteId = 17; 
            _Rep.Excluir(oCli.ClienteId);
            Assert.Pass();
        }

        [Test]
        public void SelecionarCliente()
        {
            repositoryOficina _Rep = new repositoryOficina();
            Cliente oCli = _Rep.Selecionar(13);
            Assert.Pass();
        }

        public void SelecionarTodos()
        {
            repositoryOficina _Rep = new repositoryOficina();
            List<Cliente> oLista = _Rep.SelecionarTodos();
            Assert.Pass();
        }
    }
}