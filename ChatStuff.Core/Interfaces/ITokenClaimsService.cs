using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatStuff.Core.Interfaces;
public interface ITokenClaimsService
{
    Task<string> GenerateJwtToken(string userName);
}

