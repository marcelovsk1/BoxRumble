using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
  public GameManager gameManager;

  private void Start()
  {
    GetComponentInChildren<TMPro.TextMeshProUGUI().gameObject
      .LeanScale(new Vector3(1.2f, 1.2f), 0.5f)
      .setLoopPingPong();
  }
  public void Play()
  {
    GetComponent<CanvasGroup>().LeanAlpha(0, 0.4f)
      .setOncComplete(OnComplete);
  }

}
