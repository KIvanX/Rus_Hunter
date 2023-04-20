using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    public float HP { get; set; }
    public void TakeDamage(float damage);
}
