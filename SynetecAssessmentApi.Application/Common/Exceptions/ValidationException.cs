using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Application.Common.Exceptions
{
    [Serializable]
    public class ValidationException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //
        public ValidationException(IReadOnlyCollection<ValidationFailure> failures)
            : this(CreateMessage(failures))
        {
            var failureGroups = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage);

            foreach (var failureGroup in failureGroups)
            {
                var propertyName = failureGroup.Key;
                var propertyFailures = failureGroup.ToArray();

                Errors.Add(propertyName, propertyFailures);
            }
        }

        public static string CreateMessage(IEnumerable<ValidationFailure> failures)
        {

            return $"ValidationException: {Environment.NewLine} { string.Join($",{Environment.NewLine}", failures.Select(x => x.PropertyName + ":" + string.Join(", ", x.ErrorMessage))) }";

            //return "One or more validation failures have occurred.";
        }

        public IDictionary<string, string[]> Errors { get; } = new Dictionary<string, string[]>();

        public ValidationException(string message) : base(message)
        {
        }

        public ValidationException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ValidationException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

        public ValidationException(string[] resultErrors) : base(resultErrors.FirstOrDefault())
        {
            Errors.Add("General", resultErrors);
        }

    }
}
