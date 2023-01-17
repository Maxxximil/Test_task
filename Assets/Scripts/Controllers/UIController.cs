using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Scripts.Controllers
{
    //���������� ����������
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

        private void Awake()
        {
            Instance = this;
        }


        private void Start()
        {
            //���������� � ����������� ������ � ������
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
                _currentTime -= Time.deltaTime;//��������� ����� ������� ������
            }
            if (_currentTime <= 0)
            {
                //���� ����� ����� ��������� �������
                IsPressed = false;
                ActiveShield(IsPressed);
                UpdateCurrentTime();
            }
          

        }
        //����� ������
        public void WinGame()
        {
            //��������� �����������
            StartCoroutine(NewGame());
        }

        IEnumerator NewGame()
        {
            //���������� ����� ������ � ����� ����� ������������� �����
            yield return new WaitForSeconds(2f);
            WinScreen.SetActive(true);
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(0);
        }

        //������� ������
        public void Pressed(bool isPressed)
        {
            IsPressed = isPressed;
            ActiveShield(IsPressed);   
        }

        //��������� ������� ��
        public void UpdateCurrentTime()
        {
            _currentTime = _pressedTime;
        }

        //��������� ����
        public void ActiveShield(bool isActive)
        {
            Player.player.IsShieldActive = isActive;
            Player.player.ActiveShield(isActive);
        }

        //�����
        public void PauseGame()
        {
            if (Time.timeScale == 1.5f)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1.5f;

            }

        }
        //�����
        public void Exit()
        {
            Application.Quit();
        }
    }
}