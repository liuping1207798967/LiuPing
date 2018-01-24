using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePosition : MonoBehaviour {

    Vector3 pos;
    Quaternion Eulers;
    Vector3 scals;
    // Use this for initialization
    void Start()
    {
        pos = this.gameObject.transform.position;
        Eulers = this.gameObject.transform.rotation;
        scals = this.gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update () {
		
	}
    public void Back()
    {
        this.gameObject.transform.position = pos;
        this.transform.rotation = Eulers;
        this.transform.localScale = scals;
    }
}
