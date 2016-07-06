﻿namespace GraphQLCore.Tests.Schemas
{
    using GraphQLCore.Type;

    public enum FurColor
    {
        BROWN = 0,
        BLACK = 1,
        TAN = 2,
        SPOTTED = 3
    }

    public class ComplicatedArgs : GraphQLObjectType
    {
        public ComplicatedArgs() : base("ComplicatedArgs", "")
        {
            this.Field("intArgField", (int? intArg) => "");
            this.Field("nonNullIntArgField", (int nonNullIntArg) => "");
            this.Field("stringArgField", (string stringArg) => "");
            this.Field("booleanArgField", (bool booleanArg) => "");
            this.Field("enumArgField", (FurColor enumArg) => "");
            this.Field("floatArgField", (float floatArg) => "");
            this.Field("stringListArgField", (string[] stringListArg) => "");
        }
    }

    public class ComplicatedObject
    {
        public bool BooleanField { get; set; }
        public FurColor EnumField { get; set; }
        public float FloatField { get; set; }
        public int? IntField { get; set; }
        public int NonNullIntField { get; set; }
        public string StringField { get; set; }
        public string[] StringListField { get; set; }
    }

    public class ComplicatedObjectType : GraphQLObjectType<ComplicatedObject>
    {
        public ComplicatedObjectType() : base("ComplicatedObjectType", "")
        {
            this.Field("intField", e => e.IntField);
            this.Field("nonNullIntField", e => e.NonNullIntField);
            this.Field("stringField", e => e.StringListField);
            this.Field("booleanField", e => e.BooleanField);
            this.Field("enumField", e => e.EnumField);
            this.Field("floatField", e => e.FloatField);
            this.Field("stringListField", e => e.StringListField);
        }
    }

    public class FurColorEnum : GraphQLEnumType<FurColor>
    {
        public FurColorEnum() : base("FurColorEnum", "")
        {
        }
    }

    public class QueryRoot : GraphQLObjectType
    {
        public QueryRoot() : base("QueryRoot", "")
        {
            var complicatedArgs = new ComplicatedArgs();

            this.Field("complicatedArgs", () => complicatedArgs);
        }
    }

    public class TestSchema : GraphQLSchema
    {
        public TestSchema()
        {
            var queryRoot = new QueryRoot();

            this.AddKnownType(queryRoot);
            this.AddKnownType(new FurColorEnum());
            this.AddKnownType(new ComplicatedObjectType());
            this.AddKnownType(new ComplicatedArgs());
            this.Query(queryRoot);
        }
    }
}