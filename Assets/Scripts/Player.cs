using Scripts.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public static Player player;

    public Material activeShield;
    public Material nonActiveShield;
    public Object Explosion;
    public Object Win;
    
    public NavMeshAgent Agent;

    public bool IsShieldActive = false;
    private bool _isWin = false;

    private void Awake()
    {
        player = this;    
    }

    private void Start()
    {
       Agent.SetDestination(GameController.Instance.FinishPoint.position);//Ищем путь
    }


    //Активируем цвет героя во время действия щита
    public void ActiveShield(bool isActive)
    {
        if (isActive)
        {
            gameObject.GetComponent<MeshRenderer>().material = activeShield;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = nonActiveShield;

        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Death") && !IsShieldActive) //При попадании в смертельную зону перезапуск
        {
            GameOver();
        }
        if (other.CompareTag("Finish")&&!_isWin)//При попадании в победную зону новая игра
        {
            _isWin = true;
            GameWin();
        }
    }

    //Активация победных частиц
    private void GameWin()
    {
        Agent.isStopped = true;
        GameObject go = (GameObject)Instantiate(Win);
        go.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        UIController.Instance.WinGame();
    }

    //Активация проигрыша
    public void GameOver()
    {
        Agent.isStopped = true;
        GameObject go = (GameObject)Instantiate(Explosion);
        go.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Destroy(gameObject);
        GameController.Instance.SpawnPlayer();
    }

    
}
