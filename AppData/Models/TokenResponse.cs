using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class TokenResponse
    {
        public string? accessToken { get; set; }
        public string? expiredInMS { get; set; }
    }
}
