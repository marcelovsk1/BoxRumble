using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject hazardPrefab;
    public int maxHazardsToSpawn = 3;
    public void Enable()
    {
      gameObject.SetActive(true);
    }
    public TMPro.TextMeshProUGUI scoreText;
    public Image backgroundMenu;

    private int score;
    private float timer;
    private static bool gameOver;
    private static GameManager instance;
    private static GameManager Instance => instance;

    void Start()
    {
        instance = this;
        StartCoroutine(SpawnHazards());
    }

    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.Escape))
      {
       if (Time.timeScale == 0)
       {
          StartCoroutine(ScaleTime(0, 1, 0.5f));
          backgroundMenu.gameObject.SetActive(false);
       }
       if (Time.timeScale == 1)
       {
          StartCoroutine(ScaleTime(1, 0, 0.5f));
          backgroundMenu.gameObject.SetActive(true);
       }
      }

      if (gameOver)
        return;
      timer += Time.deltaTime;

      if(timer >= 1f)
      {
        score++;
        scoreText.text = score.ToString();

        timer = 0;
      }
    }

    IEnumerator ScaleTime(float start, float end, float duration)
    {
      float lastTime = Time.realtimeSinceStartup;
      float timer = 0.0f;

      while (timer < duration)
      {
        Time.timeScale = Mathf.Lerp(start, end, timer / duration);
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        timer += (Time.realtimeSinceStartup - lastTime);
        lastTime = Time.realtimeSinceStartup;
        yield return null;
      }

      Time.timeScale = end;
      Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    private IEnumerator SpawnHazards()
    {
      var hazardToSpawn = Random.Range(1, 4);

      for (int i = 0; i < hazardToSpawn; i++)
    {
      var x = Random.Range(-11 ,11);
      var drag = Random.Range(1f, 3f);

      var hazard = Instantiate(hazardPrefab, new Vector3(x, 11, 0), Quaternion.identity); //Hazard Height
      hazard.GetComponent<Rigidbody>().drag = drag;
    }

      yield return new WaitForSeconds(1.5f);

      yield return SpawnHazards();

    }

    public static void GameOver()
    {
      gameOver = true;
    }

}
