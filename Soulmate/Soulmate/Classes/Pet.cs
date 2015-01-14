﻿using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate.Classes
{
    class Pet : GameObjects
    {
        Texture petTexture = new Texture("Pictures/Pet/WolfSeiteLinks.png");

        
        public Pet(Sprite player)
        {
            sprite = new Sprite(petTexture);
            sprite.Position = new Vector2f(player.Position.X - 100, player.Position.Y);
        }

        override public void update(GameTime time)
        {
            hitBox = new HitBox(sprite.Position, getWeidth(), getHeight());
        }
    }
}
