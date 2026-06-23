using UnityEngine;

public class ReportCommand : ICommand
{
    public ReportCommand()
    {
        
    }

    public void Execute()
    {
        Debug.Log("Dead body reported");
    }
}
