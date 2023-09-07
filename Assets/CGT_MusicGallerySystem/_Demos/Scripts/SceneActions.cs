using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CGT.MusicGallery.Demo
{
    public class SceneActions : MonoBehaviour
    {
        public virtual void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public virtual void ExitGame()
        {
            Application.Quit();
        }
    }
}