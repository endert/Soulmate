using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate.Classes
{
    class Player : GameObjects
    {
        Texture playerTexture = new Texture("Pictures/Player/SpielerSeiteRechtsSchwert.png");
        Map map;
        Vector2f movement;

        float att = 1;
        float def = 1;

        Texture[] playerTextures;

        public Player(Vector2f spawnPosition, Map levelMap)
        {
            sprite = new Sprite(playerTexture);
            sprite.Position = spawnPosition;
            position = spawnPosition;
            hitBox = new HitBox(sprite.Position, new Texture("Pictures/Player/SpielerSeiteRechts.png").Size.X, getHeight());
            map = levelMap;
        }

        override public void update(GameTime time)
        {
            movementSpeed = 0.2f * (float)time.EllapsedTime.TotalMilliseconds;

            //Console.Clear();
            //Console.WriteLine(movementSpeed);
            //Console.WriteLine((float)time.EllapsedTime.TotalMilliseconds);

            sprite.Position = position;
            hitBox.setPosition(sprite.Position);

            movement = new Vector2f(0, 0);
            movement = getKeyPressed(movementSpeed);
            move(movement);

            hitFromDirections.Clear();
        }
    }
}
