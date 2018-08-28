using System;
using System.Threading.Tasks;
using DataValidation.Interfaces;

namespace DataValidation.Extensions
{
    public class ConditionalPattern<T> : IConditionalPattern<T>
    {
        public static async Task<T> RunConditionalPattern(T entry, PatternConditionalDelegate<T> patternConditionalDelegate,
            PatternResultDelegate<T> patternPassedResultDelegate,
            PatternDelegate<T> patternFailedDelegate)
        {
            var pattern = new ConditionalPattern<T>(patternPassedResultDelegate, patternFailedDelegate);
            return await pattern.Run(patternConditionalDelegate, entry);
        }

        public static async Task<T> RunConditionalPattern(T entry,
            PatternResultDelegate<T> patternPassedResultDelegate,
            PatternDelegate<T> patternFailedDelegate, params IConditionalException<T>[] conditionalExceptions)
        {
            var pattern = new ConditionalPattern<T>(patternPassedResultDelegate, patternFailedDelegate);
            return await pattern.Run(entry, patternConditionalDelegates: conditionalExceptions);
        }

        private ConditionalPattern(PatternResultDelegate<T> patternPassedResultDelegate,
            PatternDelegate<T> patternFailedDelegate)
        {
            _patternPassedResultDelegate = patternPassedResultDelegate;
            _patternFailedDelegate = patternFailedDelegate;
        }

        public async Task<T> Run(T entry, bool invokePassedResult = true, 
            params IConditionalException<T>[] patternConditionalDelegates)
        {
            foreach (var patternConditionalDelegate in patternConditionalDelegates)
            {
                await patternConditionalDelegate.Run(entry);
                //runs until exception is thrown.
            }

            if (invokePassedResult)
                return await _patternPassedResultDelegate.Invoke(entry);

            return entry;
        }

        public async Task<T> Run(PatternConditionalDelegate<T> patternConditionalDelegate, T entry)
        {
            await Run(entry, false,
                ConditionalException<T>.Create(async (e) => await Task.FromResult(e != null),
                    typeof(ArgumentNullException), nameof(entry)));

            if (await patternConditionalDelegate(entry))
                return await _patternPassedResultDelegate(entry);

            _patternFailedDelegate(entry);
            return entry;
        }

        private readonly PatternResultDelegate<T> _patternPassedResultDelegate;
        private readonly PatternDelegate<T> _patternFailedDelegate;
    }
}