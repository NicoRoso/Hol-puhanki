using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour
{
    [SerializeField] private Animator _loadingScreen;
    [SerializeField] private Slider _progressBar;
    [SerializeField] private bool _endAnim;


    public void EndLoadingAnimation()    { _endAnim = false; }
    public void Awake() { DontDestroyOnLoad(this); }

    public void LoadLevel(string name)
    {
        _loadingScreen.SetTrigger("StartAnim");
        GetComponent<Canvas>().sortingOrder = 1;
        _endAnim = true;
        _progressBar.value = 0;
        StartCoroutine(AsyncLoad(name));
    }

    IEnumerator AsyncLoad(string name)
    {
        while (_endAnim)
        {
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.1f);

        AsyncOperation loading = SceneManager.LoadSceneAsync(name);
        loading.allowSceneActivation = false;

        while (loading.progress < 0.9f)
        {
            _progressBar.value = _progressBar.value = Mathf.Clamp01(loading.progress / 0.9f);
            yield return null;
        }

        _progressBar.value = _progressBar.value = Mathf.Clamp01(loading.progress / 0.9f);
        yield return new WaitForSeconds(0.5f);
        loading.allowSceneActivation = true;
        _loadingScreen.SetTrigger("EndAnim");
    }
}