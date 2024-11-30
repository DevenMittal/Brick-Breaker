using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreaker
{
    class Paddle
    {
        public Vector2 speed;
        public Texture2D texture;
        public Vector2 position;
        public Color tint;


        public Rectangle hitbox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            }
        }
        
        public Paddle(Texture2D texture, Vector2 position, Color tint, Vector2 speed)
        {
            this.texture = texture;
            this.position = position;
            this.tint = tint;
            this.speed = speed;
        }

        





































    }
}
