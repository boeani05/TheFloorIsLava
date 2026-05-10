using UnityEngine;

public class KillZone : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ResetPlayerPosition(other.gameObject);
        }
    }

    private void ResetPlayerPosition(GameObject player)
    {
        Vector3 startPositionOfPlayer = new(-0.233f, 1.484f, 1.002f);

        DisableCharacterController(player);

        player.transform.position = startPositionOfPlayer;

        EnableCharacterController(player);
    }

    private void DisableCharacterController(GameObject player)
    {
        player.GetComponent<CharacterController>().enabled = false;
    }

    private void EnableCharacterController(GameObject player)
    {
        player.GetComponent<CharacterController>().enabled = true;
    }
}
