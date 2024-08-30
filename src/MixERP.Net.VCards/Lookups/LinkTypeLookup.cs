using System;
using System.Collections.Generic;
using System.Linq;
using MixERP.Net.VCards.Types;

namespace MixERP.Net.VCards.Lookups
{
    public static class LinkTypeLookup
    {
        private static readonly Dictionary<LinkType, string> Lookup = new Dictionary<LinkType, string>
        {
            {LinkType.Preferred, "PREF"},
            {LinkType.Home, "HOME"},
            {LinkType.Work, "WORK"}
        };

        public static string ToVCardString(this LinkType linkType)
        {
            return Lookup[linkType];
        }

        public static LinkType Parse(string linkType)
        {
            return string.IsNullOrWhiteSpace(linkType) ? LinkType.Home : Lookup.FirstOrDefault(x => string.Equals(x.Value, linkType, StringComparison.OrdinalIgnoreCase)).Key;
        }
    }
}