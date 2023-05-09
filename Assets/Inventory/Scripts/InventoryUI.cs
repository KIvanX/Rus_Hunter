using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private List<Image> _icons = new();
    [SerializeField] private List<TextMeshProUGUI> _amounts = new();
    [SerializeField] private GridLayoutGroup _grid;
    [SerializeField] private CraftList _craftList;
    [SerializeField] private Transform _craftItemPrefab;
    [SerializeField] private Inventory _inventory;

    private void Start()
    {
        foreach (var craftItem in _craftList)
        {
            _craftItemPrefab.GetChild(1).GetComponent<Image>().sprite = craftItem.Resource.Item.Icon;
            _craftItemPrefab.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = craftItem.Resource.Amount.ToString();
            _craftItemPrefab.GetChild(2).GetComponent<TextMeshProUGUI>().text = craftItem.Resource.Item.Name;
            _craftItemPrefab.GetChild(3).GetComponent<TextMeshProUGUI>().text = craftItem.GetRecipe();

            Instantiate(_craftItemPrefab, _grid.transform);
        }
    }

    public void OpenUI()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }
        else
        {
            gameObject.SetActive(true);
            Time.timeScale = 0;
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < _icons.Count; i++)
        {
            if (i < _inventory.GetSize())
            {
                _icons[i].color = new Color(1, 1, 1, 1);
                _icons[i].sprite = _inventory[i].Item.Icon;
                _amounts[i].text = _inventory[i].Amount > 1 ? _inventory[i].Amount.ToString() : "";
            }
            else
            {
                _icons[i].color = new Color(1, 1, 1, 0);
                _amounts[i].text = "";
            }
        }
    }

    public void Craft(string resourceName)
    {
        var craftItem = _craftList.GetCraftItem(resourceName);
        if (_inventory.Craft(craftItem))
            Debug.Log("Craft Success");
        else
            Debug.Log("Craft Error");
    }
}
