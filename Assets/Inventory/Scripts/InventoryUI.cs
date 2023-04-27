using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private List<Image> _icons = new();
    [SerializeField] private List<TextMeshProUGUI> _amounts = new();
    [SerializeField] private GridLayoutGroup _grid;
    [SerializeField] private CraftList _craftList;
    [SerializeField] private Transform _craftItemPrefab;

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

    public void UpdateUI(Inventory inventory)
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);

            for (int i = 0; i < _icons.Count; i++)
            {
                if (i < inventory.GetSize())
                {
                    _icons[i].color = new Color(1, 1, 1, 1);
                    _icons[i].sprite = inventory[i].Item.Icon;
                    _amounts[i].text = inventory[i].Amount > 1 ? inventory[i].Amount.ToString() : "";
                }
                else
                {
                    _icons[i].color = new Color(1, 1, 1, 0);
                    _amounts[i].text = "";
                }
            }
        }
    }
}
