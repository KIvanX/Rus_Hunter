using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Status")]
public class CharacterStatus : ScriptableObject
{
    public bool isSprinting;
    public bool isAiming;
    public bool isCrouching;
    public bool isStretching;

    public int BowLevel;
    public float BowDamage;
}
