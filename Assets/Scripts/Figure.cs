using System;
using Managers;
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
    public FigureForm FForm { get; set; }
    private PolygonCollider2D polygonCollider;

    public void Initialize(FForm form, FColor color, FAnimal animal)
    {
        FForm = GetComponent<FigureForm>();
        
        FForm.Form = form;
        FForm.Color = color;
        FForm.Animal = animal;
        
        FForm.Initilize();
        polygonCollider = gameObject.AddComponent<PolygonCollider2D>();
    }

    private void OnMouseDown()
    {
        ActionBar.Instance.AddFigure(this);
        Destroy(gameObject);
    }

    public void DisableInteractions()
    {
        polygonCollider.enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
}
