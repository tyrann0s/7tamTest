using System;
using System.Collections;
using System.Linq;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

public class FigureSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject figurePrefab;
    
    [SerializeField]
    private int amount;
    
    [SerializeField]
    private float positionOffset, spawnDelay;

    private (FForm form, FColor color, FAnimal animal)[] figureAttributes;

    private void Start()
    {
        figureAttributes = CreateBalancedAttributes();
        GameManager.Instance.FiguresCount = amount;
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        for (int i = 0; i < amount; i++)
        {
            var figureGo = Instantiate(figurePrefab);
            var figure = figureGo.GetComponent<Figure>();
            figure.Initialize(
                figureAttributes[i].form,
                figureAttributes[i].color,
                figureAttributes[i].animal
            );
            figure.transform.position = GetRandomPosition();
            yield return new WaitForSeconds(spawnDelay);
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

    private (FForm, FColor, FAnimal)[] CreateBalancedAttributes()
    {
        var adjustedAmount = (amount / 3) * 3;
        var result = new (FForm, FColor, FAnimal)[adjustedAmount];
        var forms = Enum.GetValues(typeof(FForm));
        var colors = Enum.GetValues(typeof(FColor));
        var animals = Enum.GetValues(typeof(FAnimal));

        for (int i = 0; i < adjustedAmount; i += 3)
        {
            var form = (FForm)forms.GetValue(Random.Range(0, forms.Length));
            var color = (FColor)colors.GetValue(Random.Range(0, colors.Length));
            var animal = (FAnimal)animals.GetValue(Random.Range(0, animals.Length));

            result[i] = (form, color, animal);
            result[i + 1] = (form, color, animal);
            result[i + 2] = (form, color, animal);
        }

        for (int i = result.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (result[i], result[randomIndex]) = (result[randomIndex], result[i]);
        }
        
        return result;
    }
}
