using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Scripts.Controllers
{
    //Контроллер интерфейса
    public class UIController : MonoBehaviour
    {
        public static UIController Instance;

        public GameObject Shield;
        public GameObject StartScreen;
        public GameObject WinScreen;
        public Button PauseButton;

        public bool IsPressed;
        private float _pressedTime = 2f;
        private float _currentTime;
        private bool _ispaused = false;

        private void Awake()
        {
            Instance = this;
        }


        private void Start()
        {
            //Активируем и деативируем кнопки в старте
            Shield.SetActive(false);
            StartScreen.SetActive(true);
            PauseButton.interactable = false;
            IsPressed = false;
            _currentTime = _pressedTime;
        }

        private void Update()
        {
            if (IsPressed && _currentTime > 0)
            {  
                _currentTime -= Time.deltaTime;//Обновляем время нажатой кнопки
            }
            if (_currentTime <= 0)
            {
                //Если время вышло отключаем нажатие
                IsPressed = false;
                ActiveShield(IsPressed);
                UpdateCurrentTime();
            }
          

        }
        //Метод победы
        public void WinGame()
        {
            //запускаем сопрограмму
            StartCoroutine(NewGame());
        }

        IEnumerator NewGame()
        {
            //Активируем эеран победы и через время перезагружаем сцену
            WinScreen.SetActive(true);
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(0);
        }

        //Нажатие кнопки
        public void Pressed(bool isPressed)
        {
            IsPressed = isPressed;
            ActiveShield(IsPressed);   
        }

        //Обнуление времени КД
        public void UpdateCurrentTime()
        {
            _currentTime = _pressedTime;
        }

        //Активация щита
        public void ActiveShield(bool isActive)
        {
            Debug.Log(isActive);
            Player.player.IsShieldActive = isActive;
            Player.player.ActiveShield(isActive);
        }

        //Пауза
        public void PauseGame()
        {
            if (!_ispaused)
            {
                _ispaused = true;
                Player.player.Agent.isStopped = true;
                

            }
            else
            {
                _ispaused = false;  
                Player.player.Agent.isStopped = false;
            }

        }
        //Выход
        public void Exit()
        {
            Application.Quit();
        }
    }
}