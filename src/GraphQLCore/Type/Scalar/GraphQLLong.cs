﻿namespace GraphQLCore.Type.Scalar
{
    using Language.AST;
    using System;
    using Translation;
    using Utils;

    public class GraphQLLong : GraphQLScalarType
    {
        public GraphQLLong() : base(
            "Long",
            "The `Long` scalar type represents non-fractional signed whole numeric values. Int can represent values between -(2^63) and 2^63 - 1.")
        {
        }

        public override Result GetValueFromAst(GraphQLValue astValue, ISchemaRepository schemaRepository)
        {
            if (astValue.Kind == ASTNodeKind.IntValue)
            {
                decimal value;
                if (!decimal.TryParse(((GraphQLScalarValue)astValue).Value, out value))
                    return Result.Invalid;

                if (value <= long.MaxValue && value >= long.MinValue)
                    return new Result(Convert.ToInt64(value));
            }

            return Result.Invalid;
        }

        protected override GraphQLValue GetAst(object value, ISchemaRepository schemaRepository)
        {
            if (value is float || value is double)
                value = value.ToString().ParseLongOrGiveNull();

            if (!(value is long) && !(value is int))
                return null;

            return new GraphQLScalarValue(ASTNodeKind.IntValue)
            {
                Value = value.ToString()
            };
        }
    }
}