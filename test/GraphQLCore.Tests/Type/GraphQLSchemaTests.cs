﻿namespace GraphQLCore.Tests.Type
{
    using Exceptions;
    using GraphQLCore.Type;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class GraphQLSchemaTests
    {
        private GraphQLSchema schema;

        [Test]
        public void Execute_MultipleOperationsNoOperationName_ThrowsAnError()
        {
            var exception = Assert.Throws<Exception>(new TestDelegate(() =>
                this.schema.Execute("query Example { hello } query OtherExample { hello }")));

            Assert.AreEqual("Must provide operation name if query contains multiple operations.", 
                exception.Message);
        }

       

        [Test]
        public void Execute_GenericObjectWithoutResolver_ThrowsException()
        {
            this.schema = new GraphQLSchema();
            var rootType = new GraphQLObjectType<TestType>("RootQueryType", "", this.schema);
            rootType.Field("hello", o => o.Hello);
            schema.SetRoot(rootType);

            var exception = Assert.Throws<GraphQLException>(
                new TestDelegate(() => this.schema.Execute("{ hello }")));

            Assert.AreEqual("GraphQLObjectType RootQueryType doesn't have a resolver", exception.Message);
        }


        [SetUp]
        public void SetUp()
        {
            this.schema = new GraphQLSchema();
            var rootType = new GraphQLObjectType("RootQueryType", "", this.schema);
            rootType.Field("hello", () => "world");
            rootType.Field("test",  () => "test");

            schema.SetRoot(rootType);
        }

        public class TestType
        {
            public string Hello { get; set; }
            public string Test { get; set; }
        }
    }
}