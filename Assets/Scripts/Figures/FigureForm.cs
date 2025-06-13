using System;
using UnityEngine;

public class FigureForm : MonoBehaviour
{
    public FForm Form { get; set; }
    public FColor Color { get; set; }
    public FAnimal Animal { get; set; }
    
    [SerializeField]
    private SpriteRenderer animalSpriteRenderer;
    private SpriteRenderer spriteRenderer;
    
    [Header("Form Sprites")]
    [SerializeField] private Sprite circle;
    [SerializeField] private Sprite square;
    [SerializeField] private Sprite triangle;
    [SerializeField] private Sprite star;

    [Header("Animal Sprites")]
    [SerializeField] private Sprite bear;
    [SerializeField] private Sprite bunny;
    [SerializeField] private Sprite cat;
    [SerializeField] private Sprite deer;
    [SerializeField] private Sprite fox;
    [SerializeField] private Sprite penguin;
    [SerializeField] private Sprite pig;
    [SerializeField] private Sprite sheep;
    [SerializeField] private Sprite toad;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initilize()
    {
        switch (Form)
        {
            case FForm.Circle:
                spriteRenderer.sprite = circle;
                break;
            case FForm.Square:
                spriteRenderer.sprite = square;
                break;
            case FForm.Triangle:
                spriteRenderer.sprite = triangle;
                break;
            case FForm.Star:
                spriteRenderer.sprite = star;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        switch (Color)
        {
            case FColor.Green:
                spriteRenderer.color = UnityEngine.Color.green;
                break;
            case FColor.Red:
                spriteRenderer.color = UnityEngine.Color.red;
                break;
            case FColor.Blue:
                spriteRenderer.color = UnityEngine.Color.blue;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        switch (Animal)
        {
            case FAnimal.Bear:
                animalSpriteRenderer.sprite = bear;
                break;
            case FAnimal.Bunny:
                animalSpriteRenderer.sprite = bunny;
                break;
            case FAnimal.Cat:
                animalSpriteRenderer.sprite = cat;
                break;
            case FAnimal.Deer:
                animalSpriteRenderer.sprite = deer;
                break;
            case FAnimal.Fox:
                animalSpriteRenderer.sprite = fox;
                break;
            case FAnimal.Penguin:
                animalSpriteRenderer.sprite = penguin;
                break;
            case FAnimal.Pig:
                animalSpriteRenderer.sprite = pig;
                break;
            case FAnimal.Sheep:
                animalSpriteRenderer.sprite = sheep;
                break;
            case FAnimal.Toad:
                animalSpriteRenderer.sprite = toad;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
