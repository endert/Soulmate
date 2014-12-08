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

        EnumGameStates currentGameState = EnumGameStates.inGame;
        EnumGameStates prevGameState;

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
                case EnumGameStates.none:
                    window.Close();
                    break;
                //case EGameStates.mainMenu:
                //    gameState = new MainMenu();
                //    break;
                case EnumGameStates.inGame:
                    gameState = new InGame();
                    break;
                //case EGameStates.gameWon:
                //    gameState = new GameWon();
                //    break;
                //case EGameStates.controls:
                //    gameState = new Controls();
                //    break;
                default:
                    throw new NotFiniteNumberException();
            }

            gameState.loadContent();

            gameState.initialize();

            prevGameState = currentGameState;
        }

    }
}
