using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialDemo : MonoBehaviour {

    Vector3 pos;
    
    // Use this for initialization
  
    void Start () {
     
        pos = this.gameObject.transform.position;
        

    }

    // Update is called once per frame
    void Update() {

        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //    Back();
        //}
    }

   public void Back()
    {
        this.gameObject.transform.position = pos;
        this.transform.localEulerAngles = new Vector3(0,0,0);
        this.transform.localScale = new Vector3(1,1,1);
    }

}
