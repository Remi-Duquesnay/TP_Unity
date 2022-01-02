using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour
{
    public void Action(string action)
    {
        switch (action)
        {
            case "play":
                SceneManager.LoadScene("Play");
                break;
            case "quit":
                Application.Quit();
                break;
            case "retry":
                ScoreManager.Instance.ResetScore();
                SceneManager.LoadScene("Play");
                break;
            case "died":
                StartCoroutine(GameOver());
                break;
            default:
                break;
        }
        
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameOver");
    }
}
