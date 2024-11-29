using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIInventoryItem : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private TMP_Text quantityTxt;

    [SerializeField]
    private Image borderImage;

    public event Action<UIInventoryItem> OnItemClicked,
        OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag,
        OnRightMouseButtonClicked;

    private bool empty = true;

    public void Awake()
    {
        /*
         * The Awake function in Unity is a special lifecycle method
         * that is part of the Unity MonoBehavior class. 
         * It is called automatically by Unity when a script's associated GameObject
         * is initialized, regardless of whether it is active or not. 
         * 
         * The Awake Method is used for initializing variables, references, and states
         * before the game starts running. It is called only once in the script's lifetime,
         * and it is the earliest initialization method in Unity's lifecycle. 
         * 
         * Awake is called before the Start method but after the object has been instantiated. 
         * This ensures all dependencies and references are set up before any gameplay 
         * logic begins in Start. 
         */
        ResetData();
        Deselect();
    }

    public void ResetData()
    {
        this.itemImage.gameObject.SetActive(false);
        /*
         * Explanation:
         * The .gameObject property retrieves the parent "GameObject" that the 
         * "itemImage" component is attached to. 
         * 
         * .SetActive(false) disables the GameObject that itemImage is attached to. 
         * This action:
         * 1. Makes the GameObject invisible in the scene (for UI, it disappears from the scene).
         * 2. Disables all components on the GameObject, including any scripts and their behaviors.
         * 3. Prevents the GameObject from being involved in rendering, physics, or other 
         * engine processes. 
         * 4. Disabling the GameObject affects the entire object, not just the itemImage component.
         * If I only wanted to hide the image, I could adjust its "enabled" property instead.
         * 
         * We want to temporarily hide a UI element while keeping it 
         * in memory to reuse later. We can use it when clearing an 
         * inventory slot or hiding an inactive item in the game. 
         */
        empty = true;
    }

    public void Deselect()
    {
        borderImage.enabled = false;
    }

    public void SetData(Sprite mySprite, int myQuantity)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = mySprite;
        this.quantityTxt.text = myQuantity.ToString();
        empty = false;
    }

    public void Select()
    {
        borderImage.enabled = true;
    }

    public void OnBeginDrag()
    {
        if (empty)
        {
            return;
        }
        OnItemBeginDrag?.Invoke(this);
    }
        
        /*
         * Explanation: 
         * This is a public method that might be called by Unity's drag-and-drop system.
         * If implemented in a class that derives from MonoBehavior, it could be tied into 
         * the Begin Drag event of Unity's IDragHandler interface. 
         * 
         * If the item is empty, the method returns early, and no drag 
         * behavior is initiated.
         * 
         * OnItemBeginDrag?.Invoke(this)
         * Other parts of the program (subscribed to OnItemBeginDrag) are notified 
         * when dragging starts. 
         * 
         * OnItemBeginDrag?.Invoke(this);
         * This lines uses a null-conditional operator to safely invoke the 
         * OnItemBeginDrag event.
         *
         * The null-conditional operator(?.) in C# is a shorthand way to perform
         * a null check before accessing members (properties, methods, fields) of an
         * object. It helps prevent NullReferenceException errors 
         * by ensuring that if the object is null, the operation is safely skipped 
         * rather than throwing an exception. 
         * 
         * OnItemBeginDrag:
         * This is likely a delegate or event defined elsewhere in the script or class.
         * It is invoked only if it is not null (aka there are subscribers to the event). 
         * 
         * this:
         * The current instance of the class is passed as a parameter to the event, allowing 
         * subscribers to access this object. (aka to identify the item being dragged). 
         * 
         * Example Scenario:
         * 1. If the slot is empty, the drag is ignored. 
         * 2. If the slot is not empty, the OnItemBeginDrag event is invoked, 
         * notifying other parts of the system (e.g. to create a dragging visual or to 
         * handle inventory logic.)
         * 
         */


    
}
