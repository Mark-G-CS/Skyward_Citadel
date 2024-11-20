using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class InventorySO : ScriptableObject
{
    public event Action<Dictionary<int, InventoryItemData>> OnInventoryUpdated;

    [SerializeField]
    private List<InventoryItemData> intetoryItems;

    [field: SerializeField]
    public int Size
    {
        get;
        private set;
    } = 10;

    public void Initialize()
    {

    }
}
