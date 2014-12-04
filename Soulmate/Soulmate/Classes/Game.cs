﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using Soulmate.Classes;

namespace Soulmate
{
    class Game : Soulmate.AbstractGame
    {
        public static uint windowSizeX = 1280;
        public static uint windowSizeY = 720;

        EGameStates currentGameState = EGameStates.inGame;
        EGameStates prevGameState;

        GameStates gameState;

        public Game() : base(windowSizeX, windowSizeY, "Soulmate")
        {

        }

        public override void update(GameTime time)
        {
            if (currentGameState != prevGameState)
                handleGameState();

            currentGameState = gameState.update(time);
        }

        public override void draw(RenderWindow window)
        {
            gameState.draw(window);
        }

        void handleGameState()
        {
            switch (currentGameState)
            {
                case EGameStates.none:
                    window.Close();
                    break;
                //case EGameStates.mainMenu:
                //    gameState = new MainMenu();
                //    break;
                case EGameStates.inGame:
                    gameState = new InGame();
                    break;
                //case EGameStates.gameWon:
                //    gameState = new GameWon();
                //    break;
                //case EGameStates.controls:
                //    gameState = new Controls();
                //    break;
            }

            gameState.loadContent();

            gameState.initialize();

            prevGameState = currentGameState;
        }

    }
}