﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : CollectableObject
{
    protected override void OnPickUp()
    {
        DestroySelf();
    }
}
