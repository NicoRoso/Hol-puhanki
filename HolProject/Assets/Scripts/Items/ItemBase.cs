using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
[Serializable]
public class ItemBase : ScriptableObject
{
    public string _name;
    public string _desc;
    [SerializeField] GameObject _model;
    public Sprite _icon;
}
