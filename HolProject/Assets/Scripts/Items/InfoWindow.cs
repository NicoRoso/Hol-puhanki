using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoWindow : MonoBehaviour
{
    [SerializeField] Color defaultColor;
    [SerializeField] TMP_Text nameObj;
    [SerializeField] TMP_Text desc;
    [SerializeField] Image icon;

    public void Fill(string nameObj, string desc, Sprite icon)
    {
        this.nameObj.text = nameObj;
        this.desc.text = desc;
        this.icon.sprite = icon;
        this.icon.color = Color.white;
    }

    public void Clear()
    {
        this.nameObj.text = null;
        this.desc.text = null;
        this.icon.sprite = null;
        this.icon.color = defaultColor;
    }
}
