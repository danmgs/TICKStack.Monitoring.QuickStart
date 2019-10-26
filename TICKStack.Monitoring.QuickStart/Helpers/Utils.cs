using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace TICKStack.Monitoring.QuickStart.Helpers
{
    public static class Utils
    {
        public static string GetParamValue(string variableName)
        {
            string variableValue = Environment.GetEnvironmentVariable(variableName) ?? ConfigurationManager.AppSettings[variableName];
            System.Console.WriteLine($"Configuring variable [{variableName} = {variableValue}]");
            if (!string.IsNullOrEmpty(variableValue))
                return variableValue;
            else throw new ArgumentNullException($"The following variable is undefined either in System environment or App config file: ${variableName}");
        }
    }
}
