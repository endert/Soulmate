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
        Texture playerWithoutSwordTexture = new Texture("Pictures/Player/SpielerSeiteRechts.png");

        int numFacingDirection; // nach RECHTS

        Map map;
        Vector2f movement;
        HitBox hitBoxSword { get; set; }
        Vector2f swordPosition;
        Vector2f swordVector;

        int att;
        int def;

        Texture[] playerTextures = { new Texture("Pictures/Player/SpielerFront.png"), new Texture("Pictures/Player/SpielerRueckTest.png"), 
                                     new Texture("Pictures/Player/SpielerSeiteRechtsSchwert.png"), new Texture("Pictures/Player/SpielerSeiteLinksSchwert.png") };

        public Player(Vector2f spawnPosition, Map levelMap, int spawnNumFacingDirection)
        {
            type = "player";
            numFacingDirection = spawnNumFacingDirection;
            facingInDirection = new Vector2f(1, 0); // RECHTS
            sprite = new Sprite(playerTextures[0]);
            sprite.Position = spawnPosition;
            position = spawnPosition;
            hitBox = new HitBox(sprite.Position, playerWithoutSwordTexture.Size.X, getHeight());
            maxHP = 11 * 4;
            currentHP = maxHP;
            att = 1;
            def = 0;

            swordVector = getSwordVector();
            hitBoxSword = new HitBox(swordVector, playerTextures[2].Size.X - playerWithoutSwordTexture.Size.X, 85);
            
            map = levelMap;
        }

        override public float getWidth()
        {
            return playerTextures[0].Size.X;
        }

        public int getNumFacingDirection()
        {
            return numFacingDirection;
        }

        public Vector2f getSwordVector()
        {
            if (numFacingDirection == 2)
            {
                return new Vector2f(sprite.Position.X + 70, sprite.Position.Y + 94);
            }

            else if (numFacingDirection == 3)
            {
                return new Vector2f(sprite.Position.X, sprite.Position.Y + 94);
            }

            else
                return new Vector2f(0, 0);
        }

        public int getAtt()
        {
            return att;
        }

        public int getCurrentLife()
        {
            return currentHP;
        }

        public int getMaxLife()
        {
            return maxHP;
        }

        public HitBox getHitBoxSword()
        {
            return hitBoxSword;
        }

        override public void update(GameTime gameTime)
        {
            movementSpeed = 0.4f * (float)gameTime.EllapsedTime.TotalMilliseconds;

            //Console.Clear();
            //Console.WriteLine(movementSpeed);
            //Console.WriteLine((float)time.EllapsedTime.TotalMilliseconds);
            //InGame.HUD.update(gameTime);
            takeDamage();
            animate(playerTextures);

            switch (numFacingDirection)
            {
                case 0:
                    {
                        sprite.Position = position;
                        break;
                    }
                case 1:
                    {
                        sprite.Position = position;
                        break;
                    }
                case 2:
                    {
                        sprite.Position = position;
                        break;
                    }
                case 3:
                    {
                        sprite.Position = new Vector2f(position.X - (playerTextures[2].Size.X - playerWithoutSwordTexture.Size.X), position.Y);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            hitBox.setPosition(position);

            swordPosition = getSwordVector();
            hitBoxSword.setPosition(swordPosition);
            
            movement = new Vector2f(0, 0);
            movement = getKeyPressed(movementSpeed);
            move(movement);
            
            hitFromDirections.Clear();
            
        }

        public void takeDamage()
        {
            if (hitAnotherEntity() && isVulnerable() && wasHitByEnemy())
            {
                int dmg = 0;
                tookDmg = true;
                foreach (AbstractEnemy enemy in EnemyHandler.ENEMIES)
                {
                    if (enemy.touchedPlayer())
                    {
                        dmg = enemy.getAttackDamage();
                    }
                }

                if (dmg - def >= 0)
                {
                    currentHP -= dmg - def;
                }
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
                numFacingDirection = 0;
            }
            else if (facingInDirection.Y < 0)
            {
                sprite = new Sprite(textureArray[1]); // back
                numFacingDirection = 1;
            }
            else if (facingInDirection.X > 0)
            {
                sprite = new Sprite(textureArray[2]); // right
                numFacingDirection = 2;
            }
            else
            {
                sprite = new Sprite(textureArray[3]); // left
                numFacingDirection = 3;
            }
        }
    }
}
