using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class Conteabel : MonoBehaviour, IInputClickHandler
{
    

    public GameObject HandMove;
 //   public GameObject HandCube;
   // public GameObject HandCube01;
    bool Teabel = true;
    void Start()
    {
      
    }
    void Update()
    {

    }

    public void OnInputClicked(InputClickedEventData eventData)//点击事件
    {
        //凝视的物体
        if (GazeManager.Instance.HitObject.tag  == "ray")//点击的物体名字
        {

            if (Teabel)
            {
                HandMove.GetComponent<HandMove>().enabled = true;
             //   HandCube.SetActive(true);
              //  HandCube01.SetActive(false);
                Teabel = false;
            }
            else
            {
                HandMove.GetComponent<HandMove>().enabled = false;
                Teabel = true;
             //   HandCube.SetActive(false);
              //  HandCube01.SetActive(true);
            }
        }

    }
}



