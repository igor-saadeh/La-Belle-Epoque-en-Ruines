using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents
{
    static public UnityEvent onGameover = new UnityEvent();
    static public UnityEvent onCollect = new UnityEvent();
}
