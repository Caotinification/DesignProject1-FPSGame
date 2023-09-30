using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyGame {
    public class MainMenuHandler : MonoBehaviour {
        private void Start() {
            Cursor.lockState = CursorLockMode.None;
        }
        public static void PlayGame(int stageNum) {
            SceneManager.LoadSceneAsync(stageNum);
        }
    }
}