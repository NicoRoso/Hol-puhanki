using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] SpriteRenderer _image;
    [SerializeField] float _imageAppearDuration;
    IEnumerator Spawn(GameObject enemy)
    {
        float timer = 0;
        Color startColor = _image.color;
        Color endColor = _image.color;
        endColor.a = 1;
        while(timer < _imageAppearDuration)
        {
            timer += Time.deltaTime;
            _image.color = Color.Lerp(startColor, endColor, timer / _imageAppearDuration);
            yield return null;
        }

        // effects of spawn
        CreateGo(enemy);
        yield return null;
        timer = 0;

        while (timer < _imageAppearDuration)
        {
            timer += Time.deltaTime;
            _image.color = Color.Lerp(endColor, startColor, timer / _imageAppearDuration);
            yield return null;
        }
        yield break;
    }
    
    public void SpawnEnemy(GameObject enemy)
    {
        StartCoroutine(Spawn(enemy));
    }
    void CreateGo(GameObject go)
    {
        Instantiate(go, transform.position, Quaternion.identity, null);
    }
}
