using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsCreation : MonoBehaviour
{
    [SerializeField] List<(string, string, SkillItem)> names = new List<(string, string, SkillItem)>();

    [SerializeField] Skill card1;
    [SerializeField] Skill card2;
    [SerializeField] Skill card3;
    [SerializeField] Skill[] cards = new Skill[3];
    private void Start()
    {
        cards[0] = card1;
        cards[1] = card2;
        cards[2] = card3;

        names.Add(("������� �����", "������ �������� ������", new SkillItem(StatName.Defence, 4, false)));
        names.Add(("������� �����", "������� �������� ������", new SkillItem(StatName.Defence, 2, false)));
        names.Add(("������� �����", "�������� ������", new SkillItem(StatName.Defence, 3, false)));
        names.Add(("���������� ���������", "������� �������� ���� ������������ ���������", new SkillItem(StatName.CritChance, 0.05f, false)));
        names.Add(("����������� ���������", "�������� ���� ������������ ���������", new SkillItem(StatName.CritChance, 0.1f, false)));
        names.Add(("��������� ��������", "������ �������� ���� ������������ ���������", new SkillItem(StatName.CritChance, 0.15f, false)));
        names.Add(("�����������", "������� �������� ����������� ����", new SkillItem(StatName.CritDamage, 0.05f, false)));
        names.Add(("�����", "�������� ����������� ����", new SkillItem(StatName.CritDamage, 0.1f, false)));
        names.Add(("������", "������ �������� ����������� ����", new SkillItem(StatName.CritDamage, 0.15f, false)));


        foreach (var el in cards)
        {
            var i = Random.Range(0, names.Count-1);
            el.SkillCreate(names[i].Item1, names[i].Item2, names[i].Item3);
            names.Remove(names[i]);
        }
    }
}
