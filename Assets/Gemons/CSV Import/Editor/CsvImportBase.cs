using System;
using UnityEngine;


namespace Gemons.CsvImport
{
    /// <summary>
    /// Base class used to filter for ScriptableObjects that inherit from this base class. Otherwise 
    /// the dropdown menu in the CsvImporter window would be full with all possible ScriptedObject classes.
    /// Inherit from this class to specify your own ScriptableObjects. Your child class needs to have a field
    /// or property for every column in the CSV file. Use Properties to set fields which are then visualized
    /// in the editor. Properties can be used to perform type conversions e.g. from string to enum in the setter
    /// </summary>
    public abstract class CsvImportBase : ScriptableObject, IFormattable
    {
        public string Name;
        public abstract string ToString(string format, IFormatProvider formatProvider);
        public abstract void UpdateFromImportedData(CsvImportBase importedData);

    }

}
