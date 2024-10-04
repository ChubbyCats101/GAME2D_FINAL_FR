using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public PlayerMoveControls Player;
    public GameObject[] Stages;
    //UI
    public Image[] UIhealth;
    public Text UIPoint;
    public Text UIStage;
    public GameObject UIRestartButton;

    void Update()
    {
        UIPoint.text = (totalPoint + stagePoint).ToString();
    }

    public void Nextstage()
    {

        if (stageIndex < Stages.Length - 1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();

            UIStage.text = "STAGE " + (stageIndex + 1);
        }
        else
        {
            //Game Clear
            //Player Control Lock
            Time.timeScale = 0;
            //retry Button UI
            Text btnText = UIRestartButton.GetComponentInChildren<Text>();
            btnText.text = "Clear";
            ViewBtn();
        }

        //Calculate Point 
        totalPoint += stagePoint;
        stagePoint = 0;
    }

    public void HealthDown()
    {
        if (health > 1)
        {
            health--;
            UIhealth[health].color = new Color(1, 0, 0, 0.4f);
        }
        else
        {
            //ALL UI Heal Off
            UIhealth[0].color = new Color(1, 0, 0, 0.4f);
            //Player Die Effect
            Player.onDie();
            //Result UI
            Debug.Log("ตายแล้วเล่นใหม่นะน้อง");
            //retry Button UI
            UIRestartButton.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Player Reposition
            if (health > 1)
            {
                PlayerReposition();
            }
            //Health Down
            HealthDown();
        }
    }

    void PlayerReposition()
    {
        Player.transform.position = new Vector3(0, 0, -1);
        Player.VelocityZero();
    }

    void ViewBtn()
    {
        UIRestartButton.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
