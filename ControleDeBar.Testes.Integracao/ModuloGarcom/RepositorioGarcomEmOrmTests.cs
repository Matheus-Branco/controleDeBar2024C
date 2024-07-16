using ControleDeBar.Dominio.ModuloGarcom;
using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Infra.Orm.Compartilhado;
using ControleDeBar.Infra.Orm.ModuloGarcom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeBar.Testes.Integracao.ModuloGarcom
{
    [TestClass]
    [TestCategory("Testes de Integração de Garçom")]
    public class RepositorioProdutoEmOrmTests
    {
        RepositorioGarcomEmOrm repositorioGarcom;
        ControleDeBarDbContext dbContext;

        [TestInitialize]
        public void ConfigurarTestes()
        {
            dbContext = new ControleDeBarDbContext();
            repositorioGarcom = new RepositorioGarcomEmOrm(dbContext);

            dbContext.Garcons.RemoveRange(dbContext.Garcons);
        }

        [TestMethod]
        public void Deve_Inserir_Garcom_Corretamente()
        {
            // Arrange
            Garcom novoGarcom = new Garcom("Matheus Jeremias Branco", "012-463-128.12");

            // act
            repositorioGarcom.Inserir(novoGarcom);

            // Assert
            Garcom garcomSelecionado = repositorioGarcom.SelecionarPorId(novoGarcom.Id);

            Assert.AreEqual(novoGarcom, garcomSelecionado);
        }

        [TestMethod]
        public void Deve_Editar_Garcom_Corretamente()
        {
            // Arrange 
            Garcom garcomOriginal = new Garcom("Matheus Jeremias Branco", "012-463-128.12");

            repositorioGarcom.Inserir(garcomOriginal);

            Garcom garcomParaAtualizacao = repositorioGarcom.SelecionarPorId(garcomOriginal.Id);

            garcomParaAtualizacao.Nome = "Miguel Jeremias Branco";
            garcomParaAtualizacao.CPF = "012-345-123.95";

            // Act
            repositorioGarcom.Editar(garcomOriginal, garcomParaAtualizacao);

            // Assert
            Assert.AreEqual(garcomOriginal, garcomParaAtualizacao);
        }

        [TestMethod]
        public void Deve_Excluir_Garcom_Corretamente()
        {
            // Arrange
            Garcom garcom = new Garcom("Matheus Jeremias Branco", "012-463-128.12");

            repositorioGarcom.Inserir(garcom);

            // Act
            repositorioGarcom.Excluir(garcom);

            // Assert
            Garcom garcomSelecionada = repositorioGarcom.SelecionarPorId(garcom.Id);

            Assert.IsNull(garcomSelecionada);
        }

        [TestMethod]
        public void Deve_Selecionar_Todos_Corretamente()
        {
            // Arrange
            List<Garcom> garconsParaInserir =
            [
                new Garcom("Matheus Jeremias Branco", "012-463-128.12"),
                new Garcom("Miguel Jeremias Branco", "012-345-123.95"),
            ];

            foreach (Garcom garcom in garconsParaInserir)
                repositorioGarcom.Inserir(garcom);

            // Act
            List<Garcom> garconsSelecionados = repositorioGarcom.SelecionarTodos();

            // Assert
            CollectionAssert.AreEqual(garconsParaInserir, garconsSelecionados);
        }
    }
}
