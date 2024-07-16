using ControleDeBar.Dominio.ModuloProduto;
using ControleDeBar.Infra.Orm.Compartilhado;
using ControleDeBar.Infra.Orm.ModuloProduto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeBar.Testes.Integracao.ModuloProduto
{
    [TestClass]
    [TestCategory("Testes de Integração de Produto")]
    public class RepositorioProdutoEmOrmTests
    {
        RepositorioProdutoEmOrm repositorioProduto;
        ControleDeBarDbContext dbContext;

        [TestInitialize]
        public void ConfigurarTestes()
        {
            dbContext = new ControleDeBarDbContext();
            repositorioProduto = new RepositorioProdutoEmOrm(dbContext);

            dbContext.Produtos.RemoveRange(dbContext.Produtos);
        }

        [TestMethod]
        public void Deve_Inserir_Mesa_Corretamente()
        {
            // Arrange
            Produto novoProduto = new Produto();

            // act
            repositorioProduto.Inserir(novoProduto);

            // Assert
            Produto produtoSelecionado = repositorioProduto.SelecionarPorId(novoProduto.Id);

            Assert.AreEqual(novoProduto, produtoSelecionado);
        }

        [TestMethod]
        public void Deve_Editar_Mesa_Corretamente()
        {
            // Arrange 
            Produto produtoOriginal = new Produto();

            repositorioProduto.Inserir(produtoOriginal);

            Produto produtoParaAtualizacao = repositorioProduto.SelecionarPorId(produtoOriginal.Id);

            produtoParaAtualizacao.Nome = "Coca 2L";

            // Act
            repositorioProduto.Editar(produtoOriginal, produtoParaAtualizacao);

            // Assert
            Assert.AreEqual(produtoOriginal, produtoParaAtualizacao);
        }

        [TestMethod]
        public void Deve_Excluir_Mesa_Corretamente()
        {
            // Arrange
            Produto produto = new Produto();

            repositorioProduto.Inserir(produto);

            // Act
            repositorioProduto.Excluir(produto);

            // Assert
            Produto produtoSelecionada = repositorioProduto.SelecionarPorId(produto.Id);

            Assert.IsNull(produtoSelecionada);
        }

        [TestMethod]
        public void Deve_Selecionar_Todos_Corretamente()
        {
            // Arrange
            List<Produto> produtosParaInserir =
            [
                new Produto(),
                new Produto(),
                new Produto()
            ];

            foreach (Produto produto in produtosParaInserir)
                repositorioProduto.Inserir(produto);

            // Act
            List<Produto> produtosSelecionados = repositorioProduto.SelecionarTodos();

            // Assert
            CollectionAssert.AreEqual(produtosParaInserir, produtosSelecionados);
        }
    }
}
