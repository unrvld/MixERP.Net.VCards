using MixERP.Net.VCards.Models;
using MixERP.Net.VCards.Serializer;

namespace MixERP.Net.VCards.Processors
{
    public static class RoleProcessor
    {
        public static string Serialize(VCard vcard)
        {
            if (string.IsNullOrWhiteSpace(vcard.Role))
            {
                return string.Empty;
            }

            return DefaultSerializer.GetVCardString("ROLE", vcard.Role, true, vcard.Version, vcard.Encoding, vcard.Charset);
        }

        public static void Parse(Token token, ref VCard vcard)
        {
            vcard.Role = token.Values[0];
        }
    }
}