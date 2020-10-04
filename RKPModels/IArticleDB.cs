using System;
using System.Collections.Generic;
using System.Text;

namespace RKPModels
{
    public interface IArticleDB : IDisposable
    {
        /// <summary>
        /// Gets one productkey if found. If not found returns null. If multiple found throws.
        /// </summary>
        /// <param name="article">articlenum</param>
        /// <returns>pruductkey if found otherwise null</returns>
        string[] GetProductkey(string article);
        /// <summary>
        /// Search for a part of article number and returns a non null list of found product keys
        /// </summary>
        /// <param name="article">partial article number</param>
        /// <returns>a list of results</returns>
        IList<string[]> SearchProductkey(string article);
    }
}
