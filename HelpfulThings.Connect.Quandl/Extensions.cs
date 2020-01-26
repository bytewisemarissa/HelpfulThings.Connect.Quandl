using HelpfulThings.Connect.Quandl.Exceptions;

namespace HelpfulThings.Connect.Quandl
{
    public static class Extensions
    {
        public static string[] SplitComplexCode(this string complexShortCode)
        {
            var splitShortCodes = complexShortCode.Split('/');
            
            if (splitShortCodes.Length != 2)
            {
                throw new InvalidShortCodeException("The short code combination is not formatted correctly.");
            }

            return splitShortCodes;
        }
    }
}