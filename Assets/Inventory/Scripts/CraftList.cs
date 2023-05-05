using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[System.Serializable]
public class CraftItem
{
    public Resource Resource;
    public Resource[] Ingredients;

    public CraftItem(Resource resource, params Resource[] ingredients )
    {
        Resource = resource;
        Ingredients = ingredients;
    }

    public string GetRecipe()
    {
        var ingredients = new StringBuilder();
        foreach (var ingredint in Ingredients)
        {
            ingredients.Append(ingredint.Item.Name);
            ingredients.Append(" x");
            ingredients.Append(ingredint.Amount);
            ingredients.Append(", ");
        }
        ingredients.Remove(ingredients.Length - 2, 2);

        return ingredients.ToString();
    }
}

[CreateAssetMenu]
public class CraftList : ScriptableObject, IEnumerable<CraftItem>
{
    [SerializeField] private List<CraftItem> _craftList = new();

    public IEnumerator<CraftItem> GetEnumerator()
    {
        return _craftList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return (IEnumerator)GetEnumerator();
    }

    public CraftItem GetCraftItem(string itemName)
    {
        foreach (var item in _craftList)
        {
            if (item.Resource.Item.Name == itemName)
                return item;
        }

        return null;
    }
}
