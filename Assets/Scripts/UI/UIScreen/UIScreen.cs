using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIScreen : MonoBehaviour
{
    protected object[] _datas;
    protected Image interactableMask;

    public virtual void OnInit(object[] datas)
    {
        _datas = datas;
        Transform maskTransform = transform.Find("InteractableMask");
        if(maskTransform == null)
        {
            maskTransform = new GameObject("InteractableMask", typeof(Image)).transform;
            maskTransform.SetParent(this.transform);
            maskTransform.SetAsLastSibling();
            RectTransform rectTrans = maskTransform as RectTransform;
            rectTrans.anchoredPosition = Vector2.zero * 0.5f;
            rectTrans.anchoredPosition3D = Vector3.zero;
            rectTrans.localScale = Vector2.one;
            rectTrans.anchorMin = Vector2.zero;
            rectTrans.anchorMax = Vector2.one;
            interactableMask = maskTransform.GetComponent<Image>();
            interactableMask.color = interactableMask.color - new Color(0, 0, 0, 1);
        }       
        interactableMask.raycastTarget = true;
        InitComponent();
        InitData();
        InitView();
    }

    protected virtual void InitComponent()
    {

    }
    protected virtual void InitData()
    {

    }
    protected virtual void InitView()
    {
        OnShow();
    }
    public virtual void OnShow()
    {
        interactableMask.raycastTarget = false;
        this.gameObject.SetActive(true);
    }
    public virtual void OnHide()
    {
        interactableMask.raycastTarget = true;
        OnClose();
    }
    public virtual void OnClose()
    {
        this.gameObject.SetActive(false);
    }


    public T ParseDataByIndex<T>(int index)
    {
        return (T)_datas[index];
    }
}
