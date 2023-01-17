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
    public GameObject Explosion;
    public GameObject Win;
    
    public NavMeshAgent Agent;

    public bool IsShieldActive = false;
    private bool _isWin = false;

    private void Awake()
    {
        player = this;
        GlobalEventManager.onGameEnd.AddListener(EndGame);
    }

    private void Start()
    {
       Agent.SetDestination(GameController.Instance.FinishPoint.position);//���� ����
    }


    //���������� ���� ����� �� ����� �������� ����
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

        if (other.CompareTag("Death") && !IsShieldActive) //��� ��������� � ����������� ���� ����������
        {
            GlobalEventManager.SendGameEnd(Explosion, "Death");
        }
        if (other.CompareTag("Finish")&&!_isWin)//��� ��������� � �������� ���� ����� ����
        {
            _isWin = true;
            GlobalEventManager.SendGameEnd(Win, "Win");
        }
    }

    ////��������� �������� ������
    //private void GameWin()
    //{
    //    Agent.isStopped = true;
    //    GameObject go = (GameObject)Instantiate(typeOfGameEnding);
    //    go.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    //    UIController.Instance.WinGame();
    //}

    private void EndGame(GameObject typeOfGameEnding, string nameOfGameEnding)
    {
        Agent.isStopped = true;
        GameObject go = (GameObject)Instantiate(typeOfGameEnding);
        go.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (nameOfGameEnding == "Win") UIController.Instance.WinGame();
        if (nameOfGameEnding == "Death")
        {
            Destroy(gameObject);
            GlobalEventManager.SendSpawnPlayer();
        }
    }

    //��������� ���������
    //public void GameOver()
    //{
    //    Agent.isStopped = true;
    //    GameObject go = (GameObject)Instantiate(Explosion);
    //    go.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    //    Destroy(gameObject);
    //    GlobalEventManager.SendSpawnPlayer();
    //}

    
}
