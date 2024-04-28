using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticWavesValue
{
    static int enemiesInWaveLeft;
    public static void EraseData()
    {
        enemiesInWaveLeft = 100000;
    }
    public static int GetLeftEnemies()
    {
        return enemiesInWaveLeft;
    }
    public static void SetEnemiesLeft(int value)
    {
        enemiesInWaveLeft = value;
    }
    public static void MinusEnemy()
    {
        enemiesInWaveLeft--;
    }
}
