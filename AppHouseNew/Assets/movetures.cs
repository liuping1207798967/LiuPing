using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class movetures : MonoBehaviour
{

    public MovieTexture movTexture;
    // Use this for initialization

    void Start()
    {
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        GameObject ga = GameObject.FindGameObjectWithTag("a");
        if (GazeManager .Instance .HitObject==ga)
        {
        this.GetComponent<VideoPlayer>().enabled = true;
        }
       
    }
    void Update()
    {
    }

}
    
   
