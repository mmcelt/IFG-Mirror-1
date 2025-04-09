using System.Collections.Generic;
using UnityEngine;


namespace Gemons.CsvImport.Demo
{
    public class TestScript : MonoBehaviour
    {
        #region Constants
        #endregion

        #region Events
        #endregion

        #region Public Fields
        [Header("Item list to be plotted to Debug.Log on Start")]
        public List<ItemDataImportExample> ItemList;
        #endregion

        #region Properties
        #endregion

        #region Private Fields
        #endregion

        #region Methods
        /// <summary>
        /// Called upon start of script
        /// </summary>
        private void Start()
        {
            foreach (ItemDataImportExample example in ItemList)
            {
                Debug.Log(example);
            }
        }
        #endregion
    }
}
