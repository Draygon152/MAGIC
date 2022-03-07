// Written by Angel Rubio

using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class EffectEvent : UnityEvent<Player, GameObject, BaseSpell> { }