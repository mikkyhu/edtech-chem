﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Loader : MonoBehaviour
{

    private string loadLevel = "Battle";

    void Start()
    {

        StartCoroutine(loadSceneAfterDelay(33));

    }

    IEnumerator loadSceneAfterDelay(float waitbySecs)
    {

        yield return new WaitForSeconds(waitbySecs);
        SceneManager.LoadScene(loadLevel);
    }

}
		




