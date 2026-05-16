using UnityEngine;

public class KillZone : MonoBehaviour
{

    [SerializeField] Transform startPositionOfPlayer;
    [SerializeField] Transform startPositionOfLava;
    [SerializeField] float startRisingAt;
    [SerializeField] float risingSpeed;

    void Update()
    {
        Timer();

        Rise();
    }

    private void Timer()
    {

        if (!DidTimerRunOut())
        {
            startRisingAt -= Time.deltaTime;
        }
    }

    private void Rise()
    {
        if (DidTimerRunOut())
        {
            LetLavaRise();
        }
    }

    private bool DidTimerRunOut()
    {
        return startRisingAt < 0;
    }

    private void LetLavaRise()
    {
        gameObject.transform.position += new Vector3(0, risingSpeed, 0) * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            ResetPlayerPosition(other.gameObject);

            gameObject.transform.position = startPositionOfLava.position;
        }
    }

    private void ResetPlayerPosition(GameObject player)
    {
        DisableCharacterController(player);

        player.transform.position = startPositionOfPlayer.position;

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
