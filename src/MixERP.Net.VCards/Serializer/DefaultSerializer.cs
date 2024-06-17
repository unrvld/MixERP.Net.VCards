using System;
using System.Linq;
using System.Text;
using MixERP.Net.VCards.Extensions;
using MixERP.Net.VCards.Types;

namespace MixERP.Net.VCards.Serializer
{
    public static class DefaultSerializer
    {
        public static string GetVCardString(string key, string value, bool mustEscape, VCardVersion version, Encoding encoding, Encoding charset, string type = "")
        {
            string[] types = {type};
            return GetVCardString(key, value, mustEscape, version, types, encoding, charset);
        }

        public static string GetVCardString(string key, string value, bool mustEscape, VCardVersion version, string[] types, Encoding encoding, Encoding charset )
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return string.Empty;
            }

            if (mustEscape)
            {
                value = value.Escape();
            }

            //Legal values for v3
            //"TYPE=dom;TYPE=postal"
            //or
            //"TYPE=dom,postal"
            string type = "TYPE=" + string.Join(",", types);

            if (version == VCardVersion.V2_1)
            {
                type = string.Join(";", types);
            }

            string line = key;


            if (types.Any(x => !string.IsNullOrWhiteSpace(x)))
            {
                line = line + ";" + type;
            }

            if (encoding != null)
            {
                line = line + $";ENCODING={encoding.HeaderName}";
            }
            if (charset != null)
            {
                line = line + $";CHARSET={charset.HeaderName}";
            }

            line = line + ":" + value + Environment.NewLine;
            return line;
        }
    }
}