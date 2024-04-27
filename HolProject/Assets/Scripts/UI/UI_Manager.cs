using System;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private Slider _helthbar;

    public static Action OnTakeMaxHP;

    private void OnEnable()
    {
        PlayerStatSys.OnToMaxHp += GetMaxHP;
        PlayerStatSys.OnToHp += GetHp;
    }

    private void OnDisable()
    {
        PlayerStatSys.OnToMaxHp -= GetMaxHP;
        PlayerStatSys.OnToHp -= GetHp;
    }

    private void GetMaxHP(int value)
    {
        _helthbar.maxValue = value;
    }

    private void GetHp(int value)
    {
        _helthbar.value = value;
    }
}
