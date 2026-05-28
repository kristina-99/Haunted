using System;
using UnityEngine;

public class GameConstants
{
    [Flags]
    public enum StatusEffect
    {
        None = 0,
        Stunned = 1
    }

    public enum GamePhase
    {
        Night,
        Day,
        Voting,
        Ended
    }

    public enum CharacterRole
    {
        Normal,
        Protector,
        Priest,
        Trapper,
        Haunted
    }
}
