﻿using System;
using GraphQL.Language.AST;

namespace GraphQL.Type.Scalars
{
    public class GraphQLFloat : GraphQLScalarType
    {
        public GraphQLFloat(GraphQLSchema schema) : base("Float", 
            "The `Float` scalar type represents signed double-precision fractional " +
            "values as specified by " +
            "[IEEE 754](http://en.wikipedia.org/wiki/IEEE_floating_point). ",
            schema)
        {
        }
    }
}