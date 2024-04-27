using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStatSys : MonoBehaviour
{
    [SerializeField] private int _hp=100;

    private Stat _hpMax = new Stat("MaxHP", 100);
    private Stat _atc = new Stat("Atack", 3);
    private Stat _def = new Stat("Defence", 1);
    private Stat _critCh = new Stat("Crit Chance", 15, 100);
    private Stat _critDmg = new Stat("Crit Damage", 1.1f);

    [SerializeField] private List<Stat> _parametrs = new List<Stat>();



    private void Start()
    {
        _parametrs.Add(_hpMax); 
        _parametrs.Add(_atc); 
        _parametrs.Add(_def); 
        _parametrs.Add(_critCh); 
        _parametrs.Add(_critDmg); 
    }

    #region HP
    private int GetHP() { return _hp; }
    private void MaxHP() {  _hp = (int)_hpMax.value; }
    public void AddHP(int amount) { _hp += (_hp + amount > _hpMax.value ? (int)_hpMax.value : amount); }
    public void AddHP(float amount) { AddHP((int)amount); }
    
    private void RemoveHP(int amount)
    {
        _hp -= amount;
        if (_hp < 0)
        {
            _hp = 0;
            Death();
        }
    }

    private void RemoveHP(float amount) { RemoveHP((int)amount); }

    #endregion

    public void GetDamage(int amount) { RemoveHP(amount / _def.value); }

    public void Attack() { float damage = _atc.value * (UnityEngine.Random.Range(0, 100) > _critCh.value ? 1 : _critDmg.value); }
    private void GainStat(int id, float amount) { _parametrs[id].GainStat(amount); }
    private void GainStatPercent(int id, float percent) { _parametrs[id].GainStatPercent(percent); }
    public void GainStatByName(string name, float amount, bool isPercent=false) 
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

    private void Death() {  }
}


