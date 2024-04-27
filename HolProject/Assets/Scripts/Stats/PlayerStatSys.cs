using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStatSys : MonoBehaviour
{
    [SerializeField] private int _hp=100;

    private Stat _hpMax = new Stat(StatName.MaxHp, 100);
    private Stat _atc = new Stat(StatName.Atack, 3);
    private Stat _def = new Stat(StatName.Defence, 1);
    private Stat _critCh = new Stat(StatName.CritChance, 15, 100);
    private Stat _critDmg = new Stat(StatName.CritDamage, 1.1f);

    [SerializeField] private List<Stat> _parametrs = new List<Stat>();

    [Header("Actions")]
    public static Action<int> OnToMaxHp;
    public static Action<int> OnToHp;

    private void Start()
    {
        _parametrs.Add(_hpMax); 
        _parametrs.Add(_atc); 
        _parametrs.Add(_def); 
        _parametrs.Add(_critCh); 
        _parametrs.Add(_critDmg);

        OnToMaxHp?.Invoke(_hp);
        OnToHp?.Invoke(_hp);

    }

    #region HP section
    public int GetHP() { return _hp; }
    private void MaxHP() {  _hp = (int)_hpMax.value; }

    public void AddHP(int amount) { _hp = (_hp + amount > _hpMax.value ? (int)_hpMax.value : _hp + amount); OnToHp?.Invoke(_hp);}

    public void AddHP(float amount) { AddHP((int)amount); }
    
    private void RemoveHP(int amount)
    {
        _hp -= amount;
        if (_hp <= 0)
        {
            _hp = 0;
            Death();
        }
    }

    private void RemoveHP(float amount) { RemoveHP((int)amount); }

    #endregion 

    public void GetDamage(int amount) { RemoveHP(amount / _def.value); OnToHp?.Invoke(_hp); }
    public void Attack() { float damage = _atc.value * (UnityEngine.Random.Range(0, 100) > _critCh.value ? 1 : _critDmg.value); }

    # region Stat section
    private void GainStat(int id, float amount) { _parametrs[id].GainStat(amount); }
    private void GainStatPercent(int id, float percent) { _parametrs[id].GainStatPercent(percent); }
    public void GainStatByName(StatName name, float amount, bool isPercent=false) 
    {
        for (int i = 0; i < _parametrs.Count; i++)
        {
            if (_parametrs[i].statName == name)
            {
                if (isPercent) GainStatPercent(i, amount);
                else GainStat(i, amount);
                break;
            }
        }
    }

    public void RemoveStatByName(StatName name, float amount, bool isPercent = false)
    {
        amount *= -1;
        GainStatByName(name, amount, isPercent);
    }

#endregion

    private void Death() 
    {
        GameObject.FindGameObjectWithTag("CardsHolder").GetComponent<Animator>().SetTrigger("AppearCards");
        Time.timeScale = 0;
    }
}


