using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Gemons.CsvImport
{
    /// <summary>
    /// Class to parse a CSV file into a list of dictionaries. Every row is a list entry, every column a dictionary field.
    /// </summary>
    public static class CsvReader
    {
        #region Constants
        const string _splitExpression = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
        const string _lineSplitExpression = @"\r\n|\n\r|\n|\r";
        static readonly char[] _trimChars = { '\"' };
        #endregion

        #region Methods
        /// <summary>
        /// Reads a CSV text tile into a list of dictionaries. Each row of the CSV being one Dictionary in the list.
        /// Every column in the CSV is a field in the dictionary.
        /// </summary>
        /// <param name="file">path to file that needs parsing</param>
        /// <returns>list of dictionaries with CSV row and column data.</returns>
        public static List<Dictionary<string, object>> Read(string file)
        {
            List<Dictionary<string, object>> listOfRows = new List<Dictionary<string, object>>();
            string rawText = System.IO.File.ReadAllText(file);
            string[] lines = Regex.Split(rawText, _lineSplitExpression);
            if (lines.Length <= 1)
            {
                return listOfRows;
            }

            string[] columnNames = Regex.Split(lines[0], _splitExpression);
            for (int iLine = 1; iLine < lines.Length; iLine++)
            {
                string[] values = Regex.Split(lines[iLine], _splitExpression);
                if (values.Length == 0 || values[0] == "")
                {
                    continue;
                }

                Dictionary<string,object> cellData = new Dictionary<string, object>();
                for (int iColumn = 0; iColumn < columnNames.Length && iColumn < values.Length; iColumn++)
                {
                    object value = values[iColumn].TrimStart(_trimChars).TrimEnd(_trimChars).Replace("\\", "");

                    // Parse float and integer values if possible using invariant culture format
                    if (int.TryParse((string)value, NumberStyles.Integer, CultureInfo.InvariantCulture, out int n))
                    {
                        value = n;
                    }
                    else if (float.TryParse((string)value, NumberStyles.Float, CultureInfo.InvariantCulture, out float f))
                    {
                        value = f;
                    }
                    cellData[columnNames[iColumn]] = value;
                }
                listOfRows.Add(cellData);
            }
            return listOfRows;
        }
        #endregion
    }

}
