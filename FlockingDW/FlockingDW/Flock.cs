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
    public class Flock
    {
        public List<Bird> myBirds;
        Random rand;
        Texture2D birdSprite;
        int currentMiliseconds;
        int maxMiliseconds;
        int maxBirds;
        bool allowC = false;
        bool allowS = false;


        public Flock(ContentManager content)
        {
            birdSprite = content.Load<Texture2D>("New Piskel");
            myBirds = new List<Bird>();
            rand = new Random();
            currentMiliseconds = 0;
            maxMiliseconds = 100;
            maxBirds = 50;
        }

        public void Update(GameTime gameTime)
        {
            if (currentMiliseconds >= maxMiliseconds)
            {
                if (myBirds.Count < maxBirds)
                {
                    float factor = (float)(rand.NextDouble());
                    myBirds.Add(new Bird(birdSprite, (factor * 1280f), (factor * 720f)));
                }
                currentMiliseconds = 0;
            }

            foreach (Bird myBird in myBirds)
            {

                myBird.Update(gameTime, myBirds);
            }



            currentMiliseconds += gameTime.ElapsedGameTime.Milliseconds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bird myFlock in myBirds)
            {
                myFlock.Draw(spriteBatch);
            }
        }

    }
}
