using UI;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [SerializeField] private GameObject resultScreen;
        
        private void Awake()
        {
            if (Instance && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void ShowWinScreen()
        {
            var screen = Instantiate(resultScreen);
            screen.GetComponent<ResultScreen>().SetResult("You win :D");
        }
        
        public void ShowLoseScreen()
        {
            var screen = Instantiate(resultScreen);
            screen.GetComponent<ResultScreen>().SetResult("You lose :(");
        }
        
        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }
}
