// <copyright file="WorkingExpressionSet.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using IX.Math.ExpressionState;
using IX.Math.Extraction;
using IX.Math.Generators;
using IX.Math.Nodes;
using IX.Math.Registration;
using IX.System.Collections.Generic;

namespace IX.Math
{
    internal class WorkingExpressionSet
    {
        // Definition
#pragma warning disable SA1401 // Fields must be private
        internal MathDefinition Definition;

        internal string[] AllOperatorsInOrder;
        internal string[] AllSymbols;
        internal Regex FunctionRegex;

        // Initial data
        internal string InitialExpression;
        internal CancellationToken CancellationToken;

        // Working domain
        internal Dictionary<string, ConstantNodeBase> ConstantsTable;
        internal Dictionary<string, string> ReverseConstantsTable;
        internal Dictionary<string, ExpressionSymbol> SymbolTable;
        internal Dictionary<string, string> ReverseSymbolTable;
        internal string Expression;
        internal NodeBase Body;
        internal IParameterRegistry ParameterRegistry;

        // Scrap
        internal LevelDictionary<string, Type> UnaryOperators;
        internal LevelDictionary<string, Type> BinaryOperators;

        internal Dictionary<string, Type> NonaryFunctions;
        internal Dictionary<string, Type> UnaryFunctions;
        internal Dictionary<string, Type> BinaryFunctions;
        internal Dictionary<string, Type> TernaryFunctions;

        internal LevelDictionary<Type, IConstantsExtractor> Extractors;

        // Results
        internal object ValueIfConstant;
        internal bool Success = false;
        internal bool InternallyValid = false;
        internal bool Constant = false;
        internal bool PossibleString = false;
#pragma warning restore SA1401 // Fields must be private

        private IEnumerable<Assembly> assembliesForFunctions;

        internal WorkingExpressionSet(
            string expression,
            MathDefinition mathDefinition,
            IEnumerable<Assembly> assembliesForFunctions,
            Dictionary<string, Type> nonaryFunctions,
            Dictionary<string, Type> unaryFunctions,
            Dictionary<string, Type> binaryFunctions,
            Dictionary<string, Type> ternaryFunctions,
            LevelDictionary<Type, IConstantsExtractor> extractors,
            CancellationToken cancellationToken)
        {
            this.ParameterRegistry = new StandardParameterRegistry();
            this.ConstantsTable = new Dictionary<string, ConstantNodeBase>();
            this.ReverseConstantsTable = new Dictionary<string, string>();
            this.SymbolTable = new Dictionary<string, ExpressionSymbol>();
            this.ReverseSymbolTable = new Dictionary<string, string>();

            this.InitialExpression = expression;
            this.CancellationToken = cancellationToken;
            this.Expression = expression;
            this.Definition = mathDefinition;

            this.assembliesForFunctions = assembliesForFunctions;

            this.AllOperatorsInOrder = new[]
            {
                this.Definition.GreaterThanOrEqualSymbol,
                this.Definition.LessThanOrEqualSymbol,
                this.Definition.GreaterThanSymbol,
                this.Definition.LessThanSymbol,
                this.Definition.NotEqualsSymbol,
                this.Definition.EqualsSymbol,
                this.Definition.XorSymbol,
                this.Definition.OrSymbol,
                this.Definition.AndSymbol,
                this.Definition.AddSymbol,
                this.Definition.SubtractSymbol,
                this.Definition.DivideSymbol,
                this.Definition.MultiplySymbol,
                this.Definition.PowerSymbol,
                this.Definition.LeftShiftSymbol,
                this.Definition.RightShiftSymbol,
                this.Definition.NotSymbol,
            };

            this.NonaryFunctions = nonaryFunctions;
            this.UnaryFunctions = unaryFunctions;
            this.BinaryFunctions = binaryFunctions;
            this.TernaryFunctions = ternaryFunctions;

            this.Extractors = extractors;

            this.FunctionRegex = new Regex($@"(?'functionName'.*?){Regex.Escape(this.Definition.Parentheses.Item1)}(?'expression'.*?){Regex.Escape(this.Definition.Parentheses.Item2)}");
        }

