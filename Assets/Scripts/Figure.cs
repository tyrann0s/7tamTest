using System;
using UnityEngine;
using Random = UnityEngine.Random;

public enum FForm
{
    Circle,
    Square,
    Triangle,
    Star
}

public enum FColor
{
    Green,
    Red,
    Blue
}

public enum FAnimal
{
    Bear,
    Bunny,
    Cat,
    Deer,
    Fox,
    Penguin,
    Pig,
    Sheep,
    Toad
}

public class Figure : MonoBehaviour
{
    private FigureForm figureForm;
    private PolygonCollider2D polygonCollider;

    public void Initialize(FForm form, FColor color, FAnimal animal)
    {
        figureForm = GetComponent<FigureForm>();
        
        figureForm.Form = form;
        figureForm.Color = color;
        figureForm.Animal = animal;
        
        figureForm.Initilize();
        polygonCollider = gameObject.AddComponent<PolygonCollider2D>();
    }
}
