using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public Transform SpawnPoint;
    public Transform FinishPoint;
    public GameObject PlayerPrefab;

    public float TimeToRespawn = 2f;
    private float _currentTime;
    private bool _isRespawned = false;

    private void Start()
    {
        Instance = this;
        _currentTime = TimeToRespawn;
    }

    private void Update()
    {
        if (_isRespawned) return;
        if (_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;
        }
        else
        {
            SpawnPlayer();
        }
    }

    public void SpawnPlayer()
    {
        Vector3 pos = SpawnPoint.position;
        GameObject player = Instantiate(PlayerPrefab, pos, Quaternion.identity);
        _isRespawned = true;
    }
}