        internal void Initialize()
        {
            IEnumerable<string> operators = this.AllOperatorsInOrder
                .OrderByDescending(p => p.Length)
                .Where(p => this.AllOperatorsInOrder.Any(q => q.Length < p.Length && p.Contains(q)));

            var i = 1;
            foreach (var op in operators.OrderByDescending(p => p.Length))
            {
                var s = $"@op{i}@";

                this.Expression = this.Expression.Replace(op, s);

                var allIndex = Array.IndexOf(this.AllOperatorsInOrder, op);
                if (allIndex != -1)
                {
                    this.AllOperatorsInOrder[allIndex] = s;
                }

                if (this.Definition.AddSymbol == op)
                {
                    this.Definition.AddSymbol = s;
                }

                if (this.Definition.AndSymbol == op)
                {
                    this.Definition.AndSymbol = s;
                }

                if (this.Definition.DivideSymbol == op)
                {
                    this.Definition.DivideSymbol = s;
                }

                if (this.Definition.NotEqualsSymbol == op)
                {
                    this.Definition.NotEqualsSymbol = s;
                }

                if (this.Definition.EqualsSymbol == op)
                {
                    this.Definition.EqualsSymbol = s;
                }

                if (this.Definition.GreaterThanOrEqualSymbol == op)
                {
                    this.Definition.GreaterThanOrEqualSymbol = s;
                }

                if (this.Definition.GreaterThanSymbol == op)
                {
                    this.Definition.GreaterThanSymbol = s;
                }

                if (this.Definition.LessThanOrEqualSymbol == op)
                {
                    this.Definition.LessThanOrEqualSymbol = s;
                }

                if (this.Definition.LessThanSymbol == op)
                {
                    this.Definition.LessThanSymbol = s;
                }

                if (this.Definition.MultiplySymbol == op)
                {
                    this.Definition.MultiplySymbol = s;
                }

                if (this.Definition.NotSymbol == op)
                {
                    this.Definition.NotSymbol = s;
                }

                if (this.Definition.OrSymbol == op)
                {
                    this.Definition.OrSymbol = s;
                }

                if (this.Definition.PowerSymbol == op)
                {
                    this.Definition.PowerSymbol = s;
                }

                if (this.Definition.LeftShiftSymbol == op)
                {
                    this.Definition.LeftShiftSymbol = s;
                }

                if (this.Definition.RightShiftSymbol == op)
                {
                    this.Definition.RightShiftSymbol = s;
                }

                if (this.Definition.SubtractSymbol == op)
                {
                    this.Definition.SubtractSymbol = s;
                }

                if (this.Definition.XorSymbol == op)
                {
                    this.Definition.XorSymbol = s;
                }

                i++;
            }

            // Operator string interpretation support
            // ======================================

            // Binary operators
            var binaryOperators = new LevelDictionary<string, Type>
            {
                // First tier - Comparison and equation operators
                { this.Definition.GreaterThanOrEqualSymbol, typeof(Nodes.Operations.Binary.GreaterThanOrEqualNode), 10 },
                { this.Definition.LessThanOrEqualSymbol, typeof(Nodes.Operations.Binary.LessThanOrEqualNode), 10 },
                { this.Definition.GreaterThanSymbol, typeof(Nodes.Operations.Binary.GreaterThanNode), 10 },
                { this.Definition.LessThanSymbol, typeof(Nodes.Operations.Binary.LessThanNode), 10 },
                { this.Definition.NotEqualsSymbol, typeof(Nodes.Operations.Binary.NotEqualsNode), 10 },
                { this.Definition.EqualsSymbol, typeof(Nodes.Operations.Binary.EqualsNode), 10 },

                // Second tier - Logical operators
                { this.Definition.OrSymbol, typeof(Nodes.Operations.Binary.OrNode), 20 },
                { this.Definition.XorSymbol, typeof(Nodes.Operations.Binary.XorNode), this.Definition.OperatorPrecedenceStyle == OperatorPrecedenceStyle.CStyle ? 21 : 20 },
                { this.Definition.AndSymbol, typeof(Nodes.Operations.Binary.AndNode), this.Definition.OperatorPrecedenceStyle == OperatorPrecedenceStyle.CStyle ? 22 : 20 },

                // Third tier - Arithmetic second-rank operators
                { this.Definition.AddSymbol, typeof(Nodes.Operations.Binary.AddNode), 30 },
                { this.Definition.SubtractSymbol, typeof(Nodes.Operations.Binary.SubtractNode), 30 },

                // Fourth tier - Arithmetic first-rank operators
                { this.Definition.DivideSymbol, typeof(Nodes.Operations.Binary.DivideNode), 40 },
                { this.Definition.MultiplySymbol, typeof(Nodes.Operations.Binary.MultiplyNode), 40 },

                // Fifth tier - Power operator
                { this.Definition.PowerSymbol, typeof(Nodes.Operations.Binary.PowerNode), 50 },

                // Sixth tier - Bitwise shift operators
                { this.Definition.LeftShiftSymbol, typeof(Nodes.Operations.Binary.LeftShiftNode), 60 },
                { this.Definition.RightShiftSymbol, typeof(Nodes.Operations.Binary.RightShiftNode), 60 },
            };

            this.BinaryOperators = binaryOperators;

            // Unary operators
            var unaryOperators = new LevelDictionary<string, Type>
            {
                // First tier - Negation and inversion
                { this.Definition.SubtractSymbol, typeof(Nodes.Operations.Unary.SubtractNode), 1 },
                { this.Definition.NotSymbol, typeof(Nodes.Operations.Unary.NotNode), 1 },
            };

            this.UnaryOperators = unaryOperators;

            // All symbols
            this.AllSymbols = this.AllOperatorsInOrder
                .Union(new[]
                {
                    this.Definition.ParameterSeparator,
                    this.Definition.Parentheses.Item1,
                    this.Definition.Parentheses.Item2,
                })
                .ToArray();

            // Special symbols

            // Euler-Napier constant (e)
            ConstantsGenerator.GenerateNamedNumericSymbol(
                this.ConstantsTable,
                this.ReverseConstantsTable,
                "e",
                global::System.Math.E);

            // Archimedes-Ludolph constant (pi)
            ConstantsGenerator.GenerateNamedNumericSymbol(
                this.ConstantsTable,
                this.ReverseConstantsTable,
                "π",
                global::System.Math.PI,
                $"{this.Definition.SpecialSymbolIndicators.Item1}pi{this.Definition.SpecialSymbolIndicators.Item2}");

            // Golden ratio
            ConstantsGenerator.GenerateNamedNumericSymbol(
                this.ConstantsTable,
                this.ReverseConstantsTable,
                "φ",
                1.6180339887498948,
                $"{this.Definition.SpecialSymbolIndicators.Item1}phi{this.Definition.SpecialSymbolIndicators.Item2}");

            // Bernstein constant
            ConstantsGenerator.GenerateNamedNumericSymbol(
                this.ConstantsTable,
                this.ReverseConstantsTable,
                "β",
                0.2801694990238691,
                $"{this.Definition.SpecialSymbolIndicators.Item1}beta{this.Definition.SpecialSymbolIndicators.Item2}");

            // Euler-Mascheroni constant
            ConstantsGenerator.GenerateNamedNumericSymbol(
                this.ConstantsTable,
                this.ReverseConstantsTable,
                "γ",
                0.5772156649015328,
                $"{this.Definition.SpecialSymbolIndicators.Item1}gamma{this.Definition.SpecialSymbolIndicators.Item2}");

            // Gauss-Kuzmin-Wirsing constant
            ConstantsGenerator.GenerateNamedNumericSymbol(
                this.ConstantsTable,
                this.ReverseConstantsTable,
                "λ",
                0.3036630028987326,
                $"{this.Definition.SpecialSymbolIndicators.Item1}lambda{this.Definition.SpecialSymbolIndicators.Item2}");
        }
    }
}