﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitialization : MonoBehaviour {

	// Use this for initialization
    IEnumerator Start () {
        yield return new WaitForSeconds(.5f);
        PlayerSingleton.s_player.SceneLoaded();

    }
	}
