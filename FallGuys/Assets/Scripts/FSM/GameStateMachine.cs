using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine
{
    private Dictionary<Type, IGameState> _states;
    private IGameState _currentState;
}
