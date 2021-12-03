using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScenesEditor : MonoBehaviour
{
    [ExecuteInEditMode]
    public class PrintAwake : MonoBehaviour
    {
        void Awake()
        {
            Debug.Log("Editor causes this Awake");
        }

        void Update()
        {
            Debug.Log("Editor causes this Update");
        }
    }
}
