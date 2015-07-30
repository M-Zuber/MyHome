using System;
using System.Diagnostics;

namespace MyHome.Infrastructure.Validation
{
    public class Contract
    {
        public static void Requires<TException>(bool predicate, string message = "")
            where TException : Exception, new()
        {
            if (!predicate)
            {
                Debug.WriteLine(message);
                throw new TException();
            }
        }
    }
}