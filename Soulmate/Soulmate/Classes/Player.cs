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
        Vector2f swordPosition;
        Vector2f swordVector;

        float att = 1;
        float def = 1;
        float life = 10;
        bool attack = false;

        Texture[] playerTextures = { new Texture("Pictures/Player/SpielerSeiteRechtsSchwert.png") };

        public Player(Vector2f spawnPosition, Map levelMap)
        {
            type = "player";
            sprite = new Sprite(playerWithSwordTexture);
            sprite.Position = spawnPosition;
            position = spawnPosition;
            hitBox = new HitBox(sprite.Position, playerWithoutSwordTexture.Size.X, getHeight());

            swordVector = new Vector2f(sprite.Position.X + 70, sprite.Position.Y + 94);
            hitBoxSword = new HitBox(swordVector, playerWithSwordTexture.Size.X - playerWithoutSwordTexture.Size.X, 85);
            
            map = levelMap;
        }

        public float getAtt()
        {
            return att;
        }

        public float getLife()
        {
            return life;
        }

        public bool getAttack()
        {
            return attack;
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

            takeDamage();
            
            sprite.Position = position;
            hitBox.setPosition(sprite.Position);

            swordPosition = new Vector2f(sprite.Position.X + 70, sprite.Position.Y + 94);
            hitBoxSword.setPosition(swordPosition);

            movement = new Vector2f(0, 0);
            movement = getKeyPressed(movementSpeed);
            move(movement);

            hitFromDirections.Clear();
        }

        public void takeDamage()
        {
            if(hitAnotherEntity())
            {
                Console.WriteLine("HIT!!!!");
                life--;
                Console.WriteLine(life);
            }
        }

        public void pressedKeyForAttack()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                attack = true;
            }
            else
                attack = false;
        }
    }
}
