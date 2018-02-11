using System;
using UnityEngine;

namespace Bomberman
{
    public class PowerUp : MonoBehaviour
    {
        public Sprite ExtraBombSprite;

        public Sprite ExplosionExpanderSprite;

        public Sprite SkullSprite;

        public int Kind;

        public void Start()
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();

            switch (Kind)
            {
                case Constants.CellKinds.WallExtraBomb:
                    spriteRenderer.sprite = ExtraBombSprite;
                    break;
                case Constants.CellKinds.WallExpander:
                    spriteRenderer.sprite = ExplosionExpanderSprite;
                    break;
                case Constants.CellKinds.WallSkull:
                    spriteRenderer.sprite = SkullSprite;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
