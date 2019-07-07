using SimpleWebScraper.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SimpleWebScraper.Builders
{
    class ScrapeCriteriaBuilder
    {

        private string _data;
        private string _regex;
        private RegexOptions _regexOptions;
        private List<ScrapeCriteriaPart> _parts;


        public ScrapeCriteriaBuilder()
        {
            SetDefaults();
        }

        private void SetDefaults()
        {
            _data = string.Empty;
            _regex = string.Empty;
            _regexOptions = RegexOptions.None;
            _parts = new List<ScrapeCriteriaPart>();
        }

        public ScrapeCriteriaBuilder WithData(string data)
        {
            _data = data;
            return this;
        }
        public ScrapeCriteriaBuilder WithRegex(string regex)
        {
            _regex = regex;
            return this;
        }
        public ScrapeCriteriaBuilder WithRegexOption(RegexOptions regexOptions)
        {
            _regexOptions = regexOptions;
            return this;
        }
        public ScrapeCriteriaBuilder WithParts(ScrapeCriteriaPart parts)
        {
            _parts.Add(parts);
            return this;
        }

        public ScrapeCriteria Build()
        {
            ScrapeCriteria scrapeCriteria = new ScrapeCriteria();
            scrapeCriteria.Data = _data;
            scrapeCriteria.Regex = _regex;
            scrapeCriteria.RegexOption = _regexOptions;
            scrapeCriteria.Parts = _parts;
            return scrapeCriteria;
        }



    }
}
