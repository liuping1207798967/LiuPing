using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class alldestore : MonoBehaviour
{
    public Button b0;
    public Text t0;
    public Button b1;
    public Text t1;
    private float a = 1;
   
    void Start()
    {
        a = a + Time.deltaTime;
        b0.gameObject.SetActive(true);
        t0.gameObject.SetActive(true);
        b1.gameObject.SetActive(false);
        t1.gameObject.SetActive(false);

    }

    void Update()
    {

    }
    public void showall()
    {

        a += 2;
        if (a>=4)
        {
            b0.gameObject.SetActive(false);
            t0.gameObject.SetActive(false);
            b1.gameObject.SetActive(true);
            t1.gameObject.SetActive(true);
        }
    }
}
