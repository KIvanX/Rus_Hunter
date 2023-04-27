using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<Resource> _slots = new();
    private const int _maxSize = 10;

    public UnityEvent OnInventoryUpdate;

    public bool AddItems(Item item, int amount = 1)
    {
        foreach (Resource slot in _slots)
        {
            if (slot.Item.Id == item.Id)
            {
                slot.Amount += amount;
                OnInventoryUpdate.Invoke();
                return true;
            }
        }

        if (_slots.Count < _maxSize)
        {
            _slots.Add(new Resource(item, amount));
            OnInventoryUpdate.Invoke();
            return true;
        }

        return false;
    }

    public int GetSize()
    {
        return _slots.Count;
    }

    public Resource this[int index]
    {
        get => _slots[index];
    }
}
