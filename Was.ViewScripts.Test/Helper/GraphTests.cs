using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Was.ViewScripts.Helpers;

namespace Was.ViewScripts.Test.Helper
{
    [TestClass]
    public class GraphTests
    {
        [TestMethod]
        public void TopologicalOrder_3Elements_ReturnValidOrder()
        {
            var g = new Graph<string>();

            g.AddEdge("jquery", "bootstrap");
            g.AddEdge("jquery", "otherjs");

            var result = g.SortTopological();

            Assert.AreEqual("jquery", result.First());
        }

        [TestMethod]
        public void TopologicalOrder_3ComplesElements_ReturnValidOrder()
        {
            var g = new Graph<string>();

            g.AddEdge("jquery", "bootstrap");
            g.AddEdge("bootstrap", "otherjs");
            g.AddEdge("jquery", "otherjs");

            var result = g.SortTopological().ToList();

            Assert.AreEqual("jquery", result.First());
            Assert.AreEqual("bootstrap", result.Skip(1).First());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TopolgicalOrder_Cyclic_THrowsException()
        {
            var g = new Graph<string>();

            g.AddEdge("jquery", "bootstrap");
            g.AddEdge("bootstrap", "jquery");

            g.SortTopological().ToList();
        }
    }
}
