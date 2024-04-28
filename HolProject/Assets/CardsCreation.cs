using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsCreation : MonoBehaviour
{

    [SerializeField] Skill card1;
    [SerializeField] Skill card2;
    [SerializeField] Skill card3;
    [SerializeField] Skill[] cards = new Skill[3];
    private void Start()
    {
        cards[0] = card1;
        cards[1] = card2;
        cards[2] = card3;

        var names = FindObjectOfType<SkillCount>().names;

        foreach (var el in cards)
        {
            var i = Random.Range(0, names.Count-1);
            el.SkillCreate(names[i].Item1, names[i].Item2, names[i].Item3);
            names.Remove(names[i]);
        }
    }
}
