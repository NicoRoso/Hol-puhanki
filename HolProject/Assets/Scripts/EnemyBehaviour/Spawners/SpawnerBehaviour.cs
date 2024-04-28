using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    [SerializeField] GameObject _particlesAppear;
    [SerializeField] List<GameObject> _particlesPortal;
    [SerializeField] float _imageAppearDuration;

    public void SpawnEnemy(GameObject spawningObject)
    {
        StartCoroutine(Spawn(spawningObject));
    }
    void TurnOffParticle(ParticleSystem particle)
    {
        particle.Stop();
    }
    void TurnOffAppear()
    {
        TurnOffParticle(_particlesAppear.GetComponent<ParticleSystem>());
    }
    void TurnOffPortal()
    {
        foreach (GameObject go in _particlesPortal)
        {
            TurnOffParticle(go.GetComponent<ParticleSystem>());
        }
    }
    void TurnOnPortal()
    {
        foreach (GameObject go in _particlesPortal)
        {
            go.SetActive(true);
            go.GetComponent<ParticleSystem>().time = 0;
            go.GetComponent<ParticleSystem>().Play();


        }
    }
    void TurnOnAppear()
    {
        _particlesAppear.SetActive(true);
        _particlesAppear.GetComponent<ParticleSystem>().time = 0;
        _particlesAppear.GetComponent<ParticleSystem>().Play();
    }
    IEnumerator Spawn(GameObject enemy)
    {
        TurnOnPortal();
        GetComponent<AudioManager>().Play("portal" + UnityEngine.Random.Range(1,3));
        yield return new WaitForSeconds(_imageAppearDuration);
        TurnOnAppear();
        CreateGo(enemy);
        TurnOffPortal();
        TurnOffParticle(_particlesAppear.GetComponent<ParticleSystem>());
        yield break;
    }
    void CreateGo(GameObject go)
    {
        GameObject spawnedObject = Instantiate(go, transform.position, Quaternion.identity, null);
        spawnedObject.AddComponent<MinusEnemyOnDeath>();
    }
    private void Start()
    {
        TurnOffAppear();
        TurnOffPortal();
    }



}
