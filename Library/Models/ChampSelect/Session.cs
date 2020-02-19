using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models.ChampSelect
{
    public struct ActionRequest
    {
        public int actorCellId;
        public int championId;
        public bool completed;
        public uint id;
        public string type; // TODO: enum
    }

    public class Action
    {
        public int actorCellId;
        public int championId;
        public bool completed;
        public uint id;
        public bool isAllyAction;
        public bool isInProgress;
        public int pickTurn;
        public string type; // TODO: enum & props
    }

    public class Session
    {
        public Action[][] actions { get; set; }
        public bool allowBattleBoost { get; set; }
        public bool allowDuplicatePicks { get; set; }
        public bool allowLockedEvents { get; set; }
        public bool allowRerolling { get; set; }
        public bool allowSkinSelection { get; set; }
        public Bans bans { get; set; }
        public List<object> benchChampionIds { get; set; }
        public bool benchEnabled { get; set; }
        public int boostableSkinCount { get; set; }
        public ChatDetails chatDetails { get; set; }
        public int counter { get; set; }
        public EntitledFeatureState entitledFeatureState { get; set; }
        public bool isSpectating { get; set; }
        public int localPlayerCellId { get; set; }
        public int lockedEventIndex { get; set; }
        public List<object> myTeam { get; set; }
        public int rerollsRemaining { get; set; }
        public bool skipChampionSelect { get; set; }
        public List<object> theirTeam { get; set; }
        public Timer timer { get; set; }
        public List<object> trades { get; set; }
    }
    public class Bans
    {
        public List<int> myTeamBans { get; set; }
        public int numBans { get; set; }
        public List<int> theirTeamBans { get; set; }
    }

    public class ChatDetails
    {
        public string chatRoomName { get; set; }
        public object chatRoomPassword { get; set; }
    }

    public class EntitledFeatureState
    {
        public int additionalRerolls { get; set; }
        public List<object> unlockedSkinIds { get; set; }
    }

    public class Timer
    {
        public long adjustedTimeLeftInPhase { get; set; }
        public long adjustedTimeLeftInPhaseInSec { get; set; }
        public long internalNowInEpochMs { get; set; }
        public bool isInfinite { get; set; }
        public string phase { get; set; }
        public long timeLeftInPhase { get; set; }
        public long timeLeftInPhaseInSec { get; set; }
        public long totalTimeInPhase { get; set; }
    }
}
