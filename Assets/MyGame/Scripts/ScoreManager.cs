using UnityEngine;

namespace MyGame {
    public class ScoreManager : MonoBehaviour {
        [SerializeField] TMPro.TextMeshProUGUI text;
        
        [SerializeField] public WaveSpawnerLogic waveObj;

        public static System.Action<int> scoreGetEvent;

        public static int playerScore = 0;

        public void UpdateScore(int score) {
            playerScore += score;
            text.text = $"Score: {playerScore}";
        }

        public void targetSpawnHandler(GameObject target) {
            Target targetObj = target.GetComponent<Target>();
            targetObj.onDestruction += UpdateScore;
        }

        void Start() {
            playerScore = 0;
            WaveSpawnerLogic.onTargetSpawn += targetSpawnHandler;
        }

        private void OnDestroy() {
            WaveSpawnerLogic.onTargetSpawn -= targetSpawnHandler;
        }

        private void OnDisable() {
            PlayerPrefs.SetInt("Score", playerScore);
        }

    }
}