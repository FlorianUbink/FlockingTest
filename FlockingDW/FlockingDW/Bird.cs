using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlockingDW
{
    public class Bird
    {
        public Vector2 position;
        Vector2 renderPosition;
        Vector2 direction;
        Vector2 origin;
        Vector2 target;
        float speed;
        float rotation;
        float comfortZone;
        float distanceBirds;
        Texture2D sprite;
        Random rand = new Random();

        bool allowMovement = false;

        public Bird(Texture2D sprite, float x, float y)
        {
            this.sprite = sprite;
            
            position = new Vector2(x, y);
            renderPosition = new Vector2();
            Target(new Vector2(rand.Next(1280), rand.Next(720)));
            speed = 1f;
            origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
            comfortZone = 50f;
            distanceBirds = 25f;
        }

        public void Update(List<Bird> Friends)
        {

            //Bounds();


            Seperation(Friends);

            if (allowMovement)
            {
                position += direction * speed;
            }

            position.X = (position.X + 1280) % 1280;
            position.Y = (position.Y + 720) % 720;


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            renderPosition.X = position.X - sprite.Width/2;
            renderPosition.Y = position.Y - sprite.Height / 2;
            spriteBatch.Draw(sprite, renderPosition, null, Color.White, rotation, origin,1f, SpriteEffects.None,0);
        }


        private void Seperation(List<Bird> Friends)
        {

            foreach (Bird friend in Friends)
            {
                Vector2 sum = new Vector2(0, 0);
                Vector2 diff = new Vector2();
                float d = Vector2.Distance(position, friend.position);
                int count = 0;

                if ((d > 0) && (d < distanceBirds))
                {
                    diff = Vector2.Subtract(position, friend.position);
                    diff.Normalize();
                    diff = diff / d;

                    sum = sum + diff;
                    count += 1;
                }

                if (count > 0)
                {
                    sum = sum / count;
                    direction = sum;
                    rotation = (float)(Math.Atan2(direction.Y, direction.X) + Math.PI);
                    direction.Normalize();
                }

            }
        }

        public void Target(Vector2 target)
        {
            direction = target - position;
            rotation = (float)(Math.Atan2(direction.Y, direction.X) + Math.PI);
            direction.Normalize();
            allowMovement = true;
        }
    }
}
