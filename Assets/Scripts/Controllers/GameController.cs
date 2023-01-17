using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Controllers
{

    //���������� ����
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;

        public Transform SpawnPoint;
        public Transform FinishPoint;
        public GameObject PlayerPrefab;
        public NavigationBacker navigationBacker;

        public int Difficulty = 4;//��� ������ �������� ��� ������ ���
        public float TimeToRespawn = 2f;
        private float _currentTime;
        private bool _isRespawned = false;

        private void Awake()
        {
            Instance = this;

            GlobalEventManager.onSpawnPlayer.AddListener(SpawnPlayer);
            GlobalEventManager.onShowUI.AddListener(ShowUI);
        }
        private void Start()
        {
            _currentTime = TimeToRespawn;//�������� ����� �� ��������
        }

        private void Update()
        {
            if (_isRespawned) return;//���� ������������ ������� �� �������
            if (_currentTime > 0)
            {
                _currentTime -= Time.deltaTime;//��������� �����
            }
            else
            {
                //������� ������ � ���������� UI

                GlobalEventManager.SendSpawnPlayer();
                GlobalEventManager.SendShowUI();
                //SpawnPlayer();
                //UIController.Instance.Shield.SetActive(true);
                //UIController.Instance.PauseButton.interactable = true;
                //UIController.Instance.StartScreen.SetActive(false);


            }
        }

        private static void ShowUI()
        {
            UIController.Instance.Shield.SetActive(true);
            UIController.Instance.PauseButton.interactable = true;
            UIController.Instance.StartScreen.SetActive(false);
        }

        public void SpawnPlayer()//����� ������
        {
            Vector3 pos = SpawnPoint.position;
            pos.y += 1f;
            GameObject player = Instantiate(PlayerPrefab, pos, Quaternion.identity);
            _isRespawned = true;
            navigationBacker.BakeNavMesh();

        }

        
    }
}