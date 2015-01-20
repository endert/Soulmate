﻿using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate.Classes
{
    class PlayerPetFusion : Player
    {
        Pet fusionedPet;
        Player fusionedPlayer;
        int duration;   // in milliSec

        Texture[] werwolfTexture = {new Texture("Pictures/Player/PlayerWerewolf/WerwolfFront.png"), new Texture("Pictures/Player/PlayerWerewolf/WerwolfRueck.png"),
                                   new Texture("Pictures/Player/PlayerWerewolf/WerwolfSeiteRechts.png"), new Texture("Pictures/Player/PlayerWerewolf/WerwolfSeiteLinks.png")};

        public PlayerPetFusion(Pet pet, Player player, int durationInSec)
        {
            fusionedPet = pet;
            fusionedPlayer = player;
            type = "werwolf";
            duration = durationInSec * 1000;
            numFacingDirection = player.getNumFacingDirection();
            facingInDirection = player.getFacingDirection(); // RECHTS
            sprite = new Sprite(werwolfTexture[numFacingDirection]);
            sprite.Position = player.getPosition();
            position = player.getPosition();
            hitBox = new HitBox(sprite.Position, playerWithoutSwordTexture.Size.X, getHeight());
            maxHP = player.getMaxHP();
            currentHP = player.getCurrentHP();
            att = player.getAtt();
            def = player.getDef();

            ObjectHandler.player = this;
            transformWatch.Start();
        }
    }
}
