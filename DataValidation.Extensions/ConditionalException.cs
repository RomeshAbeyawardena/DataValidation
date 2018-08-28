using System;
using System.Threading.Tasks;
using DataValidation.Interfaces;

namespace DataValidation.Extensions
{
    public class ConditionalException<T> : IConditionalException<T>
    {
        public static ConditionalException<T> Create(PatternConditionalDelegate<T> patternConditionalException, Type exceptionType,
            params object[] exceptionArgs)
        {
            return new ConditionalException<T>(patternConditionalException, exceptionType, exceptionArgs);
        }

        private ConditionalException(PatternConditionalDelegate<T> patternConditionalException, Type exceptionType, params object[] exceptionArgs)
        {
            _patternConditionalException = patternConditionalException;
            ExceptionType = exceptionType;
            _exceptionArgs = exceptionArgs;
        }

        public async Task Run(T entry)
        {
            if (await _patternConditionalException.Invoke(entry))
                return;

            throw (Exception)Activator.CreateInstance(ExceptionType, _exceptionArgs);
        }

        private readonly PatternConditionalDelegate<T> _patternConditionalException;
        public Type ExceptionType { get; }
        private readonly object[] _exceptionArgs;
    }
}