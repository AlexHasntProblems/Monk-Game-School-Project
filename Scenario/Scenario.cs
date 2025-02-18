using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scenario : MonoBehaviour
{
    [SerializeField] private Transform _monk;
    [SerializeField] private Transform _demon;
    [SerializeField] private Transform _roll;
    [SerializeField] private GameObject _monkDialogueWindow;
    [SerializeField] private GameObject _demonDialogueWindow;
    [SerializeField] private GameObject _mobileControlls;
    [SerializeField] private MusicPlayer _musicPlayer;
    [SerializeField] private string[] _dialogueTextsRU;
    [SerializeField] private string[] _dialogueTextsEN;
    [SerializeField] private string[] _hintTextsRU;
    [SerializeField] private string[] _hintTextsEN;
    private Animator _monkAnimator;
    private string[][] _dialogueTexts = new string[2][];
    private string[][] _hintTexts = new string[2][];
    private Animator _demonAnimator;
    private bool _isSkipButtonPressed = false;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        _monkAnimator = _monk.GetComponent<Animator>();
        _demonAnimator = _demon.GetComponent<Animator>(); 
        _dialogueTexts[0] = _dialogueTextsEN;
        _dialogueTexts[1] = _dialogueTextsRU;
        _hintTexts[0] = _hintTextsEN;
        _hintTexts[1] = _hintTextsRU;
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(PlayScene());
    }

    private IEnumerator PlayScene()
    {
        yield return new WaitForSeconds(5f);
        yield return StartCoroutine(MoveDemonToBase());
        _demon.GetComponent<AudioSource>().Stop();
        _demonAnimator.SetTrigger("idle");
        yield return new WaitForSeconds(0.5f);
        Destroy(_roll.gameObject);     
        yield return StartCoroutine(MoveDemonOut());
        Destroy(_demon.gameObject);  
        _monkAnimator.SetTrigger("idle");
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(ShowDialogueWindowWithText(_monkDialogueWindow, _dialogueTexts[(int)GlobalSettings.language][0]));
        Hint.Instance.Hide();
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(ShowDialogueWindowWithText(_monkDialogueWindow, _dialogueTexts[(int)GlobalSettings.language][1]));
        _monkDialogueWindow.SetActive(false);
        Hint.Instance.Hide();
        if (!UnityEngine.Device.Application.isMobilePlatform)
        {
            Guide.Instance.Show();
            Hint.Instance.Show();
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => Input.GetKey(KeyCode.Space));
            Hint.Instance.Hide();
            Guide.Instance.Hide();
        }        
        _monk.GetComponent<PlayerController>().enabled = true;
        GameObject.Find("Canvas").transform.Find("Health Bar").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("Blades Counter").gameObject.SetActive(true);
        if (UnityEngine.Device.Application.isMobilePlatform)
            _mobileControlls.SetActive(true);
        yield return new WaitUntil(() => _monk.position.x >= 20f);
        _monk.localScale = new Vector3(1f, 1f, 1f);
        Player.Instance.MovementSpeed /= 1.5f;
        Camera.main.transform.position = new Vector3(Player.Instance.transform.position.x, Player.Instance.transform.position.y, Camera.main.transform.position.z);
        Camera.main.GetComponent<CameraPositionInterpolation>().enabled = true;
        _musicPlayer.PlayNextClip();
    } 

    private IEnumerator MoveDemonToBase()
    {
        _demonAnimator.SetTrigger("walk");
        _demon.GetComponent<AudioSource>().Play();

        while (_demon.position.x >= 0.9f)
        {
            _demon.position += new Vector3(-1, 0, 0) * Time.deltaTime * 6;
            yield return null;
        }
    }

    private IEnumerator MoveDemonOut()
    {
        _demonAnimator.SetTrigger("walk");
        _demon.GetComponent<SpriteRenderer>().flipX = true;
        _demon.GetComponent<AudioSource>().Play();
        
        while (_demon.position.x <= 15f)
        {
            _demon.position += new Vector3(1, 0, 0) * Time.deltaTime * 6;
            yield return null;
        }
    }

    private IEnumerator ShowDialogueWindowWithText(GameObject window, string text)
    {
        window.SetActive(true);
        if (UnityEngine.Device.Application.isMobilePlatform) 
            GameObject.Find("Canvas").transform.Find("Skip Button").gameObject.SetActive(true);
        window.transform.GetChild(0).GetComponent<Text>().text = text;
        Hint.Instance.ShowHintWithText(_hintTexts[(int)GlobalSettings.language][0]);
        yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space)) || _isSkipButtonPressed);
        _isSkipButtonPressed = false;
        if (UnityEngine.Device.Application.isMobilePlatform)
            GameObject.Find("Canvas").transform.Find("Skip Button").gameObject.SetActive(false);
    }
    public void Skip()
    {
        _isSkipButtonPressed = true;
    }
    private void AttachCameraToPlayer()
    {
        Camera.main.transform.position = new Vector3(Player.Instance.transform.position.x, Player.Instance.transform.position.y, Camera.main.transform.position.z);
        Camera.main.transform.SetParent(_monk);
    }
}
