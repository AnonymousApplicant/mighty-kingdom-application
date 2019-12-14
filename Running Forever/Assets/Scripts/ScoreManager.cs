using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // variable that holds the instance for the singleton setup

    public float currentScore;

    public float coinScore;

    private float timer;
    [SerializeField]
    private float timeMultiplier;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        currentScore = (timer * timeMultiplier) + coinScore;
    }
}
