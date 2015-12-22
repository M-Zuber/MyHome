namespace MyHome.UI.Helpers
{
    public static class HelperMethods
    {
        public static bool IsDecimal(this string input)
        {
            decimal dbToParse;
            return decimal.TryParse(input, out dbToParse);
        }
    }
}
