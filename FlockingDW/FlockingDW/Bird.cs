using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlockingDW
{
    public class Bird
    {
        public Vector2 position;
        Vector2 velocity;
        Vector2 origin;
        Vector2 steer;

        float rotation;
        float speedLimit;
        Texture2D sprite;
        Random rand = new Random();
        public bool Alive;

        int totalTime = 0;
        int elapsedTime = 5000;

        public Bird(Texture2D sprite, float x, float y)
        {
            this.sprite = sprite;
            Alive = true;
            position = new Vector2(x, y);
            steer = new Vector2();
            velocity = new Vector2((float)rand.NextDouble()*40, (float)rand.NextDouble() * 40);
            origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
            speedLimit = 50f;
        }

        public void Update(GameTime gameTime, List<Bird> Friends)
        {

            steer = randomSteer(gameTime, steer);

            velocity.X += steer.X * gameTime.ElapsedGameTime.Milliseconds / 1000;
            velocity.Y += steer.Y * gameTime.ElapsedGameTime.Milliseconds / 1000;
            velocity = limiter(velocity, speedLimit);

            position.X += velocity.X * gameTime.ElapsedGameTime.Milliseconds / 1000;
            position.Y += velocity.Y * gameTime.ElapsedGameTime.Milliseconds / 1000;

            rotation = (float)(Math.Atan2(velocity.Y, velocity.X) + Math.PI);

            position.X = (position.X + 1280) % 1280;
            position.Y = (position.Y + 720) % 720;

            Debug.WriteLine("Steer: " + steer + "Velocity: " + velocity);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, rotation, origin,1f, SpriteEffects.None,0);
        }


        private Vector2 limiter(Vector2 vector, float speedLimit)
        {
            float length = (float)(Math.Sqrt((vector.X * vector.X) + (vector.Y * vector.Y)));
            if(length>speedLimit)
            {

                Vector2 newVelocity = (speedLimit / length) * vector;
                float newLength = (float)(Math.Sqrt((newVelocity.X * newVelocity.X) + (newVelocity.Y * newVelocity.Y)));
                return newVelocity;
            }
            else
            {
                return vector;
            }
        }

        private Vector2 randomSteer(GameTime gameTime, Vector2 vector)
        {
                if (elapsedTime >= totalTime)
                {
                elapsedTime = 0;
                    return new Vector2((float)rand.NextDouble() * 30, (float)rand.NextDouble() * 30);
                }
                else
                {
                elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
                    return vector;
                }

        }

       
    }
}
