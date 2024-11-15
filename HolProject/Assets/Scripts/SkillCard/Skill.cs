using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] private string _skillName;
    [SerializeField] private string _skillDesc;
    [SerializeField] private List<SkillItem> _skillItems = new List<SkillItem>();

    [SerializeField] private TMP_Text textName;
    [SerializeField] private TMP_Text textdesc;

    public void SkillCreate(string skillName, string skillDesc, SkillItem skills)
    {
        textName.text = skillName;
        textdesc.text = skillDesc;
        _skillItems.Add(skills);
    }

    public void GainSkill()
    {
        PlayerStatSys playerStats = FindAnyObjectByType<PlayerStatSys>();

        foreach (var item in _skillItems)
        {
            playerStats.GainStatByName(item.statName, item.statValue, item.statIsPercent);
        }

        GameObject.FindGameObjectWithTag("CardsHolder").GetComponent<Animator>().SetTrigger("DisappearCards");
        FindObjectOfType<SkillCount>().skillCount++;

        GameObject.FindAnyObjectByType<LoadScreen>().LoadLevel("Level_0_Library");
    }
}
/*
[Serializable]
public class SkillDictionary
{
    [SerializeField] private SkillDictionaryItem[] _dictItems;

    public Dictionary<StatName, (float, bool)> ConvertToUnityDict()
    {
        Dictionary<StatName, (float, bool)> dict = new Dictionary<StatName, (float, bool)>();
        foreach (SkillDictionaryItem item in _dictItems)
        {
            dict.Add(item.statName, (item.statValue, item.statIsPercent));
        }
        return dict;
    }
}*/

[Serializable]
public class SkillItem
{
    public StatName statName;
    public float statValue;
    public bool statIsPercent;

    public SkillItem(StatName statName, float statValue, bool statIsPercent)
    { 
        this.statName = statName;
        this.statValue = statValue;
        this.statIsPercent = statIsPercent;
    }
}