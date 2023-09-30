using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyGame {
    public class WaveSpawnerLogic : MonoBehaviour {
        [SerializeField] public int MaxGameTime; // in seconds
        [SerializeField] public int MaxEnemyCount = 10;

        public TMPro.TextMeshProUGUI text;

        public GameObject[] targets = { };
        int currEnemyCount = 0;
        public float gameTimeLeft = 0;

        public static System.Action<GameObject> onTargetSpawn;

        private void Start() {
            gameTimeLeft = MaxGameTime;
            StartCoroutine(spawnRoutine());
        }

        private void Update() {
            var scene = SceneManager.GetActiveScene();
            if (gameTimeLeft <= 0 && scene == gameObject.scene) { // exclude the main menu and score display
                SceneManager.LoadScene(4); // score menu return
                return; 
            }
            UpdateTimer();
        }

        private void onTargetDestruction(int _) {
            currEnemyCount--;
        }

        System.Collections.IEnumerator spawnRoutine() {
            while (true) {
                yield return new WaitForSeconds(1f);
                if (currEnemyCount > MaxEnemyCount) { continue; }
                GameObject randomTarget = targets[Random.Range(0, targets.Length)];

                GameObject newTarget = Instantiate(randomTarget, new Vector3(Random.Range(-30f, 30f), 10, Random.Range(-30f, 30f)), Quaternion.identity);
                newTarget.GetComponent<Target>().onDestruction += onTargetDestruction;
                currEnemyCount++;
                onTargetSpawn?.Invoke(newTarget);
            }
        }

        private void UpdateTimer() {
            gameTimeLeft = Mathf.Clamp(gameTimeLeft - Time.deltaTime, 0, MaxGameTime);
            text.text = System.TimeSpan.FromSeconds(gameTimeLeft).ToString(@"mm\:ss\:fff");
        }

    }
}