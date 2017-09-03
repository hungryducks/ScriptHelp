﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ScriptHelp.Scripts.Syntax
{
    /// <summary>
    /// PatternDefinition
    /// </summary>
    public class PatternDefinition
    {
        private readonly Regex _regex;
        private ExpressionType _expressionType = ExpressionType.Identifier;
        private readonly bool _isCaseSensitive = false;

        /// <summary>
        /// PatternDefinition
        /// </summary>
        /// <param name="regularExpression">regularExpression</param>
        public PatternDefinition(Regex regularExpression)
        {
            if (regularExpression == null)
                throw new ArgumentNullException("regularExpression");
            _regex = regularExpression;
        }

        /// <summary>
        /// PatternDefinition
        /// </summary>
        /// <param name="regexPattern">regexPattern</param>
        public PatternDefinition(string regexPattern)
        {
            if (String.IsNullOrEmpty(regexPattern))
                throw new ArgumentException("regex pattern must not be null or empty", "regexPattern");

            _regex = new Regex(regexPattern, RegexOptions.Compiled);
        }

        /// <summary>
        /// PatternDefinition
        /// </summary>
        /// <param name="tokens">tokens</param>
        public PatternDefinition(params string[] tokens)
            : this(true, tokens)
        {
        }

        /// <summary>
        /// PatternDefinition
        /// </summary>
        /// <param name="tokens">tokens</param>
        public PatternDefinition(IEnumerable<string> tokens)
            : this(true, tokens)
        {
        }

        /// <summary>
        /// PatternDefinition
        /// </summary>
        /// <param name="caseSensitive">caseSensitive</param>
        /// <param name="tokens">tokens</param>
        internal PatternDefinition(bool caseSensitive, IEnumerable<string> tokens)
        {
            if (tokens == null)
                throw new ArgumentNullException("tokens");

             caseSensitive = _isCaseSensitive;

            var regexTokens = new List<string>();

            foreach (var token in tokens)
            {
                var escaptedToken = Regex.Escape(token.Trim());

                if (escaptedToken.Length > 0)
                {
                    if (Char.IsLetterOrDigit(escaptedToken[0]))
                        regexTokens.Add(String.Format(@"\b{0}\b", escaptedToken));
                    else
                        regexTokens.Add(escaptedToken);
                }
            }

            string pattern = String.Join("|", regexTokens);
            var regexOptions = RegexOptions.Compiled;
            if (!caseSensitive)
                regexOptions = regexOptions | RegexOptions.IgnoreCase;
            _regex = new Regex(pattern, regexOptions);
        }

        /// <summary>
        /// ExpressionType
        /// </summary>
        internal ExpressionType ExpressionType 
        {
            get { return _expressionType; }
            set { _expressionType = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        internal bool IsCaseSensitive 
        {
            get { return _isCaseSensitive; }
        }

        /// <summary>
        /// Regex
        /// </summary>
        internal Regex Regex
        {
            get { return _regex; }
        }
    }
}