using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Lance
{

    [ExecuteInEditMode]
    public class Deleter : MonoBehaviour
    {
        [MenuItem("Tools/Player Prefs remover")]

        public static void DeletePlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefsDeleted");

        }
    }
}
