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
        Texture playerWithSwordTexture = new Texture("Pictures/Player/SpielerSeiteRechtsSchwert.png");
        Texture playerWithoutSwordTexture = new Texture("Pictures/Player/SpielerSeiteRechts.png");

        Map map;
        Vector2f movement;
        HitBox hitBoxSword { get; set; }

        float att = 1;
        float def = 1;

        Texture[] playerTextures;

        public Player(Vector2f spawnPosition, Map levelMap)
        {
            type = "player";
            sprite = new Sprite(playerWithSwordTexture);
            sprite.Position = spawnPosition;
            position = spawnPosition;
            hitBox = new HitBox(sprite.Position, playerWithoutSwordTexture.Size.X, getHeight());
            hitBoxSword = new HitBox(new Vector2f(sprite.Position.X + 70, sprite.Position.Y), playerWithSwordTexture.Size.X - playerWithoutSwordTexture.Size.X, getHeight());
            map = levelMap;
        }

        public float getAtt()
        {
            return att;
        }

        public HitBox getHitBoxSword()
        {
            return hitBoxSword;
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

        //public bool takeDamage()
        //{
        //    if(hitBoxSword.hit(EnemyHandler.getEnemies()))
        //}
    }
}
