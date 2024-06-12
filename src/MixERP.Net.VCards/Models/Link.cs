using MixERP.Net.VCards.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.VCards.Models
{
    public class Link
    {
        public LinkType Type { get; set; }
        public string Number { get; set; }
        public int Preference { get; set; }
        public Uri Url { get; set; }
    }
}
