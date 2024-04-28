using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] GameObject _particlesAppear;
    [SerializeField] List<GameObject> _particlesPortal;
    [SerializeField] float _imageAppearDuration;
    
    IEnumerator Spawn(GameObject enemy)
    {
        TurnOnPortal();
        
        yield return new WaitForSeconds(_imageAppearDuration);
        TurnOnAppear();
        CreateGo(enemy);
        TurnOffPortal();
        TurnOffParticle(_particlesAppear.GetComponent<ParticleSystem>());
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
    void TurnOffParticle(ParticleSystem particle)
    {
        particle.loop = false;
    }
    void TurnOffPortal()
    {
        foreach(GameObject go in _particlesPortal)
        {
            TurnOffParticle(go.GetComponent<ParticleSystem>());
        }
    }
    private void Start()
    {
        foreach(GameObject go in _particlesPortal)
        {
            go.SetActive(false);
        }
        _particlesAppear.SetActive(false);
    }
    void TurnOnPortal()
    {
        foreach (GameObject go in _particlesPortal)
        {
            go.SetActive(true);
            go.GetComponent<ParticleSystem>().loop = true;
            go.GetComponent<ParticleSystem>().time = 0;
            go.GetComponent<ParticleSystem>().Play();


        }
    }
    void TurnOnAppear()
    {
        _particlesAppear.SetActive(true);
        _particlesAppear.GetComponent<ParticleSystem>().loop = true;
        _particlesAppear.GetComponent<ParticleSystem>().time = 0;
        _particlesAppear.GetComponent<ParticleSystem>().Play();
    }

}
