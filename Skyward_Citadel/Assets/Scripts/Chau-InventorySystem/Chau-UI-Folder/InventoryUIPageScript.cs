using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using System;


public class InventoryUIPageScript : MonoBehaviour
{
    [SerializeField]
    private UIInventoryItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    System.Collections.Generic.List<UIInventoryItem> listOfUIItems = new System.Collections.Generic.List<UIInventoryItem>();

    public void InitializeInventoryUI(int inventorySize)
    {
        for(int i = 0; i < inventorySize; ++i)
        {
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);

            /*Explanation:
             * 1. The Instantiate function creates a copy of the object "itemPrefab"
             * at the specified position and rotation. 
             * 2. Parameters:
             * a. itemPrefab: this is the object you want to clone.
             * It's usually a prefab or existing object in Unity. 
             * b. Vector3.zero: The position where the new object will be instantiated. 
             * Vector3.zero represents the point (0,0,0) in 3D space. 
             * c. Quaternion.identity: the rotation of the new object.
             * Quaternion.identity means no rotation (aligned with the world axes)
             * 3. Type Casting: The Instantiate function returns a generic Object. 
             * By assigning it to a variable of type UIInventoryItem, you are casting it to that
             * type, implying that itemPrefab is expected to be (or derived from) 
             * a UIInventoryItem class or component. 
             */

            uiItem.transform.SetParent(contentPanel);
            /* Explanation:
             * The SetParent method changes the parent of the uiItem's Transform to contentPanel.
             * 
             * Parameters:
             * 1. uiItem.transform: This refers to the Transform compoent of the uiItem. 
             * The Transform component handles the position, rotation, and scale of an object in Unity. 
             * 2. contentPanel This is the new parent Tranform for uiItem.
             * It is likely a contanier or layout group within the Unity hierarchy.
             * 
             * Effect:
             * 1. The uiItem will now be a child of contentPanel in the hierarchy. 
             * This means its position, rotation, and scale will be relative to the contentPanel. 
             * 
             * UI Use Case:
             * 1. If contentPanel is part of a UI layout, this operation ensures that uiItem 
             * is included in the layout and positioned correctly within the UI. 
             * 
             * If you are dynamically creating UI inventory items (like buttons or icons) and 
             * want them to appear inside a scrollable list or grid, this line ensures the new item 
             * is visually and structurally part of that list by attaching it to the contentPanel.
             */

            listOfUIItems.Add(uiItem);

            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightMouseButtonClicked += HandleShowItemActions;

        }
    }

    private void HandleShowItemActions(UIInventoryItem item)
    {
        
    }

    private void HandleEndDrag(UIInventoryItem item)
    {
        
    }

    private void HandleSwap(UIInventoryItem item)
    {
        
    }

    private void HandleBeginDrag(UIInventoryItem item)
    {
        
    }

    private void HandleItemSelection(UIInventoryItem item)
    {
        Debug.Log(item.name);
    }

    //Make it able for player to hide and show the inventory page. 
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
