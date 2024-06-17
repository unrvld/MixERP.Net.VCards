using System;
using System.Linq;
using System.Collections.Generic;
using MixERP.Net.VCards.Models;
using MixERP.Net.VCards.Types;

namespace MixERP.Net.VCards.Processors
{
    public static class CalendarUserAddressesProcessor
    {
        public static string Serialize(VCard vcard)
        {
            if (vcard.CalendarUserAddresses == null || vcard.CalendarUserAddresses.Count() == 0)
            {
                return string.Empty;
            }

            //Only vCard 4.0 supports CALADRURI property
            if (vcard.Version != VCardVersion.V4)
            {
                return string.Empty;
            }

            return UrlProcessor.SerializeLinks(vcard.CalendarUserAddresses, "CALADRURI", vcard.Version, vcard.Encoding, vcard.Charset);
        }

        public static void Parse(Token token, ref VCard vcard)
        {
            string value = token.Values[0];
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            var uri = new Uri(value, UriKind.RelativeOrAbsolute);

            var urls = (List<Link>)vcard.CalendarUserAddresses ?? new List<Link>();
            urls.Add(new Link { Url = uri });
            vcard.CalendarUserAddresses = urls;
        }
    }
}