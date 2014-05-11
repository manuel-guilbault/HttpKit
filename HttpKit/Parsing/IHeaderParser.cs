using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpKit.Parsing
{
    /// <summary>
    /// Interface for a generic HTTP header parser.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IHeaderParser<TResult>
    {
        /// <summary>
        /// Parse a header using a given tokenizer.
        /// </summary>
        /// <param name="tokenizer"></param>
        /// <returns>The header value.</returns>
        /// <exception cref="ArgumentNullException">If tokenizer is null.</exception>
        /// <exception cref="ParsingException">If a parsing error occurs.</exception>
        TResult Parse(Tokenizer tokenizer);
    }
}
