using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class funcs : MonoBehaviour {

    public static funcs A = null;
    void Awake()
    {
        A = this;
    }
    public void Activeobj(string Na,bool bl)
    {
        Allsay.A.Di[Na].SetActive(bl);
    }
    public void ActiveAu(string aus)
    {
        Allsay.A.Au_sc.clip = Allsay.A.Di_Au[aus];
        Allsay.A.Au_sc.Play();
        Allsay.A.Au_sc.Play();

    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

 
}
