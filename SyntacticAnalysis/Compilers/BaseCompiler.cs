using Heraldry.Blazon.Vocabulary;
using Heraldry.Blazon.Vocabulary.Entries;
using Heraldry.LexicalAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldry.SyntacticAnalysis.Compilers
{
    public abstract class BaseCompiler
    {
        private RootCompiler root;

        protected RootCompiler Compilers { get { return root; } }

        internal BaseCompiler(RootCompiler root)
        {
            this.root = root;
        }

        protected Token PopToken()
        {
            return root.PopToken();
        }

        protected Token PopTokenAs(DefinitionType type, object subtype = null)
        {
            Token token = PeekToken();
            if (!TokenIs(token, type, subtype))
            {
                throw new ExpectedTokenNotFoundException(type, subtype);
            }


            return PopToken();
        }

        protected List<T> PopList<T, TDef>(DefinitionType type, Func<TDef, T> defToVal) where TDef : class, IDefinition
        {
            return PopList(type, null, defToVal);
        }

        protected List<T> PopList<T, TDef>(DefinitionType type, object subtype, Func<TDef, T> defToVal) where TDef : class, IDefinition
        {
            List<T> list = new List<T>
            {
                defToVal(PopDefinition<TDef>(type, subtype))
            };

            while (NextTokenIs(DefinitionType.Separator, Separator.Comma))
            {
                PopToken();

                list.Add(defToVal(PopDefinition<TDef>(type, subtype)));
            }

            if (NextTokenIs(DefinitionType.KeyWord, KeyWord.And))
            {
                PopToken();
                list.Add(defToVal(PopDefinition<TDef>(type, subtype)));


            }
            else if (list.Count > 1)
            {
                throw new ExpectedTokenNotFoundException(DefinitionType.KeyWord, KeyWord.And);
            }

            return list;
        }


        protected TDefinition PopDefinition<TDefinition>(DefinitionType type, object subtype = null) where TDefinition : class, IDefinition
        {
            Token token = PopTokenAs(type, subtype);
            TDefinition definition = token.Definition as TDefinition;

            return definition;
        }

        protected Token PeekToken(int offset = 0)
        {
            return root.PeekToken(offset);
        }

        protected bool NextTokenIs(DefinitionType type, object subtype = null)
        {
            return TokenIs(PeekToken(), type, subtype);
        }

        protected bool NextTokenIs(params TokenType[] tokenTypes)
        {
            return TokenIs(PeekToken(), tokenTypes);
        }

        /// <summary>
        /// Checks whether token type matches expected type and if provided, if tokens subtype matches expected subtype
        /// </summary>
        protected bool TokenIs(Token token, DefinitionType expectedType, object expSubtype = null)
        {
            return token != null && token.Type == expectedType && (expSubtype == null || expSubtype.Equals(token.Subtype));
        }

        protected bool TokenIs(Token token, params TokenType[] expectedTypes)
        {
            if (expectedTypes.Length == 0)
            {
                throw new ArgumentException("Token type check needs at least one expected type"); ;
            }
            if (token == null)
            {
                return false;
            }

            return expectedTypes.Any(type =>
            {
                return token.Type == type.Type &&
                    (type.Subtype == null || type.Subtype.Equals(token.Subtype));
            });
        }
    }
}
