﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IListener
{
    void Invoke(GameObject sender);
}
