using System;
using System.Collections.Generic;
using System.Linq;
using Benday.Presidents.Core.Models;

namespace Benday.Presidents.Core.Services
{
    public interface IPresidentValidatorStrategy
    {
        bool IsValid(President validateThis);
    }
}
