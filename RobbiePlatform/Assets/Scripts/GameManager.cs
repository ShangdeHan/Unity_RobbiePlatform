using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    SceneFader fader;
    List<Orb> orbs;
    Door lockedDoor;
    DoorUP lockedDoorUP;
    //public int orbNum;
    public int deathNum;
    float gameTime;
    bool gameIsOver;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        orbs = new List<Orb>();
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (gameIsOver) return;
        // orbNum = instance.orbs.Count;
        gameTime += Time.deltaTime;
        UIManager.UpdateTimeUI(gameTime);
    }

    public static void RegisterDoor(Door door)
    {
        instance.lockedDoor = door;
    }

    public static void RegisterDoorUP(DoorUP door)
    {
        instance.lockedDoorUP = door;
    }

    public static void RegisterSceneFader(SceneFader obj)
    {
        instance.fader = obj;
    }

    public static void RegisterOrb(Orb orb)
    {
        if (instance == null) return;
        if (!instance.orbs.Contains(orb)) instance.orbs.Add(orb);
        UIManager.UpdateOrbUI(instance.orbs.Count);
    }

    public static void PlayerGrabbedOrb(Orb orb)
    {
        if (!instance.orbs.Contains(orb)) return;
        instance.orbs.Remove(orb);

        if(instance.orbs.Count == 0)
        {
            instance.lockedDoor.Open();
        }
        if (instance.orbs.Count == 2)
        {
            instance.lockedDoorUP.Open();
        }
        UIManager.UpdateOrbUI(instance.orbs.Count);
    }

    public static void PlayerDied()
    {
        instance.fader.FadeOut();
        instance.deathNum++;
        UIManager.UpdateDeathUI(instance.deathNum);
        instance.Invoke("RestartScene", 1.5f);
    }

    void RestartScene()
    {
        instance.orbs.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void PlayerWon()
    {
        instance.gameIsOver = true;
        UIManager.DisplayGameOver();
        AudioManager.PlayerWonAudio();
    }

    public static bool GameOver()
    {
        return instance.gameIsOver;
    }
}
