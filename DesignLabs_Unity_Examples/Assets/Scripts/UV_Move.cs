using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UV_Move : MonoBehaviour
{

    public float Offset_X = 0;
    //记录UV在X轴上的偏移值

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Offset_X -= Time.deltaTime*0.3f;
        //让UV在X轴上的便宜值跟随时间减少
        gameObject.GetComponent<Renderer>().material.SetTextureOffset("_MainTex",new Vector2(Offset_X,0));
        //把偏移值附给物体UV的偏移属性


	}
}
