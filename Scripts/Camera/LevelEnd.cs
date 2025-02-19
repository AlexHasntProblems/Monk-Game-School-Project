using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            StartCoroutine(PlayEndingScene());
        }
    }

    private IEnumerator PlayEndingScene()
    {
        Player.Instance.GetComponent<PlayerController>().enabled = false;
        yield return new WaitUntil(() => !Player.Instance.GetComponent<PlayerController>().enabled);
        Player.Instance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        StartCoroutine(SetIdleState());
        Debug.Log(Player.Instance.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name);
        yield return StartCoroutine(GetComponent<CameraFocus>().FocusAt());
        GameObject.Find("Canvas").transform.Find("Menu").gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private IEnumerator SetIdleState()
    {
        for (;true;)
        {
            Player.Instance.GetComponent<Animator>().SetTrigger("idle");
            yield return null;
        }
    }
}
