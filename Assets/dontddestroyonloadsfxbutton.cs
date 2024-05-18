using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    public class dontddestroyonloadsfxbutton : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

    }
}
