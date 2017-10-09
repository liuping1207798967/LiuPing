using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HoloToolkit.Unity.InputModule
{
    
    public class hand : MonoBehaviour, IInputClickHandler
    {
        // public GameObject[] prefab = new GameObject[3];
        public GameObject prefab;
        //void Start()
        //{
        //    prefab = new GameObject[3];
        //}
        public void OnInputClicked(InputClickedEventData eventData)
        {
            // GameObject pro = Instantiate(prefab[UnityEngine.Random.Range(0, prefab.Length)]) as GameObject;
            GameObject pro = Instantiate(prefab);
            pro.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 0.85f;
            Rigidbody rb = pro.GetComponent<Rigidbody >();
            rb.useGravity = false;
            rb.velocity = Camera.main.transform.forward * 10;
        }
       public void Start()
        {
            InputManager.Instance.PushModalInputHandler(this .gameObject );
        }
    }
} 
