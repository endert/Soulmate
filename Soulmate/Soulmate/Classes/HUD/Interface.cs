﻿using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate.Classes
{
    class Interface
    {
        LifePlayer lifePlayer;
        BarFusionPlayerPet barFusionPlayerPet;

        public Interface()
        {
            lifePlayer = new LifePlayer();
            barFusionPlayerPet = new BarFusionPlayerPet();
        }

        public void update(GameTime gameTime)
        {
            lifePlayer.update(gameTime);
            barFusionPlayerPet.update(lifePlayer.getLastLifeHeartSpritePositionBottomY());
        }

        public void draw(RenderWindow window)
        {
            lifePlayer.draw(window);
            barFusionPlayerPet.draw(window);
        }
    }
}
