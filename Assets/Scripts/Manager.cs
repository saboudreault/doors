﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private bool nextLevel;
    private GameObject pawn;
    private PlayerData playerScript;
    private GazeCheck gazeScript;
    private PositionCheck positionScript;

    [HideInInspector]
    public int beat;

    [HideInInspector]
    public float timeMoving;

    [HideInInspector]
    public int roomNumber;

    [Space]

    [Header("Beat 1")]
    public Image fadeInOverlay;
    public GameObject wallStart;
    public GameObject triggerStart;
    public GameObject doorCover;
    public GameObject doorStart;

    [Space]

    [Header("Beat 2")]
    public GameObject tower;
    public GameObject doorDeadEnd;
    public GameObject wallDeadEnd;
    public GameObject triggerDoor;
    public GameObject triggerDeadEnd;
    public GameObject triggerColor;

    private PositionCheck deadEndScript;
    private bool inDeadEnd;

    void Start()
    {
        pawn = GameObject.Find("Player");
        playerScript = pawn.GetComponent<PlayerData>();
        gazeScript = wallStart.GetComponent<GazeCheck>();
        positionScript = wallStart.GetComponent<PositionCheck>();
        deadEndScript = triggerDeadEnd.GetComponent<PositionCheck>();
    }

    void Update()
    {
        BeatMachine();
        IncrementBeat();

        if (Input.GetKeyDown(KeyCode.F12))
        {
            ReloadScene.s_instance.sceneAllowed = true;
        }
    }

    void BeatMachine()
    {
        switch (beat)
        {
            case 0:
                Beat0();
                break;
            case 1:
                Beat1();
                break;
            case 2:
                Beat2();
                break;
            case 3:
                Beat3();
                break;
        }
    }

    void IncrementBeat()
    {
        if (nextLevel)
        {
            beat++;
            nextLevel = false;
        }

    }

    // Intro
    void Beat0()
    {
        fadeInOverlay.CrossFadeAlpha(0, 2.0f, false);

        timeMoving = playerScript.timeMoving;

        if (timeMoving > 5)
        {
            nextLevel = true;
        }
    }

    // Line
    void Beat1()
    {
        gazeScript = wallStart.GetComponent<GazeCheck>();
        positionScript = triggerStart.GetComponent<PositionCheck>();

        RevertLayoutSwitch();

        if (gazeScript.isVisible && positionScript.isInside)
        {
            doorStart.SetActive(true);
            doorCover.SetActive(false);
            nextLevel = true;
        }
    }

    // Backtrack
    void Beat2()
    {
        gazeScript = doorDeadEnd.GetComponent<GazeCheck>();
        positionScript = triggerDoor.GetComponent<PositionCheck>();

        if (gazeScript.isVisible && positionScript.isInside)
        {
            LayoutSwitch();
            nextLevel = true;
        }
    }

    // Wall Follow
    void Beat3()
    {
        gazeScript = tower.GetComponent<GazeCheck>();

        if (gazeScript.isVisible)
        {
            if (wallDeadEnd.transform.localPosition.z - pawn.transform.localPosition.z >= 2)
            {
                wallDeadEnd.transform.position = new Vector3(wallDeadEnd.transform.localPosition.x, wallDeadEnd.transform.localPosition.y, pawn.transform.localPosition.z + 2);
            }
        }

    }

    void LayoutSwitch()
    {
        triggerColor.SetActive(true);

        GameObject[] deletedGOs;
        deletedGOs = GameObject.FindGameObjectsWithTag("LayoutSwitch");

        foreach (GameObject deletedGO in deletedGOs)
        {
            deletedGO.SetActive(false);
        }


    }

    void RevertLayoutSwitch()
    {
        triggerColor.SetActive(false);

        GameObject[] deletedGOs;
        deletedGOs = GameObject.FindGameObjectsWithTag("LayoutSwitch");

        foreach (GameObject deletedGO in deletedGOs)
        {
            deletedGO.SetActive(true);
        }
    }

}