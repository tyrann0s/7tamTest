using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ResultScreen : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI resultText;
        [SerializeField] private Button restartButton;
    
        private void Start()
        {
            restartButton.onClick.AddListener(OnRestartClick);
        }

        public void SetResult(string result)
        {
            resultText.text = result;
        }
    
        private void OnRestartClick()
        {
            GameManager.Instance.RestartGame();
        }

    }
}
