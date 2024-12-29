using System.Collections.Generic;


namespace Shared.NuGet.CallResults
{
    public static partial class Result
    {
        public static CallResult ValidationFail(IRequestModelWithValidation model)
        {
            return new ValidationFailResult(model) as CallResult;
        }

        public static CallResult<T> ValidationFail<T>(IRequestModelWithValidation model)
        {
            return new ValidationFailResult<T>(model);
        }

        public static CallListResult<T> ValidationFailList<T>(IRequestModelWithValidation model)
        {
            return new ValidationFailListResult<T>(model);
        }

        public static CallResult ValidationFail(Dictionary<string, string> modelErrors)
        {
            return new ValidationFailResult(modelErrors);
        }

        public static CallResult ValidationFail(string key, string errorMessage)
        {
            return new ValidationFailResult(key, errorMessage);
        }

        public static CallResult<T> ValidationFail<T>(string key, string errorMessage)
        {
            return new ValidationFailResult<T>(key, errorMessage);
        }
    }
}
