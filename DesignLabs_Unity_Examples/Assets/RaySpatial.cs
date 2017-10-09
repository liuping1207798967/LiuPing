//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//namespace HoloToolkit.Unity.SpatialMapping
//{

//    public class RaySpatial : MonoBehaviour
//    {

//        // public LayerMask lays;
//        //  public GameObject[] game;
//        protected SpatialMappingManager spatialMappingManager;
//        //void Start()
//        //{
//        //    lays.value = game.layer;
//        //    spatialMappingManager = SpatialMappingManager.Instance;
//        //}

//        //// Update is called once per frame
//        void Update()
//        {
//            Vector3 pos = gameObject.transform.position;
//            Vector3 direc = gameObject.transform.up;
//            RaycastHit hitinfor;
//            if (Physics.Raycast(pos, direc, out hitinfor, 30.0f, spatialMappingManager.LayerMask))
//            {
//                if (hitinfor.collider != null && hitinfor.collider.tag == "ray")
//                {
//                    gameObject.GetComponent<TapToPlace>().SavedAnchorFriendlyName = "gameObject_" + System.DateTime.Now.ToString("yyyyMMddhhmmss");
//                    WorldAnchorManager.Instance.AttachAnchor(gameObject, gameObject.GetComponent<TapToPlace>().SavedAnchorFriendlyName);
//                }
//                else
//                {
//                    gameObject.GetComponent<TapToPlace>().SavedAnchorFriendlyName = "gameObject_" + System.DateTime.Now.ToString("yyyyMMddhhmmss");
//                    WorldAnchorManager.Instance.AttachAnchor(gameObject, gameObject.GetComponent<TapToPlace>().SavedAnchorFriendlyName);
//                }
//            }
//            // LayerMask la = 31 << (LayerMask.NameToLayer("SpatialMapping"));
//            //   bool bhit = Physics.Raycast(pos,);
//        }
//    }
//}
