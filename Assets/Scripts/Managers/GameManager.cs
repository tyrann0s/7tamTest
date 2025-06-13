using System.Collections.Generic;
using Figures;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public int FiguresCount { get; set; }

        private void Awake()
        {
            if (Instance && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void Win()
        {
            UIManager.Instance.ShowWinScreen();
            HideGameField();
        }
        
        public void Lose()
        {
            UIManager.Instance.ShowLoseScreen();
            HideGameField();
        }

        private void HideGameField()
        {
            GameField.Instance.gameObject.SetActive(false);
        }

        public void Reset()
        {
            var figures = FindObjectsByType<Figure>(FindObjectsSortMode.None);
            
            foreach (var figure in figures)
            {
                Destroy(figure.gameObject);
            }
            
            ActionBar.Instance.Clear();
            var amount = figures.Length;
            
            FigureSpawner.Instance.Respawn(amount);
        }
        
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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