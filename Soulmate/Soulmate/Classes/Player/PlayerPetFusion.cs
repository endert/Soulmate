using SFML.Graphics;
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
            type = "PlayerPetFusion";
            playerTextures = new Texture[]{new Texture("Pictures/Player/PlayerWerewolf/WerwolfFront.png"), new Texture("Pictures/Player/PlayerWerewolf/WerwolfRueck.png"),
                                   new Texture("Pictures/Player/PlayerWerewolf/WerwolfSeiteRechts.png"), new Texture("Pictures/Player/PlayerWerewolf/WerwolfSeiteLinks.png")};
            duration = durationInSec * 1000;
            numFacingDirection = player.getNumFacingDirection();
            facingInDirection = player.getFacingDirection(); // RECHTS
            sprite = new Sprite(werwolfTexture[numFacingDirection]);
            sprite.Position = player.getPosition();
            position = player.getPosition();
            maxFusionValue = ObjectHandler.player.maxFusionValue;
            currentFusionValue = maxFusionValue;
            hitBox = ObjectHandler.player.getHitBox();
            maxHP = player.getMaxHP();
            currentHP = player.getCurrentHP();
            att = player.getAtt();
            def = player.getDef();

            ObjectHandler.player = this;
            ObjectHandler.gObjs.Add(this);
            ObjectHandler.deleateType("player");
            ObjectHandler.deleateType("pet");

            transformWatch.Start();
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
            fusionedPlayer.setPositon(position);
            fusionedPet.setPositon(position);
            currentFusionValue = maxFusionValue - (transformWatch.ElapsedMilliseconds/100);
            if (transformWatch.ElapsedMilliseconds >= duration)
            {
                deFuse();
            }
        }

        public void deFuse()
        {
            fusionedPlayer.currentFusionValue = currentFusionValue;
            ObjectHandler.player = fusionedPlayer;
            ObjectHandler.pet = fusionedPet;
            ObjectHandler.gObjs.Add(fusionedPlayer);
            ObjectHandler.gObjs.Add(fusionedPet);

            ObjectHandler.deleateType(type);
        }
    }
}
