using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeBow : MonoBehaviour
{
	[SerializeField]
	private CharacterStatus _characterStatus;

	public void Start()
	{
		UpdateUpgradeMenu();
	}

	public void UpdateBow()
	{
		var cost = 50 + _characterStatus.BowLevel * 50;
		if (_characterStatus.BowLevel < 20 && DataHolder.coins >= cost)
		{
			_characterStatus.BowLevel++;
			_characterStatus.BowDamage += 10;
			DataHolder.update_coins(DataHolder.coins - cost);

			UpdateUpgradeMenu();
		}
	}

	private void UpdateUpgradeMenu()
	{
		transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"lv.{_characterStatus.BowLevel}";

		if (_characterStatus.BowLevel < 20)
		{
			transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = $"x{50 + _characterStatus.BowLevel * 50}";
			transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = $"lv.{_characterStatus.BowLevel + 1}";
		}
		else
		{
			transform.GetChild(2).gameObject.SetActive(false);
			transform.GetChild(3).gameObject.SetActive(false);
			transform.GetChild(5).gameObject.SetActive(false);
			transform.GetChild(6).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Макс. уровень";
		}
	}
}
