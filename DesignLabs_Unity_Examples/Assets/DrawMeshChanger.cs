using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;
using UnityEngine.VR.WSA.Input;
using System;
using HoloToolkit.Unity.SpatialMapping;

public class DrawMeshChanger : MonoBehaviour
{

    GestureRecognizer recognizer;

    public bool isWireframe = true;
    public Material Wireframe;
    public Material Occlusion;

    // Use this for initialization  
    void Start()
    {
        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += Recognizer_TappedEvent;
        recognizer.StartCapturingGestures();
    }

    private void Recognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        SpatialMappingManager.Instance.SetSurfaceMaterial(isWireframe ? Occlusion : Wireframe);
        isWireframe = !isWireframe;
    }
}

//作者：Zhansongtao
//链接：http://www.jianshu.com/p/5cffc3f5c38a
//來源：简书
//著作权归作者所有。商业转载请联系作者获得授权，非商业转载请注明出处。