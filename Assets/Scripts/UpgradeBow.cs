using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBow : MonoBehaviour
{
	[SerializeField]
	private CharacterStatus _characterStatus;

	public void UpdateBow()
	{
		if (_characterStatus.BowLevel == 1 && DataHolder.coins >= 100)
		{
			_characterStatus.BowLevel = 2;
			_characterStatus.BowDamage = 45;
			DataHolder.update_coins(DataHolder.coins - 100);
		}
	}
}
