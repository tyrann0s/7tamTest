using System;
using UnityEngine;

namespace Figures.Skills
{
    public class Ice : MonoBehaviour, IFigureSkill
    {
        private Figure figure;
        private int figuresToUnlock = 6;
        private Rigidbody2D rb;

        public void Initialize()
        {
            figure = GetComponent<Figure>();
            rb = GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Static;
            figure.CanBePressed = false;
        }

        public void Use()
        {
            figuresToUnlock--;

            if (figuresToUnlock <= 0)
            {
                if (rb) rb.bodyType = RigidbodyType2D.Dynamic;
                figure.CanBePressed = true;
            }
        }
    }
}
