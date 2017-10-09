using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroys : MonoBehaviour {
    private float emit;
    public float durtions = 10f;
	void Start () {
        emit = Time.fixedTime;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.fixedTime - emit > durtions)
        {
            Destroy(this .gameObject );
        }
	}
}
