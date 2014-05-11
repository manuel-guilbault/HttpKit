using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpKit.Parsing
{
    /// <summary>
    /// Interface of a generic HTTP header reader.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IHeaderReader<T>
    {
        /// <summary>
        /// Read a HTTP header value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">If value is null.</exception>
        /// <exception cref="ParsingException">If a parsing error occurs.</exception>
        T Read(string value);
    }
}
