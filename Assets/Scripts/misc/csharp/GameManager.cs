﻿using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerInputService playerInputService;

    public GameObject gamePlaySoldier;
    public ParticleSystem soldierSmoke;
    public SargeManager sarge;

    public bool receiveDamage;
    public bool scores;
    public float time;
    public bool running;

    private void Start()
    {
        TrainingStatistics.ResetStatistics();

        time = 0.0f;

        Transform auxT;
        bool hasCutscene = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            auxT = transform.GetChild(i);

            if (auxT.name == "Cutscene")
            {
                if (auxT.gameObject.activeSelf)
                {
                    hasCutscene = true;
                    break;
                }
            }
        }

        if (!hasCutscene)
        {
            StartGame();
        }
    }

    private void Update()
    {
        if (!playerInputService.IsPausing && running)
        {
            time += Time.deltaTime;
        }
    }

    void CutsceneStart()
    {
        gamePlaySoldier.SetActive(false);
    }

    void StartGame()
    {
        running = true;

        if (gamePlaySoldier != null)
        {
            gamePlaySoldier.SetActive(true);
        }

        if (soldierSmoke != null)
        {
            if (GameQualitySettings.ambientParticles)
            {
                soldierSmoke.Play();
            }
        }

        if (sarge != null && SceneManager.GetActiveScene().name == "demo_forest")
        {
            sarge.ShowInstruction("instructions");
            sarge.ShowInstruction("instructions2");
            sarge.ShowInstruction("instructions3");
            sarge.ShowInstruction("instructions4");
            sarge.ShowInstruction("instructions5");
            sarge.ShowInstruction("additional_instructions");
        }
    }
}