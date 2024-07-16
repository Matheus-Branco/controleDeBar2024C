using ControleDeBar.Dominio.ModuloConta;
using ControleDeBar.Infra.Orm.Compartilhado;
using ControleDeBar.Infra.Orm.ModuloConta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeBar.Testes.Integracao.ModuloConta
{
    [TestClass]
    [TestCategory("Testes de Integração de Conta")]
    public class RepositorioContaEmOrmTests
    {
        RepositorioContaEmOrm repositorioConta;
        ControleDeBarDbContext dbContext;

        [TestInitialize]
        public void ConfigurarTestes()
        {
            dbContext = new ControleDeBarDbContext();
            repositorioConta = new RepositorioContaEmOrm(dbContext);

            dbContext.Contas.RemoveRange(dbContext.Contas);
        }

        [TestMethod]
        public void Deve_Inserir_Conta_Corretamente()
        {
            // Arrange
            Conta novaConta = new Conta();

            // act
            repositorioConta.Inserir(novaConta);

            // Assert
            Conta contaSelecionada = repositorioConta.SelecionarPorId(novaConta.Id);

            Assert.AreEqual(novaConta, contaSelecionada);
        }

        [TestMethod]
        public void Deve_Editar_Conta_Corretamente()
        {
            // Arrange 
            Conta contaOriginal = new Conta();

            repositorioConta.Inserir(contaOriginal);

            Conta contaParaAtualizacao = repositorioConta.SelecionarPorId(contaOriginal.Id);

            contaParaAtualizacao.Titular = "";

            // Act
            repositorioConta.Editar(contaOriginal, contaParaAtualizacao);

            // Assert
            Assert.AreEqual(contaOriginal, contaParaAtualizacao);
        }

        [TestMethod]
        public void Deve_Excluir_Conta_Corretamente()
        {
            // Arrange
            Conta conta = new Conta();

            repositorioConta.Inserir(conta);

            // Act
            repositorioConta.Excluir(conta);

            // Assert
            Conta contasSelecionada = repositorioConta.SelecionarPorId(conta.Id);

            Assert.IsNull(contasSelecionada);
        }

        [TestMethod]
        public void Deve_Selecionar_Todos_Corretamente()
        {
            // Arrange
            List<Conta> contasParaInserir =
            [
                new Conta(),
                new Conta(),
                new Conta()
            ];

            foreach (Conta conta in contasParaInserir)
                repositorioConta.Inserir(conta);

            // Act
            List<Conta> contasSelecionadas = repositorioConta.SelecionarContas();

            // Assert
            CollectionAssert.AreEqual(contasParaInserir, contasSelecionadas);
        }
    }
}
