using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
[Serializable]
public class ItemBase : ScriptableObject
{
    [SerializeField] string _name;
    [SerializeField] GameObject _model;
    public Sprite _icon;
}
