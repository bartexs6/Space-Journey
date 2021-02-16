using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/* ? */
public class Tween : MonoBehaviour
{

    public UnityEvent onCompleteCallBack;

    public void OnEnable()
    {
        transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), 0.3f).setDelay(0.2f).setOnComplete(OnComplete);
    }

    public void OnComplete()
    {
        if (onCompleteCallBack != null)
        {
            onCompleteCallBack.Invoke();
        }
    }

    public void OnClose()
    {

    }

    public void Destroy()
    {
        
    }
}
