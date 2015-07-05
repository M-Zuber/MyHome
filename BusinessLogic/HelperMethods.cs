namespace BusinessLogic
{
    public class HelperMethods
    {
        //TODO should this be in the bl or the ui??
        /// <summary>
        /// Checks if the given string is a floating point number
        /// </summary>
        /// <param name="strText">The string to be checked</param>
        /// <returns>The result of the check</returns>
        public static bool IsNumeric(string strText)
        {
            double dbToParse;
            return double.TryParse(strText, out dbToParse);
        }
    }
}
