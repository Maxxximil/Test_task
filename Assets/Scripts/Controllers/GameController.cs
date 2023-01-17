using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Controllers
{

    //Контроллер игры
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;

        public Transform SpawnPoint;
        public Transform FinishPoint;
        public GameObject PlayerPrefab;
        public NavigationBacker navigationBacker;

        public int Difficulty = 4;//Чем больше значение тем меньше зон
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
            _currentTime = TimeToRespawn;//Обнуляем время до респавна
        }

        private void Update()
        {
            if (_isRespawned) return;//Если зареспавнены выходим из апдейта
            if (_currentTime > 0)
            {
                _currentTime -= Time.deltaTime;//Уменбшаем время
            }
            else
            {
                //Спавним игрока и активируем UI

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

        public void SpawnPlayer()//Спавн игрока
        {
            Vector3 pos = SpawnPoint.position;
            pos.y += 1f;
            GameObject player = Instantiate(PlayerPrefab, pos, Quaternion.identity);
            _isRespawned = true;
            navigationBacker.BakeNavMesh();

        }

        
    }
}