using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlockingDW
{
    public class Magnet
    {
        public bool magnatize = false;
        Vector2 position;
        Texture2D sprite;
        float magnetRange;

        public Magnet(ContentManager content, int x, int y)
        {
            sprite = content.Load<Texture2D>("StaticBlock");
            position = new Vector2(x, y);
            magnetRange = 250f;
        }

        public void Update(List<Bird> birds)
        {
            if (magnatize)
            {
                foreach (Bird bird in birds)
                {
                    float dist = Vector2.Distance(position, bird.position);
                    if ((dist > 0) && (dist < magnetRange))
                    {
                        bird.Target(position);
                    }
                }
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, Color.White);
        }

    }
}
