using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] private GoalMenuController goalMenu;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            goalMenu.ShowGoalMenu();
        }
    }
}
