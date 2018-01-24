using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Imageback : MonoBehaviour
{
    Image image;

    void Start()
    {

        Tweener tweener = this.transform.DOLocalMove(new Vector3(0, 0, 0), 2f); //让image在2秒钟内从当前位置移动到原点
        tweener.SetAutoKill(false);
        tweener.Pause();   //方法 停止
        tweener.OnPause(imagemove);  //回调函数
        imagemove();

        image = GetComponent<Image>();


    }
    //上面
    public void imagemove()
    {
        this.transform.DOLocalMove(new Vector3(-74, 333.2f, -17), 2f);

    }

    //下面
    public void imageback()
    {
        this.transform.DOLocalMove(new Vector3(-74, -54.6f, -17), 2f);

    }
  

  
}
