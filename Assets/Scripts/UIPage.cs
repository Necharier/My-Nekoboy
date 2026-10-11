using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIPage : MonoBehaviour
{
     private UIInventory itemPrefab;
    [SerializeField] private RectTransform contentPanel;
    [SerializeField] InventoryDescription itemDescription;
    List<UIInventory> uiItemList = new List<UIInventory>();

    public Sprite sprite;
    public string title, description;

    public void Initialize(int inventorySize)
    {
        for(int i = 0; i < inventorySize; i++)
        {
            UIInventory uiItem = Instantiate(itemPrefab, contentPanel);
            uiItemList.Add(uiItem);

            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.onItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightMouseBtnClick += HandleShowItemActions;
        }
    }

    void Awake()
    {
        Hide();
        itemDescription.ResetDescription();
    }

    private void HandleShowItemActions(UIInventory obj)
    {
        
    }

    private void HandleEndDrag(UIInventory obj)
    {
        throw new NotImplementedException();    
    }

    private void HandleSwap(UIInventory obj)
    {
        throw new NotImplementedException();
    }

    private void HandleBeginDrag(UIInventory obj)
    {
        throw new NotImplementedException();
    }

    private void HandleItemSelection(UIInventory obj)
    {
        itemDescription.SetDescription(sprite, title, description);
        uiItemList[0].Select();
    }


    public void Show()
    {
        gameObject.SetActive(true);
        itemDescription.ResetDescription();

        uiItemList[0].SetData(sprite);
    }
    public void Hide() => gameObject.SetActive(false);


}