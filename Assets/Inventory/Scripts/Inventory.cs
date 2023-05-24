using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour, IEnumerable<Resource>
{
    [SerializeField]
    private List<Resource> _slots = new();
    private const int _maxSize = 10;

    public UnityEvent OnShortInventoryUpdate;
    public UnityEvent OnFullInventoryUpdate;

    public bool AddItems(Item item, int amount = 1)
    {
        foreach (Resource slot in _slots)
        {
            if (slot.Item.Id == item.Id)
            {
                slot.Amount += amount;
                OnShortInventoryUpdate.Invoke();
                return true;
            }
        }

        if (_slots.Count < _maxSize)
        {
            _slots.Add(new Resource(item, amount));
            OnShortInventoryUpdate.Invoke();
            return true;
        }

        return false;
    }

    public bool Craft(CraftItem craftItem)
    {
        if (craftItem == null)
            return false;

        foreach (var ingredient in craftItem.Ingredients)
        {
            var itemAmount = _slots
                .Where(s => s.Item.Id == ingredient.Item.Id)
                .Select(s => s.Amount)
                .Sum();

            if (itemAmount < ingredient.Amount)
                return false;
        }

        foreach (var ingredient in craftItem.Ingredients)
        {
            var amount = ingredient.Amount;

            foreach (var slot in _slots.Where(s => s.Item.Id == ingredient.Item.Id))
            {
                if (slot.Amount >= amount)
                {
                    slot.Amount -= amount;
                    break;
                }
                else
                {
                    amount -= slot.Amount;
                    slot.Amount = 0;
                }
            }
        }

        _slots = _slots.Where(s => s.Amount > 0).ToList();
        AddItems(craftItem.Resource.Item, craftItem.Resource.Amount);
        OnShortInventoryUpdate.Invoke();
        OnFullInventoryUpdate.Invoke();

        return true;
    }

    public bool FindItem(int id)
        => _slots.Where(s => s.Item.Id == id).Any();

    public bool UseItem(int id)
    {
        if (!FindItem(id))
            return false;
        
        foreach (var slot in _slots)
            if (slot.Item.Id == id)
            {
                slot.Amount -= 1;
                break;
            }

		_slots = _slots.Where(s => s.Amount > 0).ToList();
		OnShortInventoryUpdate.Invoke();
		return true;

	}

    public int GetSize()
    {
        return _slots.Count;
    }

    public IEnumerator<Resource> GetEnumerator()
    {
        return _slots.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return (IEnumerator)GetEnumerator();
    }

    public Resource this[int index]
    {
        get => _slots[index];
    }
}
