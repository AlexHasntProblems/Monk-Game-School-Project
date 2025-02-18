using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string[] names;
    public uint MinSpawnCount;
    public uint MaxSpawnCount;

    public abstract void Apply(uint count);
}
