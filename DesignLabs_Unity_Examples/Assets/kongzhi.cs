using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HUX.Interaction
{

    public class kongzhi : MonoBehaviour
    {
        void Start()
        {

        }
        void Update()
        {

        }
        //定位模式函数
        public void Place_Mode()
        {
            mnr.A.Obj_Bds.GetComponent<TapToPlace_Bd>().enabled = true;
            mnr.A.Obj_Bds.GetComponent<Rotate_Bd>().enabled = false;
           // mnr.A.Obj_Bds.GetComponent<BoundingBoxTarget>().enabled = false ;
        }

        //手动拖拽旋转模式函数
        public void Rotation_Mode()
        {
            if (Vrb_Mnr.A.Obj_Bds.GetComponent<TapToPlace_Bd>().IsBeingPlaced == false)
            {
                mnr.A.Obj_Bds.GetComponent<TapToPlace_Bd>().enabled = false;
                mnr.A.Obj_Bds.GetComponent<Rotate_Bd>().enabled = true;
              //  mnr.A.Obj_Bds.GetComponent<BoundingBoxTarget>().enabled = false;

            }
        }
        public void big_Mode()
        {
           

            mnr.A.Obj_Bds.GetComponent<TapToPlace_Bd>().enabled = false;
            //  mnr.A.Obj_Bds.GetComponent<Rotate_Bd>().enabled = true;
            mnr.A.Obj_Bds.AddComponent<BoundingBoxTarget>();
            
        }
        public void show_Mode()
        {
            mnr.A.room.SetActive(true);
        }
        public void scale_Mode()
        {
            mnr.A.room.AddComponent<BoundingBoxTarget>();
        }
    }
}
        
