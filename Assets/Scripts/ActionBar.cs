using System.Collections.Generic;
using Managers;
using UnityEngine;

public class ActionBar : MonoBehaviour
{
    public static ActionBar Instance { get; private set; }
    public List<Figure> Figures { get; private set; } = new ();
    
    [SerializeField]
    private List<Transform> figurePositions = new ();

    private int currentIndex;

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    public void AddFigure(Figure figure)
    {
        Figures.Add(figure);
        figure.DisableInteractions();
        var figureGo = Instantiate(figure.gameObject, figurePositions[currentIndex]);
        figureGo.transform.position = figurePositions[currentIndex].position;
        figureGo.transform.rotation = Quaternion.identity;
        currentIndex++;
        CheckConditions();
    }

    private void CheckConditions()
    {
        if (Figures.Count >= 3 && AreLastThreeEqual())
        {
            RemoveLastThree();
            if (GameManager.Instance.FiguresCount == 0) GameManager.Instance.Win();
        }
        
        if (Figures.Count >= 7)
        {
            GameManager.Instance.Lose();
        }
    }

    private bool AreLastThreeEqual()
    {
        var last = Figures[^1].FForm;
        var second = Figures[^2].FForm;
        var third = Figures[^3].FForm;

        return last.Form == second.Form && second.Form == third.Form &&
               last.Color == second.Color && second.Color == third.Color &&
               last.Animal == second.Animal && second.Animal == third.Animal;
    }

    private void RemoveLastThree()
    {
        for (int i = 0; i < 3; i++)
        {
            Destroy(figurePositions[currentIndex - 1].GetChild(0).gameObject);
            Figures.RemoveAt(Figures.Count - 1);
            GameManager.Instance.FiguresCount--;
            currentIndex--;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
