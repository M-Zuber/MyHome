namespace MyHome.UI.Helpers
{
    public static class HelperMethods
    {
        public static bool IsDouble(this string input)
        {
            double dbToParse; 
            return double.TryParse(input, out dbToParse);
        }
    }
}
