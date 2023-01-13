using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Controllers
{
    public class UIController : MonoBehaviour
    {
        public static UIController Instance;

        public GameObject Shield;

        public bool IsPressed;
        private float _pressedTime = 2f;
        private float _currentTime;

        private void Awake()
        {
            Instance = this;
        }


        private void Start()
        {
            Shield.SetActive(false);
            IsPressed = false;
            _currentTime = _pressedTime;
        }

        private void Update()
        {
            if (IsPressed && _currentTime > 0)
            {  
                _currentTime -= Time.deltaTime;
            }
            if (_currentTime <= 0)
            {
                IsPressed = false;
                ActiveShield(IsPressed);
                UpdateCurrentTime();
            }
          

        }

        public void Pressed(bool isPressed)
        {
            IsPressed = isPressed;
            ActiveShield(IsPressed);   
        }

        public void UpdateCurrentTime()
        {
            _currentTime = _pressedTime;
        }

        public void ActiveShield(bool isActive)
        {
            Debug.Log(isActive);
            Player.player.IsShieldActive = isActive;
            Player.player.ActiveShield(isActive);
        }
    }
}