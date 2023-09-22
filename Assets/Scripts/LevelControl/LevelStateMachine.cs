﻿using System;
using UnityEngine;

namespace LevelControl
{
    public class LevelStateMachine : MonoBehaviour
    {
        public static LevelStateMachine Instance { get; private set; }

        public LevelStates state;
        public event EventHandler<LevelStates> OnGameStateChanged;

        public static event Action ShowMainMenuUI;
        public static event Action ShowGameplayUI;
        public static event Action StartGameplay;
        public static event Action ShowDeathUI;
        public static event Action ResetValues;
        public static event Action OnWinLevel;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            state = LevelStates.MainMenu;
            OnGameStateChanged?.Invoke(this, state);
        }

        private void Update()
        {
            switch (state)
            {
                case LevelStates.MainMenu:
                    ShowMainMenuUI?.Invoke();
                    state = LevelStates.MainMenu;
                    OnGameStateChanged?.Invoke(this, state);
                    break;

                case LevelStates.StartGameplay:
                    ShowGameplayUI?.Invoke();
                    state = LevelStates.StartGameplay;
                    OnGameStateChanged?.Invoke(this, state);
                    break;

                case LevelStates.GameplayInProgress:
                    StartGameplay?.Invoke();
                    state = LevelStates.GameplayInProgress;
                    OnGameStateChanged?.Invoke(this, state);
                    break;

                case LevelStates.LevelEnd:
                    state = LevelStates.LevelEnd;
                    OnWinLevel?.Invoke();
                    OnGameStateChanged?.Invoke(this, state);
                    break;

                case LevelStates.Death:
                    state = LevelStates.Death;
                    ShowDeathUI?.Invoke();
                    ResetValues?.Invoke();
                    OnGameStateChanged?.Invoke(this, state);
                    break;
            }
        }
    }
}