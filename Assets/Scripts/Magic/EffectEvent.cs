//Written by Angel Rubio

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]

public class EffectEvent : UnityEvent<Player, GameObject, BaseSpell> { }