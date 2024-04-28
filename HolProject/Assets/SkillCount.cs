using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCount : MonoBehaviour
{
    public List<(string, string, SkillItem)> names = new List<(string, string, SkillItem)>();
    public int skillCount = 0;

    private void Start()
    {
        names.Add(("������� �����", "������ �������� ������", new SkillItem(StatName.Defence, 4, false)));
        names.Add(("������� �����", "������� �������� ������", new SkillItem(StatName.Defence, 2, false)));
        names.Add(("������� �����", "�������� ������", new SkillItem(StatName.Defence, 3, false)));
        names.Add(("���������� ���������", "������� �������� ���� ������������ ���������", new SkillItem(StatName.CritChance, 0.05f, false)));
        names.Add(("����������� ���������", "�������� ���� ������������ ���������", new SkillItem(StatName.CritChance, 0.1f, false)));
        names.Add(("��������� ��������", "������ �������� ���� ������������ ���������", new SkillItem(StatName.CritChance, 0.15f, false)));
        names.Add(("�����������", "������� �������� ����������� ����", new SkillItem(StatName.CritDamage, 0.05f, false)));
        names.Add(("�����", "�������� ����������� ����", new SkillItem(StatName.CritDamage, 0.1f, false)));
        names.Add(("������", "������ �������� ����������� ����", new SkillItem(StatName.CritDamage, 0.15f, false)));
    }
}
