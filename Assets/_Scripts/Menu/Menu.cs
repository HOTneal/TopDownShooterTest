﻿using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class Menu : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadSceneAsync(1);
        }
    
        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
