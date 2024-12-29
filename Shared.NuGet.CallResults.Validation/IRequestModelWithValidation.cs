using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.NuGet.CallResults
{
    public interface IRequestModelWithValidation
    {
        Dictionary<string, List<string>> GetValidationErrors();
    }
}
