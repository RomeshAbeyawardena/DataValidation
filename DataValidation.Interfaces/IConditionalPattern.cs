using System.Threading.Tasks;

namespace DataValidation.Interfaces
{
    public delegate Task<bool> PatternConditionalDelegate<in T>(T entry);

    public delegate Task<T> PatternResultDelegate<T>(T entry);

    public delegate void PatternDelegate<in T>(T entry);

    public interface IConditionalPattern<T>
    {
        Task<T> Run(PatternConditionalDelegate<T> patternConditionalDelegate, T entry);
        Task<T> Run(T entry, bool invokePassedResult = true, params IConditionalException<T>[] patternConditionalDelegate);
    }
}