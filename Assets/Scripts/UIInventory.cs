using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    //[SerializeField] private TMP_Text quantityTxt;
    //[SerializeField] private Image borderImage;

    public event Action<UIInventory> OnItemClicked,
    onItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnRightMouseBtnClick;

    private bool empty = true;

    void Awake()
    {
        ResetData();
        Deselect();
    }

    public void ResetData()
    {
        this.itemImage.gameObject.SetActive(false);
        empty = true;
    }

    public void Deselect()
    {
        //borderImage.enabled = false;
    }

    public void SetData(Sprite sprite)
    {
       this.itemImage.gameObject.SetActive(true);
       this.itemImage.sprite = sprite;
       empty = false; 
    }

    public void Select()
    {
        //borderImage.enabled = true;
    }

    public void OnBeginDrag()
    {
        if(empty) return;
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnDrop()
    {
        onItemDroppedOn?.Invoke(this);
    }

    public void OnEndDrug()
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnPointerClick(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;
        if(pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseBtnClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }
}
