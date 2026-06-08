using static GameConstants;

public interface IMiniGame
{
    MiniGameType MiniGameType{get;}
    string DisplayName{get;}
    float TaskDuration{get;}
    RoomId AssignedRoom{get;}
}
