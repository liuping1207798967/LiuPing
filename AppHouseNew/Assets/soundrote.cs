using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundrote : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	}

    public void Soundrote()
    {
        Func_Mnr.A.Au_Ctrl("Au_OK");
    }
    public void Tiaorote()
    {
        Func_Mnr.A.Au_Ctrl("Au_Wait");
    }
    public void huabu()
    {
        Func_Mnr.A.Au_Ctrl("Au_Place");
    }
}
