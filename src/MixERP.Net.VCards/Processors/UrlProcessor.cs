using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MixERP.Net.VCards.Extensions;
using MixERP.Net.VCards.Lookups;
using MixERP.Net.VCards.Models;
using MixERP.Net.VCards.Serializer;
using MixERP.Net.VCards.Types;

namespace MixERP.Net.VCards.Processors
{
    public static class UrlProcessor
    {
        public static string Serialize(VCard vcard)
        {
            if(vcard.Urls == null || vcard.Urls.Count() == 0)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();

            foreach (var link in vcard.Urls)
            {
                string url = link.Url.ToString();
                if (string.IsNullOrWhiteSpace(url))
                {
                    continue;
                }

                string type = link.Type.ToVCardString();

                string key = "URL";

                if (vcard.Version == VCardVersion.V4)
                {
                    if (link.Preference > 0)
                    {
                        key = key + ";PREF=" + link.Preference;
                    }
                }

                builder.Append(GroupProcessor.Serialize(key, vcard.Version, type, false, url));
            }

            return builder.ToString();
        }

        public static string SerializeLinks(IEnumerable<Link> links, string key, VCardVersion version)
        {
            var builder = new StringBuilder();
            if (links == null)
            {
                return string.Empty;
            }

            int preference = 0;

            foreach (var link in links)
            {
                if(link == null)
                {
                    continue;
                }

                preference++;

                string memberKey = key;

                memberKey = memberKey + ";PREF=" + preference;

                builder.Append(DefaultSerializer.GetVCardString(memberKey, link.Url.ToString(), false, version));
            }

            return builder.ToString();
        }

        public static void Parse(Token token, ref VCard vcard)
        {
            string url = token.Values[0];
            if (!string.IsNullOrWhiteSpace(url))
            {
                var link = new Link();
                var preference = token.AdditionalKeyMembers.FirstOrDefault(x => x.Key == "PREF");
                var type = token.AdditionalKeyMembers.FirstOrDefault(x => x.Key == "TYPE");

                link.Preference = preference.Value.ConvertTo<int>();
                link.Type = LinkTypeLookup.Parse(type.Value);
                link.Url = new Uri(token.Values[0],UriKind.RelativeOrAbsolute);

                var links = (List<Link>) vcard.Urls ?? new List<Link>();
                links.Add(link);
                vcard.Urls = links;
            }
        }
    }
}