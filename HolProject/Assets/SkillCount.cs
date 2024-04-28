using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCount : MonoBehaviour
{
    public List<(string, string, SkillItem)> names = new List<(string, string, SkillItem)>();
    public int skillCount = 0;

    private void Start()
    {
        names.Add(("Прочные кости", "Сильно повышает защиту", new SkillItem(StatName.Defence, 4, false)));
        names.Add(("Хрупкие кости", "Немного повышает защиту", new SkillItem(StatName.Defence, 2, false)));
        names.Add(("Древние кости", "Повышает защиту", new SkillItem(StatName.Defence, 3, false)));
        names.Add(("Нахождение слабостей", "Немного повышает Шанс критического попадания", new SkillItem(StatName.CritChance, 0.05f, false)));
        names.Add(("Обнаружение слабостей", "Повышает Шанс критического попадания", new SkillItem(StatName.CritChance, 0.1f, false)));
        names.Add(("Очевидные слабости", "Сильно повышает Шанс критического попадания", new SkillItem(StatName.CritChance, 0.15f, false)));
        names.Add(("Негодование", "Немного повышает Критический урон", new SkillItem(StatName.CritDamage, 0.05f, false)));
        names.Add(("Злоба", "Повышает Критический урон", new SkillItem(StatName.CritDamage, 0.1f, false)));
        names.Add(("Ярость", "Сильно повышает Критический урон", new SkillItem(StatName.CritDamage, 0.15f, false)));
    }
}
