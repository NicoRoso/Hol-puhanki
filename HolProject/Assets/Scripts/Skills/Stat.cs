using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    [field:SerializeField] public string statName { get; private set; }
    [field: SerializeField] public float value { get; private set; }
    [SerializeField] float _valueCap;

    public Stat(string name, float value, float valueCap = -1)
    {
        statName = name;
        this.value = value;
        if (valueCap <= 0) _valueCap = -1;
        else _valueCap = valueCap;
    }

    public void SetStat(float amount) { value = amount > _valueCap && _valueCap != -1 ? _valueCap : amount; ; }
    public void GainStat(float amount) { value = value + amount > _valueCap && _valueCap != -1 ? _valueCap : value + amount; ; }
    public void GainStatPercent(float percent) { value = value * percent > _valueCap && _valueCap != -1 ? _valueCap : value * percent; }
}
