using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changecolor : MonoBehaviour {

    MeshRenderer gas;
    Color cos;
	void Start () {
        gas = this.gameObject.GetComponent<MeshRenderer>();
        this.gameObject.GetComponent<MeshRenderer>().material.color = cos;


    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void changes()
    {
        gas.material.color = Color.red;
    }
    public void notchanges()
    {
        gas.material.color = cos;
    }
}
