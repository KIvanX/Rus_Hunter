using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventorySlot
{
    public Item Item;
    public int Amount;

    public InventorySlot(Item item, int amount = 1)
    {
        Item = item;
        Amount = amount;
    }
}

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<InventorySlot> _slots = new();
    [SerializeField] private int _maxSize = 10;

    public UnityEvent OnInventoryUpdate;

    public void AddItems(Item item, int amount = 1)
    {
        foreach (InventorySlot slot in _slots)
        {
            if (slot.Item.Id == item.Id)
            {
                slot.Amount += amount;
                OnInventoryUpdate.Invoke();
                return;
            }
        }

        _slots.Add(new InventorySlot(item, amount));
        OnInventoryUpdate.Invoke();
    }

    public int GetSize()
    {
        return _slots.Count;
    }

    public InventorySlot this[int index]
    {
        get => _slots[index];
    }
}
