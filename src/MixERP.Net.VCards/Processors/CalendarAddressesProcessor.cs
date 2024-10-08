using System;
using System.Collections.Generic;
using System.Linq;
using MixERP.Net.VCards.Models;
using MixERP.Net.VCards.Types;

namespace MixERP.Net.VCards.Processors
{
    public static class CalendarAddressesProcessor
    {
        public static string Serialize(VCard vcard)
        {
            if(vcard.CalendarAddresses == null || vcard.CalendarAddresses.Count() == 0)
            {
                return string.Empty;
            }

            //Only vCard 4.0 supports CALADRURI property
            if (vcard.Version != VCardVersion.V4)
            {
                return string.Empty;
            }

            return UrlProcessor.SerializeLinks(vcard.CalendarAddresses, "CALURI", vcard.Version, vcard.Encoding, vcard.Charset);
        }

        public static void Parse(Token token, ref VCard vcard)
        {
            string value = token.Values[0];
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            var uri = new Uri(value, UriKind.RelativeOrAbsolute);

            var urls = (List<Link>) vcard.CalendarAddresses ?? new List<Link>();
            urls.Add(new Link {Url = uri});
            vcard.CalendarAddresses = urls;
        }
    }
}