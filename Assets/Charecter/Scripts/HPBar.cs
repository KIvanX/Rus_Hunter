using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    private Image _hpBar;

    private void Start()
    {
        _hpBar = GetComponent<Image>();
    }

    public void UpdateUI(float hp)
    {
        _hpBar.fillAmount = hp / 100;
    }
}
