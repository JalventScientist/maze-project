using UnityEngine;
public class PlayerWInner : MonoBehaviour
{
    bool Triggered = false;

    public GameObject Player;
    [SerializeField] GameObject WinScreen;

    float dist = 50;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }
    private void FixedUpdate()
    {
        
        if(dist < 1.65f && !Triggered)
        {
            triggerWIn();
        } else if (!Triggered)
        {
            if(Player != null)
                dist = Vector3.Distance(transform.position, Player.transform.position);
        }
    }

    void triggerWIn()
    {
        GameObject.FindFirstObjectByType<Timer>().TimerActive = false;
        Cursor.lockState = CursorLockMode.None;
        Triggered = true;
        Player.SetActive(false);
        WinScreen.SetActive(true);
    }
}
