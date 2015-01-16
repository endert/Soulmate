﻿using SFML.Graphics;
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
        Texture playerWithoutSwordTexture = new Texture("Pictures/Player/SpielerSeiteRechts.png");

        Map map;
        Vector2f movement;
        HitBox hitBoxSword { get; set; }
        Vector2f swordPosition;
        Vector2f swordVector;

        float att = 1;
        float def = 1;
        float life = 10;

        Texture[] playerTextures = {  new Texture("Pictures/Player/SpielerFrontTest.png"), new Texture("Pictures/Player/SpielerBackTest.png"), 
                                      new Texture("Pictures/Player/SpielerSeiteRechtsSchwert.png"), new Texture("Pictures/Player/SpielerSeiteLinksSchwertTest.png") };

        public Player(Vector2f spawnPosition, Map levelMap)
        {
            type = "player";
            sprite = new Sprite(playerTextures[0]);
            sprite.Position = spawnPosition;
            position = spawnPosition;
            hitBox = new HitBox(sprite.Position, playerWithoutSwordTexture.Size.X, getHeight());

            swordVector = getSwordVector();
            hitBoxSword = new HitBox(swordVector, playerTextures[0].Size.X - playerWithoutSwordTexture.Size.X, 85);
            
            map = levelMap;
        }

        public Vector2f getSwordVector()
        {
            return new Vector2f(sprite.Position.X + 70, sprite.Position.Y + 94);
        }

        public float getAtt()
        {
            return att;
        }

        public float getLife()
        {
            return life;
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
            animate(playerTextures);
            
            //switch(playerTextures)
            //{
            //    case playerTextures[0]:
            //        {

            //        }
            //}

            sprite.Position = position;
            hitBox.setPosition(sprite.Position);

            swordPosition = getSwordVector();
            hitBoxSword.setPosition(swordPosition);

            movement = new Vector2f(0, 0);
            movement = getKeyPressed(movementSpeed);
            move(movement);

            hitFromDirections.Clear();
        }

        public void takeDamage()
        {
            if(hitAnotherEntity()&&isVulnerable()&&wasHitByEnemy())
            {
                tookDmg = true;
                Console.WriteLine("HIT!!!!");
                //if()
                life--;
                Console.WriteLine(life);
            }
        }

        public bool wasHitByEnemy()
        {
            bool hitByEnemy = false;
            for (int i = 0; i < getTypeFromTouchedEntities().Count; i++)
            {
                if (getTypeFromTouchedEntities()[i].Equals("enemy"))
                {
                    hitByEnemy = true;
                }
            }
            return hitByEnemy;
        }

        public bool pressedKeyForAttack()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                return true;
            }
            else
                return false;
        }

        public override void animate(Texture[] textureArray)
        {
            if (facingInDirection.Y > 0)
            {
                sprite = new Sprite(textureArray[0]); //front
            }
            else if (facingInDirection.Y < 0)
            {
                sprite = new Sprite(textureArray[1]); // back
            }
            else if (facingInDirection.X > 0)
            {
                sprite = new Sprite(textureArray[2]); // right
                //sprite.Position
            }
            else
            {
                sprite = new Sprite(textureArray[3]); // left
            }
        }
    }
}
