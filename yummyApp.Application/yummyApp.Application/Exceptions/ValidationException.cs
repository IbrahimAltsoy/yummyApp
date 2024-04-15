using FluentValidation.Results;

namespace yummyApp.Application.Exceptions
{
    public class ValidationException:Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException() : base("One or more validation failures have occured!")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(gr => gr.Key, v => v.ToArray());
        }
    }
}
