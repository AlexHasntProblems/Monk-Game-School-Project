using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthPotion : Item
{
    public override void Apply(uint count)
    {
        Player.Instance.Heal(Convert.ToSingle(count));
    }
}
