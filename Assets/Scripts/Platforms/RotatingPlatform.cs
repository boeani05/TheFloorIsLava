using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public int rotationSpeed;

    void Start()
    {

    }

    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        gameObject.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MakePlayerAChildOfRotatingPlatform(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ReverseParentChildToPlayer(other.gameObject);
        }
    }

    private void MakePlayerAChildOfRotatingPlatform(GameObject player)
    {
        player.transform.SetParent(transform);
    }

    private void ReverseParentChildToPlayer(GameObject player)
    {
        player.transform.SetParent(null);
    }
}
