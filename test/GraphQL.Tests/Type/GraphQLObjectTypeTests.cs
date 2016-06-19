﻿namespace GraphQL.Tests.Type
{
    using Exceptions;
    using GraphQL.Type;
    using NUnit.Framework;

    public class GraphQLObjectTypeTests
    {
        private GraphQLObjectType<TestModel> type;

        [Test]
        public void Name_HasCorrectName()
        {
            Assert.AreEqual("Test", type.Name);
        }

        [Test]
        public void ToString_ReturnsName()
        {
            Assert.AreEqual("Test", type.ToString());
        }

        [Test]
        public void Description_HasCorrectDescription()
        {
            Assert.AreEqual("Test description", type.Description);
        }

        [Test]
        public void AddField_TwoResolversWithSameNames_ThrowsException()
        {
            var exception = Assert.Throws<GraphQLException>(new TestDelegate(() =>
            {
                type.Field("A", () => "x");
                type.Field("A", () => "y");
            }));

            Assert.AreEqual("Can't insert two fields with the same name.", exception.Message);
        }

        [Test]
        public void AddField_OneResolverAndOnveAcessorWithSameNames_ThrowsException()
        {
            var exception = Assert.Throws<GraphQLException>(new TestDelegate(() =>
            {
                type.Field("A", () => "x");
                type.Field("A", model => model.Test);
            }));

            Assert.AreEqual("Can't insert two fields with the same name.", exception.Message);
        }

        [Test]
        public void AddField_TwoAcessorsWithSameNames_ThrowsException()
        {
            var exception = Assert.Throws<GraphQLException>(new TestDelegate(() =>
            {
                type.Field("A", model => model.Test);
                type.Field("A", model => model.Test);
            }));

            Assert.AreEqual("Can't insert two fields with the same name.", exception.Message);
        }

        [SetUp]
        public void SetUp()
        {
            this.type = new GraphQLObjectType<TestModel>("Test", "Test description");
        }

        public class TestModel
        {
            public int Test { get; set; }
        }
    }
}
