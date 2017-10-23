namespace MyHome.UI.Helpers
{
    public static class HelperMethods
    {
        public static bool IsDecimal(this string input)
        {
            return decimal.TryParse(input, out _);
        }
    }
}
