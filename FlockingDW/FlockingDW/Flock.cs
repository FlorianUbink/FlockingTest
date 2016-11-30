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
        ButtonState RightPrevious;


        public Flock(ContentManager content)
        {
            birdSprite = content.Load<Texture2D>("New Piskel");
            myBirds = new List<Bird>();
            rand = new Random();
            currentMiliseconds = 0;
            maxMiliseconds = 100;
            maxBirds = 100;
        }

        public void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
            if (currentMiliseconds >= maxMiliseconds)
            {
                if (myBirds.Count < maxBirds)
                {
                    myBirds.Add(new Bird(birdSprite, rand.Next(0, 1280), rand.Next(0,720)));
                }
                currentMiliseconds = 0;
            }

            setTarget(mouse);

            foreach (Bird myBird in myBirds)
            {
                myBird.Update(myBirds);
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

        private void setTarget(MouseState mouse)
        {
            if (mouse.RightButton == ButtonState.Released && RightPrevious == ButtonState.Pressed)
            {
                foreach (Bird bird in myBirds)
                {
                    bird.Target(new Vector2(mouse.X, mouse.Y));
                }
            }

            RightPrevious = mouse.RightButton;
        }


    }
}
