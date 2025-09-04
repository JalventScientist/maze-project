using UnityEngine;
using System.Collections;
using TMPro;
public class PlayerLocker : MonoBehaviour
{
    [SerializeField] GameObject plr;
    TMP_Text txt;

    private void Start()
    {
        plr.SetActive(false);
        txt = GetComponentInChildren<TMP_Text>();
    }

    public void ConfirmLoad()
    {
        txt.text = "Maze Generated!";
        StartCoroutine(WipeFrame());
    }
    IEnumerator WipeFrame()
    {
        yield return new WaitForSeconds(2f);
        plr.SetActive(true);
        gameObject.SetActive(false);
        GameObject.Find("WinCube").GetComponent<PlayerWInner>().Player = plr;
    }
}
