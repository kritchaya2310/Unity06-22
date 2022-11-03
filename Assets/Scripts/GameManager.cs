using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Finish")]
    public Computer[] computers;
    [SerializeField] private GameObject gate;
    [SerializeField] private GameObject finishGate;

    // Start is called before the first frame update
    void Start()
    {
        computers = FindObjectsOfType<Computer>();

        var numGameManager = FindObjectsOfType<GameManager>().Length;
        if (numGameManager > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        computers = FindObjectsOfType<Computer>();
        GateOpen();
    }

    public IEnumerator ProcessPlayerDeath(float waitRespawn)
    {
        yield return new WaitForSeconds(waitRespawn);
        LoadScene(GetCurrentBuildIndex());
    }

    public IEnumerator LoadNextLevel(float waitTime)
    {
        AudioManager.instance.PlaySFX(3);
        var nextSceneIndex = GetCurrentBuildIndex() + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        yield return new WaitForSeconds(waitTime);

        LoadScene(nextSceneIndex);
    }

    public int GetCurrentBuildIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadScene(int buildindex)
    {
        SceneManager.LoadScene(buildindex);
        DOTween.KillAll();
    }

    private void GateOpen()
    {
        if (computers.Length == 0)
        {
            
            gate.gameObject.SetActive(false);
            finishGate.gameObject.SetActive(true);
        }
        else
        {
          
            gate.gameObject.SetActive(true);
            finishGate.gameObject.SetActive(false);
        }
    }
}
