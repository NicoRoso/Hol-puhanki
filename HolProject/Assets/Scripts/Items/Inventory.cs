using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{

    [SerializeField] private PlayerInput _playerInput;
    [Space]
    [SerializeField] private PlayerStatSys _playerStatSys;
    [Space]

    [SerializeField] List<ItemBase> _items;
    [SerializeField] List<SelectSlot> _slots;
    [SerializeField] HotbarSelecting _hotBar;
    [SerializeField] int _currentItemID;

    private void Start()
    {
        _playerStatSys = FindObjectOfType<PlayerStatSys>();

        _hotBar = FindObjectOfType<HotbarSelecting>();
         _slots = _hotBar._slots;

        ImagesReset();

        _currentItemID = 0;
        _hotBar.SelectSlotByKey(0);
        SwordStatChange(true);

        _playerInput = new PlayerInput();
        _playerInput.Enable();

        _playerInput.Hotbar._1stslot.performed += ctx => SelectItem(1);
        _playerInput.Hotbar._2ndslot.performed += ctx => SelectItem(2);
        _playerInput.Hotbar._3rdslot.performed += ctx => SelectItem(3);
        _playerInput.Hotbar._4thslot.performed += ctx => SelectItem(4);

        _playerInput.UsePoution.UsePotion.performed += ctx => DrinkPot();
    }


    void ImagesReset()
    {
        int i = 0;
        while (_items[i] is not null)
        {
            _slots[i].SetImage(_items[i]._icon);
            i++;
        }
        while (i < _slots.Count)
        {
            _slots[i].RemoveImage();
            i++;
        }
    }

    private void SelectItem(int id)
    {
        if (id != _currentItemID+1 && _items[id-1] is not null)
        {
            SwordStatChange(false);

            id -= 1;
            _hotBar.SelectSlotByKey(id);

            _currentItemID = id;
            SwordStatChange(true);
        }
    }


    private void SwordStatChange(bool gain)
    {
        var item = _items[_currentItemID];
        if (item is Sword)
        {
            foreach (Stat stat in ((Sword)item).parametrs)
            {
                if (gain) _playerStatSys.GainStatByName(stat.statName, stat.value);
                else _playerStatSys.RemoveStatByName(stat.statName, stat.value);
            }
        }
    }

    private void DrinkPot()
    {
        var item = _items[_currentItemID];
        if (item is Potion)
        {
            _slots[_currentItemID].RemoveImage();
            StartCoroutine( DrinkPotion((Potion)item));
            _items[_currentItemID] = null;
            FindNextItem();
        }
        else if (item is HealthPotion)
        {
            _slots[_currentItemID].RemoveImage();
            _playerStatSys.AddHP(((HealthPotion)item).value);
            _items[_currentItemID] = null;
            FindNextItem();
        }
    }

    void FindNextItem()
    {
        int i = _currentItemID-1;
        while (i >= 0)
        {
            if (_items[i] is not null)
            {
                SelectItem(i+1);
                return;
            }
            i--;
        }
        i = _currentItemID + 1;
        while (i <= _slots.Count)
        {
            if (_items[i] is not null)
            {
                SelectItem(i+1);
                return;
            }
            i++;
        }
    }

    public IEnumerator DrinkPotion(Potion potion)
    {
        _playerStatSys.GainStatByName(potion.parametr.statName, potion.parametr.value);
        yield return new WaitForSeconds(potion.duration);
        _playerStatSys.RemoveStatByName(potion.parametr.statName, potion.parametr.value);
    }

}
