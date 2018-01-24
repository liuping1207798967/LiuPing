using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class liangcontro : MonoBehaviour {

    private HandDraggable move;

    private GameObject[] zis;

    void Start()
    {


    }
    public void controsl()
    {
        zis = GameObject.FindGameObjectsWithTag("Model");
        for (int i = 0; i < zis.Length; i++)
        {
            move = zis[i].GetComponent<HandDraggable>();
            if (GazeManager.Instance.HitObject == move)
            {
                move.enabled = true;
            }
        }
    }
    public void roomcontrosl()
    {
        zis = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < zis.Length; i++)
        {
            move = zis[i].GetComponent<HandDraggable>();
            if (GazeManager.Instance.HitObject == move)
            {
                move.enabled = true;
            }
        }
    }
    public void lostcontrosl()
    {
        zis = GameObject.FindGameObjectsWithTag("Model");
        for (int i = 0; i < zis.Length; i++)
        {
            move = zis[i].GetComponent<HandDraggable>();
            if (GazeManager.Instance.HitObject == move)
            {
                move.enabled = false;
            }
        }
    }
}
