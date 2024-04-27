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

    [SerializeField] List<ItemBase> _hotbarItems;
    [SerializeField] List<SelectSlot> _hotbarSlots;
    [SerializeField] List<HotbarInventorySlot> _inventoryHotbarSlots;
    [SerializeField] List<ItemBase> _inventoryItems;
    [SerializeField] List<InventorySlot> _inventorySlots;

    [SerializeField] HotbarSelecting _hotBar;
    [SerializeField] InventoryUI _invUI;
    [SerializeField] int _currentItemID;

    private void Start()
    {
        _playerStatSys = FindObjectOfType<PlayerStatSys>();

        _hotBar = FindObjectOfType<HotbarSelecting>();
        _hotbarSlots = _hotBar._slots;

        _invUI = FindObjectOfType<InventoryUI>();
        foreach (var slot in _invUI.GetComponentsInChildren<HotbarInventorySlot>()) { _inventoryHotbarSlots.Add(slot); }
        foreach (var slot in _invUI.GetComponentsInChildren<InventorySlot>()) { _inventorySlots.Add(slot); }

        _invUI.gameObject.SetActive(false);

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
        _playerInput.InventoryBtns.OpenAndClose.performed += ctx => OpenCloseInventory();
    }

    void OpenCloseInventory()
    {
        bool invOpen = _invUI.gameObject.activeSelf;
        _hotBar.gameObject.SetActive(invOpen);
        _invUI.gameObject.SetActive(!invOpen);

        if (!invOpen)
        {
            for (int i = 0; i < 4; i++)
            {
                if (_hotbarItems[i] is not null)
                {
                    _inventoryHotbarSlots[i].SetImage(_hotbarItems[i]._icon);
                    if (_hotbarItems[i] is Sword) {
                        _inventoryHotbarSlots[i].gameObject.GetComponentInChildren<DraggableItem>().isSword = true;
                    }
                    else
                        _inventoryHotbarSlots[i].gameObject.GetComponentInChildren<DraggableItem>().isSword = false;
                }
                else
                    _inventoryHotbarSlots[i].RemoveImage();
            }
            for (int i = 0; i < 15; i++)
            {
                if (_inventoryItems[i] is not null) 
                { 
                    _inventorySlots[i].SetImage(_inventoryItems[i]._icon);
                    _inventorySlots[i].gameObject.GetComponentInChildren<DraggableItem>().isSword = false;
                }
                else
                    _inventorySlots[i].RemoveImage();
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                if (_hotbarItems[i] is not null)
                    _hotbarSlots[i].SetImage(_hotbarItems[i]._icon);
                else
                    _hotbarSlots[i].RemoveImage();
            }
        }
    }

    void ImagesReset()
    {
        int i = 0;
        while (_hotbarItems[i] is not null)
        {
            _hotbarSlots[i].SetImage(_hotbarItems[i]._icon);
            i++;
        }
        while (i < _hotbarSlots.Count)
        {
            _hotbarSlots[i].RemoveImage();
            i++;
        }
    }

    private void SelectItem(int id)
    {
        if (id != _currentItemID+1 && _hotbarItems[id] is not null)
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
        var item = _hotbarItems[_currentItemID];
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
        var item = _hotbarItems[_currentItemID];
        if (item is Potion)
        {
            _hotbarSlots[_currentItemID].RemoveImage();
            StartCoroutine( DrinkPotion((Potion)item));
            _hotbarItems[_currentItemID] = null;
            FindNextItem();
        }
        else if (item is HealthPotion)
        {
            _hotbarSlots[_currentItemID].RemoveImage();
            _playerStatSys.AddHP(((HealthPotion)item).value);
            _hotbarItems[_currentItemID] = null;
            FindNextItem();
        }
    }

    void FindNextItem()
    {
        int i = _currentItemID-1;
        while (i >= 0)
        {
            if (_hotbarItems[i] is not null)
            {
                SelectItem(i+1);
                return;
            }
            i--;
        }
        i = _currentItemID + 1;
        while (i <= _hotbarSlots.Count)
        {
            if (_hotbarItems[i] is not null)
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
