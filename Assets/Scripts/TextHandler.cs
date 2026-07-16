using UnityEditor.Rendering.Universal;
using UnityEngine;

public class TextHandler : MonoBehaviour
{
    [SerializeField] private Turret tarablility;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Kills = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Kills == 30)
        {
            Upgrade();
        }
    }
    public int Kills;
    public void KillsPlus()
    {
        Kills ++;
    }
    public string GameState = "na";
    public void Upgrade()
    {
        GameState = "upgrade";
        while (GameState == "upgrade")
        {
            if (Input.GetKeyDown(KeyCode.Q) == true)
            {
                tarablility.FireRateUp();
                GameState = "na";
                Kills = 0;
            }
        }
    }
}
