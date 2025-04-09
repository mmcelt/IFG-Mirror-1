using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Gemons.CsvImport
{
    /// <summary>
    /// Class that adds a menu entry to "Assets/Gemons/CSV Importer" that allows to import CSV files
    /// into .asset files using ScriptableObjects. Assets are created in sub folder next to the
    /// specified CSV file.
    /// </summary>
    public class CsvImporter : EditorWindow
    {

        #region Private Fields
        TextAsset _csvFile;
        string _filePath;
        List<Type> _availableTypes = null;
        Type _selectedType = null;
        int _selectedIndex = 0;
        bool _deleteExistingObjects = false;
        #endregion

        #region Methods
        /// <summary>
        /// Adds menu entry for CSV importer.
        /// </summary>
        [MenuItem("Assets/Gemons/Import CSV")]
        public static void ShowWindow()
        {
            GetWindow(typeof(CsvImporter), true, "CSV Importer");
        }

        /// <summary>
        /// Called once the script is enabled.
        /// </summary>
        void OnEnable()
        {
            _availableTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(CsvImportBase).IsAssignableFrom(p) && !p.IsAbstract)
                .ToList();
        }

        /// <summary>
        /// Called on every GUI update.
        /// </summary>
        void OnGUI()
        {
            GUILayout.Label("Import CSV to ScriptableObjects", EditorStyles.boldLabel);

            // Drop down list of available types
            if (_availableTypes == null || _availableTypes.Count == 0)
            {
                EditorGUILayout.HelpBox("No ScriptableObject types found.", MessageType.Info);
                return;
            }

            // Selection of csv import file
            _csvFile = EditorGUILayout.ObjectField(_csvFile, typeof(TextAsset), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as TextAsset;
            if (AssetDatabase.GetAssetPath(_csvFile) != null && AssetDatabase.GetAssetPath(_csvFile) != "")
            {
                _filePath = $"{Path.GetDirectoryName(AssetDatabase.GetAssetPath(_csvFile))}/{Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(_csvFile))}";
                _filePath = _filePath.Replace("\\", "/") ;
                GUILayout.Label($"Import Destination: {_filePath}");
            }
            else
            {
                GUILayout.Label($"Drag and drop a .csv file to import.");
                return;
            }
            

            // Delete existing objects toggle button
            GUIContent toggleContent = new GUIContent("Delete existing objects", "true = existing files are completely overwritten; false = update of fields is handled by the target data class in UpdateFromImportedData(CsvImportBase importedData).");
            _deleteExistingObjects = EditorGUILayout.Toggle(toggleContent, _deleteExistingObjects);



            string[] typeNames = _availableTypes.Select(t => t.Name).ToArray();
            _selectedIndex = EditorGUILayout.Popup("Type", _selectedIndex, typeNames);

            _selectedType = _availableTypes[_selectedIndex];

            if (GUILayout.Button("Import"))
            {
                if (!Directory.Exists(_filePath))
                {
                    // The directory does not exist - create it
                    Directory.CreateDirectory(_filePath);
                    Debug.Log("Directory created: " + _filePath);
                }
                ImportCsvData();
            }
        }

        /// <summary>
        /// Import the CSV file and save each row to a .asset file in a folder with same name as the source CSV file.
        /// </summary>
        void ImportCsvData()
        {
            string fullPath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), AssetDatabase.GetAssetPath(_csvFile));

            if (IsCsvFile(fullPath) == false)
            {
                Debug.LogError($"Selected file {fullPath} is not a .csv file.");
                return;
            }

            List<Dictionary<string, object>> csvData = CsvReader.Read(fullPath);

            if (csvData == null || csvData.Count == 0)
            {
                Debug.LogError("No data could be read from file.");
                return;
            }
            Debug.Log($"{csvData.Count} lines from CSV file read.");

            foreach (Dictionary<string, object> csvRow in csvData)
            {
                CsvImportBase assetObject = CreateScriptableObject(csvRow);
                string path = $"{_filePath}/{assetObject.Name}.asset";
                CsvImportBase existingAsset = (CsvImportBase)AssetDatabase.LoadAssetAtPath(path, _selectedType);
                if (File.Exists(path) == false || _deleteExistingObjects == true || existingAsset == null)
                {
                    Debug.Log($"Overwriting asset: {path}");
                    // Create the asset
                    AssetDatabase.CreateAsset(assetObject, path);
                    AssetDatabase.SaveAssets();
                }
                else
                {
                    Debug.Log($"Updating asset: {path}");
                    // Load and update existing asset
                    existingAsset.UpdateFromImportedData(assetObject);
                }
            }
        }

        /// <summary>
        /// Creates the scriptable object and outputs information to the log window.
        /// </summary>
        /// <param name="csvRow">row of the CSV file corresponding to one output object</param>
        /// <returns>populated object conataining information of CSV row</returns>
        private CsvImportBase CreateScriptableObject(Dictionary<string, object> csvRow)
        {
            CsvImportBase newObject;
            if (_selectedType.IsSubclassOf(typeof(ScriptableObject)))
            {
                newObject = (CsvImportBase)ScriptableObject.CreateInstance(_selectedType);
            }
            else
            {
                Debug.LogError("Type is not a ScriptableObject.");
                return null;
            }

            // first try to set field values using reflection. If the field does not exist try to 
            // set a property using the given name. If also no property can be found raise an error.
            foreach (KeyValuePair<string, object> kvp in csvRow)
            {
                if (SetCsvImportBaseFieldValue(newObject, kvp.Key, kvp.Value) == false)
                {
                    if (SetCsvImportBasePropertyValue(newObject, kvp.Key, kvp.Value) == false)
                    {
                        Debug.LogError($"Could not find field or property of name '{kvp.Key}'.");
                    }
                }
            }

            // Try to call to string method of the object
            MethodInfo methodInfo = _selectedType.GetMethod(
                "ToString",
                BindingFlags.Public | BindingFlags.Instance,
                null,
                new Type[] { typeof(string), typeof(IFormatProvider) },
                null
                );
            try
            {
                string logString = (string)methodInfo.Invoke(newObject, new object[] { "", CultureInfo.InvariantCulture });
            }
            catch (TargetInvocationException ex)
            {
                Debug.LogError("An exception occurred during method invocation: " + ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                Debug.LogError("Exception: " + ex.Message);
            }

            return newObject;
        }

        /// <summary>
        /// Sets the value of a single field of the supplied CsvImportBase child object. Only works on fields not properties.
        /// </summary>
        /// <param name="obj">object to set field of</param>
        /// <param name="fieldName">name of field to be set</param>
        /// <param name="value">value to be assigned to field</param>
        bool SetCsvImportBaseFieldValue(CsvImportBase obj, string fieldName, object value)
        {
            FieldInfo fieldInfo = obj.GetType().GetField(fieldName);
            if (fieldInfo != null)
            {
                try
                {
                    fieldInfo.SetValue(obj, value);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Could not cast {value.GetType()} to field {fieldName} of type {fieldInfo.FieldType}");
                    throw e;
                }
                
            }
            else
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Sets the value of a single field of the supplied CsvImportBase child object. Only works on fields not properties.
        /// </summary>
        /// <param name="obj">object to set field of</param>
        /// <param name="propertyName">name of property to be set</param>
        /// <param name="value">value to be assigned to field</param>
        bool SetCsvImportBasePropertyValue(CsvImportBase obj, string propertyName, object value)
        {
            // Get the PropertyInfo object for the property
            PropertyInfo propertyInfo = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if (propertyInfo != null && propertyInfo.CanWrite)
            {
                try
                {
                    propertyInfo.SetValue(obj, value, null);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Could not cast {value.GetType()} to property {propertyName} of type {propertyInfo.PropertyType}");
                    throw e;
                }
                
            }
            else
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if the path leads to a csv file.
        /// </summary>
        /// <param name="fullPath">path to file</param>
        /// <returns>true = is csv; false = not a csv</returns>
        bool IsCsvFile(string fullPath)
        {
            return fullPath.ToLower().EndsWith(".csv");
        }
        #endregion


    }
}

#endif