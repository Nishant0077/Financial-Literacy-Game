using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

    public void playAction() {
        SceneManager.LoadScene("quiz_scene");
    }

    public void exitAction() {
        Application.Quit();
    }

    public void homepageAction()
    {
        SceneManager.LoadScene("computer_scene");
    }

    public void internetAction()
    {
        SceneManager.LoadScene("internet_scene");
    }

    public void creditScoreAction() {
        SceneManager.LoadScene("credit_score_scene");
    }
    public void eLedgerAction() {
        SceneManager.LoadScene("eledger_scene");
    }
}
