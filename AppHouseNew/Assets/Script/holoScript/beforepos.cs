using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beforepos : MonoBehaviour
{
    Vector3 beforeposi;
    Quaternion Eulers;
    Vector3 scals;
    void Start()
    {
        beforeposi = this.gameObject.transform.position;
        Eulers = this.gameObject.transform.rotation;
        scals = this.gameObject.transform.localScale;
    }
    public void beforepositi()
    {
       // func_Add.A.Au_Ctrl("Au_OK");
        this.gameObject.transform.position = beforeposi;
        this.transform.rotation = Eulers;
        this.transform.localScale = scals;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
