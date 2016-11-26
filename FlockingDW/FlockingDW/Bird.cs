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
        Vector2 direction;
        Vector2 origin;
        float speed;
        float rotation;
        float comfortZone;
        float distanceBirds;
        Texture2D sprite;
        Random rand = new Random();
        public bool Alive;
        public bool allowCohesion = false;
        public bool allowSeperation = false;

        public Bird(Texture2D sprite, float x, float y)
        {
            this.sprite = sprite;
            Alive = true;
            position = new Vector2(x, y);

            target(new Vector2(rand.Next(1280), rand.Next(720)));
            speed = 1f;
            origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
            comfortZone = 50f;
            distanceBirds = 25f;
        }

        public void Update(List<Bird> Friends)
        {

            Bounds();

            if (allowSeperation)
            { Seperation(Friends);}
            
            if (allowCohesion)
            {Cohesion(Friends); }
            

            position += direction * speed;

            

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, rotation, origin,1f, SpriteEffects.None,0);
        }

        private void Bounds()
        {
            if (position.X < 0 || position.X > 1280)
            {
                Alive = false;
            }

            if (position.Y < 0 || position.Y > 720)
            {
                Alive = false;
            }
        }

        private void Cohesion(List<Bird> Friends)
        {
            foreach (Bird friend in Friends)
            {
                Vector2 sum = new Vector2(0,0);
                float d = Vector2.Distance(position, friend.position);
                int count = 0;

                if ((d > 0) && (d < comfortZone))
                {
                    sum.X = sum.X + friend.position.X;
                    sum.Y = sum.Y + friend.position.Y;
                    count += 1;
                }

                if (count > 0)
                {
                    target((sum / count));
                }
            }
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
                    sum.Normalize();
                    direction = sum;
                    rotation = (float)(Math.Atan2(direction.Y, direction.X) + Math.PI);
                }

            }
        }

        public void target(Vector2 target)
        {
            direction = target - position;
            rotation = (float)(Math.Atan2(direction.Y, direction.X) + Math.PI);
            direction.Normalize();
        }
    }
}
