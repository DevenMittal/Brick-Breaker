using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreaker
{
    class Ball
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
        public Ball(Texture2D texture, Vector2 position, Color tint, Vector2 speed)
        {
            this.texture = texture;
            this.position = position;
            this.tint = tint;
            this.speed = speed;
        }

        public void Bounce(GraphicsDevice gd)
        {
            position +=speed;

            if(position.X + texture.Width > gd.Viewport.Width)
            {
                speed.X *= -1;
            }
            
            if(position.Y<0)
            {
                speed.Y *= -1;

            }
            if(position.X< 0)
            {
                speed.X *= -1;

            }
        }


























    }
}
