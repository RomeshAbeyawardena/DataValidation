namespace DataValidation.Extensions
{
    public static class RegularExpressions
    {
        public const string UkPostalCode = "\\A[A-Z]{1,2}[0-9]{1,2}[ ][0-9]{1,2}[A-Z]{2}";
        public const string UsZipCode = "\\A[0-9]{5}";
    }
}