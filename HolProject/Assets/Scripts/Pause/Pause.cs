using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject _pauseObject;

    public static bool isPaused;

    PlayerInput _playerInput;

    [SerializeField] private GameObject _settingObject;
    [SerializeField] private GameObject _fullPrefab;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _fullPrefab.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _playerInput.Pause.Pause.performed += ctx => InputPause();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    public void InputPause()
    {
        isPaused = !isPaused;
        if (isPaused )
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        _fullPrefab.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        _fullPrefab.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Setting()
    {
        _settingObject.SetActive(true);
        _pauseObject.SetActive(false);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }

}
