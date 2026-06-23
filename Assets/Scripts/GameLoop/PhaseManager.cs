using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    private float counter = 0f;
    void Start()
    {

    }

    void Update()
    {
        counter++;
        if(counter == 20f)
        {
            RoleFactory.AssignRoles();
        }
    }
}
