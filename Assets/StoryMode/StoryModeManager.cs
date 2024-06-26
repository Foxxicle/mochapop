﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryModeManager : MonoBehaviour
{
    public bool watchCutscene;
    public int Level;
    public string LevelName;
    public float cutsceneLength;
    bool setup;
    [Space(20)]
    [SerializeField]
    int debugSetLevel = -1;

    private void Start()
    {
        if (debugSetLevel != -1)
        {
            SaveData.storyLevel = debugSetLevel;
        }
    }

    public void StartButton()
    {
        DontDestroyOnLoad(gameObject);
        Level = 0;
    }

    private void Update()
    {
        switch (Level)
        {
            case 0:
                LevelName = "Cutscenes";
                break;
            case 1:
                LevelName = "GamerBird";
                break;
            case 2:
                LevelName = "PatientCroc";
                break;
            case 3:
                LevelName = "ClumsyFerret";
                break;
            case 4:
                LevelName = "SlickBrick";
                break;
        }

        if (SceneManager.GetActiveScene().name == "Cutscenes" && watchCutscene == true && setup == false)
        {
            if (SaveData.storyLevel < Level)
            {
                SaveData.storyLevel = Level;
                SaveData.Save("sLevel", SaveData.storyLevel);
            }
            //GameObject.Find("Mocha").GetComponent<Pop>().Costume(SaveData.costume);
            switch (Level)
            {
                default:
                    Debug.Log("Invalid Cutscene!");
                    cutsceneLength = 1.2f;
                    setup = true;
                    break;
                case 0:
                    Debug.Log("Starting Scene");
                    GameObject.Find("Mocha").GetComponent<Animator>().Play("story_intro", 1);
                    GameObject.Find("mochapopTragedy").GetComponent<AudioSource>().Play();
                    cutsceneLength = 22.47f;
                    setup = true;
                    break;
                case 1:
                    Debug.Log("Starting Scene");
                    GameObject.Find("Mocha").GetComponent<Animator>().Play("M_level1", 1);
                    GameObject.Find("Rival").GetComponent<Animator>().Play("R_level1", 1);
                    cutsceneLength = 14f;
                    setup = true;
                    break;
                case 2:
                    Debug.Log("Starting Scene");
                    GameObject.Find("Mocha").GetComponent<Animator>().Play("M_level2", 1);
                    GameObject.Find("Rival").GetComponent<Animator>().Play("R_level2", 1);
                    cutsceneLength = 22.47f;
                    setup = true;
                    break;
            }
        }

        if (cutsceneLength > 0)
        {
            cutsceneLength -= Time.deltaTime;
        }
        if (cutsceneLength <= 0 && setup == true)
        {
            setup = false;
            if (Level == 0)
            {
                Level++;
                Debug.Log(Level);
                if (SaveData.storyLevel < Level)
                {
                    SaveData.storyLevel = Level;
                    SaveData.Save("sLevel", SaveData.storyLevel);
                }
                GameObject.Find("Panel").GetComponent<SceneChange>().Transition("Cutscenes");
                watchCutscene = false;
            }
            else
            {
                if (SceneUtility.GetBuildIndexByScenePath(LevelName) != -1)
                {
                    GameObject.Find("Panel").GetComponent<SceneChange>().Transition(LevelName);
                }
                else
                {
                    Debug.LogError("Level not found!");
                    GameObject.Find("Panel").GetComponent<SceneChange>().Transition("StorySelect");
                }
                watchCutscene = false;
            }
        }
    }


}
