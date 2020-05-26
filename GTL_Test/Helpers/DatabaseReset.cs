using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace GTL_Test.Helpers
{
    [ExcludeFromCodeCoverage]
    public class DatabaseReset
    {
        public bool runSqlScriptFile(string pathStoreProceduresFile, string connectionString)
    {
        try
        {
            string script = File.ReadAllText(pathStoreProceduresFile);

            // split script on GO command
            IEnumerable<string> commandStrings = Regex.Split(script, @"^\s*GO\s*$",
                                     RegexOptions.Multiline | RegexOptions.IgnoreCase);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                foreach (string commandString in commandStrings)
                {
                    if (commandString.Trim() != "")
                    {
                        using (var command = new SqlCommand(commandString, connection))
                        {
                            try
                            {
                                command.ExecuteNonQuery();
                            }
                            catch (SqlException ex)
                            {
                                string spError = commandString.Length > 100 ? commandString.Substring(0, 100) + " ...\n..." : commandString;
                                return false;
                            }
                        }
                    }
                }
                connection.Close();
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
}
