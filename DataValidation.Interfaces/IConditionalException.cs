using System;
using System.Threading.Tasks;

namespace DataValidation.Interfaces
{
    public interface IConditionalException<in T>
    {
        Task Run(T entry);
        Type ExceptionType { get; }
    }
}