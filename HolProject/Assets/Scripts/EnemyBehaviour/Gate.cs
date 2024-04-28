using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] StartWavesQueue _gateCloseTrigger;
    [SerializeField] SpawnerManager _spawnerManager;
    [SerializeField] float _heighDifference;
    Vector3 upPos;

    private void Start()
    {
        upPos = transform.position;
        _gateCloseTrigger.playerCollided += MoveDown;
        _spawnerManager.onEnemiesOver += MoveUp;
    }
    private void Update()
    {
        if(_spawnerManager == null)
        {
            _spawnerManager = GameObject.FindObjectOfType<SpawnerManager>();
        }
    }
    public void MoveDown()
    {
        transform.DOMoveY(upPos.y - _heighDifference, 1);
    }
    public void MoveUp()
    {
        transform.DOMoveY(upPos.y, 2);
    }
}
