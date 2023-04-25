using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private List<Image> _icons = new();
    [SerializeField] private List<TextMeshProUGUI> _amounts = new();

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
