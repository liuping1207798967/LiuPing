using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gobak : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    public void huxing()
    {
        funcs.A.ActiveAu("Au_OK");
        SceneManager.LoadScene(2);
    }
    public void comeins()
    {
        funcs.A.ActiveAu("Au_OK");
        SceneManager.LoadScene(1);
    }
    public void Lastss()
    {
        funcs.A.ActiveAu("Au_OK");
        SceneManager.LoadScene(0);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
