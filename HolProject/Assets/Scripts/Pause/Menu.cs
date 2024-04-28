using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _settingObject;
    [SerializeField] private GameObject _fullPrefab;

    private void Awake()
    {
        _settingObject.SetActive(true);
        Time.timeScale = 1;
    }

    public void StartGame(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }

    public void Setting()
    {
        _settingObject.SetActive(true);
        _fullPrefab.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
