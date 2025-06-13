using System;
using Figures;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private Rigidbody2D rigidbody;
    
    private Camera mainCamera;
    private bool wasPressed;
    public bool CanBePressed { get; set; } = true;

    private IFigureSkill skill;

    public IFigureSkill Skill
    {
        get => skill;
        set => skill = value;
    }

    private void Awake()
    {
        mainCamera = Camera.main;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Initialize(FForm form, FColor color, FAnimal animal)
    {
        FForm = GetComponent<FigureForm>();
        
        FForm.Form = form;
        FForm.Color = color;
        FForm.Animal = animal;
        
        FForm.Initilize();
        polygonCollider = gameObject.AddComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        // Проверяем нажатие используя новую систему ввода
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            if (!wasPressed)
            {
                wasPressed = true;
                HandleTouch(Touchscreen.current.primaryTouch.position.ReadValue());
            }
        }
        else if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            if (!wasPressed)
            {
                wasPressed = true;
                HandleTouch(Mouse.current.position.ReadValue());
            }
        }
        else
        {
            wasPressed = false;
        }
    }

    private void HandleTouch(Vector2 screenPosition)
    {
        Vector2 worldPoint = mainCamera.ScreenToWorldPoint(screenPosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        if (CanBePressed && hit.collider && hit.collider.gameObject == gameObject)
        {
            ActionBar.Instance.AddFigure(this);
            Destroy(gameObject);
        }
    }

    public void DisableInteractions()
    {
        polygonCollider.enabled = false;
        rigidbody.bodyType = RigidbodyType2D.Static;
    }
    
    public void SetMass(float mass)
    {
        rigidbody.mass = mass;
    }
}