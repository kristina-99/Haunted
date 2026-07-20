using UnityEngine;

public class VotingSystem : MonoBehaviour
{
    void OnEnable()
    {
        GameEvents.OnDayStarted += VoteRandomly;
    }
    void OnDisable()
    {
        GameEvents.OnDayStarted -= VoteRandomly;
    }

    private void VoteRandomly()
    {

        if (this == null)
        {
            return;
        }

        BaseCharacter votedCharacter = null;
        float votingProbability = 2f / 3f;

        if (UnityEngine.Random.value <= votingProbability)
        {
            var alivePlayers = GameManager.Instance.gameStateModel.GetAlivePlayers();
            votedCharacter = alivePlayers[UnityEngine.Random.Range(0, alivePlayers.Count)];
        }

        string targetName = votedCharacter != null ? votedCharacter.gameObject.name : "Skip/No one";
        UnityEngine.Debug.Log($"{gameObject.name} voted for: {targetName}");

    // ScheduleCommand(new VoteCommand(this, votedCharacter));
        
    }
}
