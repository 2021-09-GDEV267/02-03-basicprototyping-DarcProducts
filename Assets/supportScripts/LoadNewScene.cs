﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewScene : MonoBehaviour
{
    public void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);

    public void LoadScene(int sceneIndex) => SceneManager.LoadScene(sceneIndex);
}