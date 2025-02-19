using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : Item
{
    public override void Apply(uint count)
    {
        Player.Instance.BladesCount += count;
    }
}
