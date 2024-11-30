using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreaker
{
    class Brick
    {
        public Texture2D texture;
        public Vector2 position;
        public Color tint;
        public Rectangle hibox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            }
        }
        public Brick(Texture2D texture, Vector2 position, Color tint)
        {
            this.texture = texture;
            this.position = position;
            this.tint = tint;
        }



        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, position, tint);

        }






















    }
}
