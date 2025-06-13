using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Figures.Skills;
using Managers;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Figures
{
    public class FigureSpawner : MonoBehaviour
    {
        public static FigureSpawner Instance { get; private set; }
    
        [SerializeField]
        private GameObject figurePrefab;
    
        [SerializeField]
        private int amount;
    
        [SerializeField]
        private float positionOffset, spawnDelay;

        private (FForm form, FColor color, FAnimal animal)[] figureAttributes;
        public List<Figure> SpawnedFigures { get; private set; } = new (); 

        private void Awake()
        {
            if (Instance && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            figureAttributes = CreateBalancedAttributes(amount);
            GameManager.Instance.FiguresCount = amount;
            StartCoroutine(Spawn());
        }

        public void Respawn(int figuresAmount)
        {
            SpawnedFigures.Clear();
            amount = figuresAmount;
            figureAttributes = CreateBalancedAttributes(amount);
            StartCoroutine(Spawn());
        }
        
        private IEnumerator Spawn()
        {
            for (int i = 0; i < amount; i++)
            {
                var figureGo = Instantiate(figurePrefab, GameField.Instance.transform);
                var figure = figureGo.GetComponent<Figure>();
                SpawnedFigures.Add(figure);
                figure.Initialize(
                    figureAttributes[i].form,
                    figureAttributes[i].color,
                    figureAttributes[i].animal
                );
            
                switch (figure.FForm.Animal)
                {
                    case FAnimal.Bear:
                        figure.SetMass(25);
                        break;
                    case FAnimal.Bunny:
                        break;
                    case FAnimal.Cat:
                        break;
                    case FAnimal.Deer:
                        break;
                    case FAnimal.Fox:
                        break;
                    case FAnimal.Penguin:
                         var iceSkill = figure.AddComponent<Ice>();
                         figure.Skill = iceSkill;
                        break;
                    case FAnimal.Pig:
                        var bombSkill = figure.AddComponent<Bomb>();
                        figure.Skill = bombSkill;
                        break;
                    case FAnimal.Sheep:
                        break;
                    case FAnimal.Toad:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            
                figure.transform.position = GetRandomPosition();
                yield return new WaitForSeconds(spawnDelay);
            }
            
            yield return new WaitForSeconds(2f);
            PrepareSkills();
        }

        private void PrepareSkills()
        {
            foreach (var figure in SpawnedFigures)
            {
                figure.Skill?.Initialize();
            }
        }
    
        private const int MAX_POSITION_RETRIES = 10;
        private bool IsPositionValid(Vector3 position)
        {
            var colliders = Physics2D.OverlapCircleAll(position, 0.5f);
            return !colliders.Any(col => col.GetComponent<Figure>() != null);
        }

        private Vector3 GetRandomPosition()
        {
            for (int attempt = 0; attempt < MAX_POSITION_RETRIES; attempt++)
            {
                var position = new Vector3(transform.position.x + Random.Range(-positionOffset, positionOffset), transform.position.y, 0);
                if (IsPositionValid(position))
                {
                    return position;
                }
            }

            return new Vector3(transform.position.x + Random.Range(-positionOffset, positionOffset), transform.position.y, 0);
        }

        private (FForm, FColor, FAnimal)[] CreateBalancedAttributes(int figuresAmount)
        {
            var adjustedAmount = (figuresAmount / 3) * 3;
            var result = new (FForm, FColor, FAnimal)[adjustedAmount];
            var forms = Enum.GetValues(typeof(FForm)).Cast<FForm>().ToList();
            var colors = Enum.GetValues(typeof(FColor)).Cast<FColor>().ToList();
            var animals = Enum.GetValues(typeof(FAnimal)).Cast<FAnimal>().ToList();

            // Хранит уже использованные комбинации
            var usedCombinations = new HashSet<(FForm, FColor, FAnimal)>();

            for (int i = 0; i < adjustedAmount; i += 3)
            {
                (FForm form, FColor color, FAnimal animal) combination;

                do
                {
                    var form = forms[Random.Range(0, forms.Count)];
                    var color = colors[Random.Range(0, colors.Count)];
                    var animal = animals[Random.Range(0, animals.Count)];
                    combination = (form, color, animal);
                } while (usedCombinations.Contains(combination) &&
                         usedCombinations.Count < forms.Count * colors.Count * animals.Count);

                // Если все возможные комбинации исчерпаны, очищаем список использованных
                if (usedCombinations.Count >= forms.Count * colors.Count * animals.Count)
                {
                    usedCombinations.Clear();
                }

                usedCombinations.Add(combination);

                // Создаем три одинаковые фигуры с этой комбинацией
                result[i] = combination;
                result[i + 1] = combination;
                result[i + 2] = combination;
            }

            // Перемешиваем массив
            for (int i = result.Length - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                (result[i], result[randomIndex]) = (result[randomIndex], result[i]);
            }

            return result;
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