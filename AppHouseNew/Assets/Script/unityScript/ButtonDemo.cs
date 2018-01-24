using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDemo : MonoBehaviour
{
    // MeshRenderer[] marr = target.GetComponentsInChildren<MeshRenderer>(true);

    public GameObject fu;
    public GameObject a;
    public GameObject b;
    void Start()
    {
        fu = GameObject.Find("FU");
        a = fu.transform.Find("A").gameObject;
        b = fu.transform.Find("B").gameObject;
    
    
    }
    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 20, 100, 30), "ha"))
        {
            a.SetActive(true);
            b.SetActive(false);
        }
        if (GUI.Button(new Rect(0, 60, 100, 30), "hi"))
        {
            a.SetActive(false);
            b.SetActive(true);
        }
    }
}
