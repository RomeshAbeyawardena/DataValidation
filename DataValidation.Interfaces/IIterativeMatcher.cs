using System;

namespace DataValidation.Interfaces
{
    public interface IIterativeMatcher
    {
        TResult GetMatch<TResult>(Func<TResult> getValueFunc, Func<TResult, bool> validationFunc, int maximumIterations = 1000);
        TResult Next<TResult>(Func<TResult, bool> isValid, Func<TResult> getValue);
    }
}