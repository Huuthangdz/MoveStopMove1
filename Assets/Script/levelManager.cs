﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class levelManager : Singleton<levelManager>
{
    public Player player;
    public NavMeshData navMeshData;
    public int botAmount;
    private List<enemi> enemis = new List<enemi>();
    [SerializeField] private GameObject botPrefabs;

    private int aliveBot;
    private int Score;
    private int totalCharacterAlive;
    public int totalCharacter => botAmount + 1;
    [SerializeField] GameObject indicatorPrefabs;
    [SerializeField] GameObject canvasIndicator;


    private static string[] randomName = { "A1", "A2", "A3", "A4", "A5", "A6", "A7", "A8", "A9", "A10", "A11", "A12" };
    // Start is called before the first frame update

    void Start()
    {
        totalCharacterAlive = totalCharacter;
        aliveBot = botAmount;
        Score = 0;  
    }

    private void Update()
    {
        InitBotAgain();
    }
    public void AliveBot()
    {
        aliveBot--;
    }
    public void ScorePlayer()
    {
        Score++;
    }
    private void InitBotAgain()
    {
        if (aliveBot < botAmount)
        {
            NewBot();
            aliveBot++;
        }
    }
    public TargetIndicator CreateIndicatorPanel(Transform target)
    {
        TargetIndicator targetIndicator = Instantiate(indicatorPrefabs, target.transform.position, Quaternion.identity).GetComponent<TargetIndicator>();
        targetIndicator.OnInit(target.transform);
        targetIndicator.transform.SetParent(canvasIndicator.transform);
        targetIndicator.SetInformation(randomName[Random.Range(0, 12)]);
        return targetIndicator;
    }
    public void OnInit()
    {
        for (int i = 0; i < botAmount; i++)
        {
            NewBot();
        }
    }
    
    public void NewBot()
    {
        enemi bot = Instantiate(botPrefabs, GetRandomPointNavmesh(), Quaternion.identity).GetComponent<enemi>();
        bot.ChangeState(new Patrol());
    }
    public void InitCharacterAlive()
    {
        totalCharacterAlive--;
    }
    public Vector3 GetRandomPointNavmesh() 
    {
        Vector3 randomPoint = new Vector3(Random.Range(-15f,15f), 0f, Random.Range(-10f,10f));
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint,out hit,10f, 1))    
        {
            return hit.position;        
        }
        return randomPoint;
    }
}
