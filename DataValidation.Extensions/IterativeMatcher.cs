using System;
using DataValidation.Interfaces;

namespace DataValidation.Extensions
{
    public class IterativeMatcher : IIterativeMatcher
    {
        public TResult GetMatch<TResult>(Func<TResult> getValueFunc, Func<TResult, bool> validationFunc, int maximumIterations = 1000)
        {
            _maximumIterations = maximumIterations;
            return Next(validationFunc, getValueFunc);
        }

        public virtual TResult Next<TResult>(Func<TResult, bool> isValid, Func<TResult> getValue)
        {
            if (_currentIteration++ < _maximumIterations)
            {
                var value = getValue();
                return isValid(value) ? value : Next(isValid, getValue);
            }

            return default(TResult);
        }

        private int _currentIteration;
        private  int _maximumIterations;
    }
}