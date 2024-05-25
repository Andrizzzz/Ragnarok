using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    public class Deleter : MonoBehaviour
    {
        void Start()
        {
            PlayerPrefs.DeleteAll();
        }

    }
}
