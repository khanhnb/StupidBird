using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StupidBird
{
    class Footer
    {
        ContentManager Content;
        SpriteBatch spriteBatch;
        Vector2 FooterPosition1;
        Vector2 FooterPosition2;
        Texture2D FooterTexture;

        public Footer(ContentManager Content, SpriteBatch spriteBatch)
        {
            this.Content = Content;
            this.spriteBatch = spriteBatch;
            FooterPosition1 = new Vector2(0f, 650f);
            FooterPosition2 = new Vector2(480f, 650f);
            FooterTexture = Content.Load<Texture2D>("img/bg-footer");
        }
        public void Draw()
        {
            spriteBatch.Draw(FooterTexture, FooterPosition1, Color.White);
            spriteBatch.Draw(FooterTexture, FooterPosition2, Color.White);
        }
        public void Update()
        {
            FooterPosition1.X -= 5.25f;
            FooterPosition2.X -= 5.25f;
            if(FooterPosition1.X <= -480f)
            {
                FooterPosition1.X = FooterPosition2.X + 480f;
            }
            if (FooterPosition2.X <= -480f)
            {
                FooterPosition2.X = FooterPosition1.X + 480f;
            }
        }
    }
}
