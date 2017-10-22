﻿using System;
using System.Diagnostics;
using System.Reflection;

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
                TException ex = new TException();
                ex.GetType().GetField("_message", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(ex, message);
                throw ex;
            }
        }
    }
}