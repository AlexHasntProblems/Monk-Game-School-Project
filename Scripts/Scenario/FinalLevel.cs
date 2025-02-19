using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalLevel : MonoBehaviour
{
    [SerializeField] private string[] _dialogueTextsRU;
    [SerializeField] private string[] _dialogueTextsEN;
    [SerializeField] private string[] _hintTextsRU;
    [SerializeField] private string[] _hintTextsEN;
    [SerializeField] private GameObject _borders;
    [SerializeField] private GameObject _scroll;
    [SerializeField] private Vector3 _cameraTargetPosition;
    public bool IsSkipButtonPressed { set; get; }
    private string[][] _dialogueTexts = new string[2][];
    private string[][] _hintTexts = new string[2][];
    private GameObject _canvas;
    private GameObject _demon;
    
    private void Awake()
    {
        _dialogueTexts[0] = _dialogueTextsEN;
        _dialogueTexts[1] = _dialogueTextsRU;
        _hintTexts[0] = _hintTextsEN;
        _hintTexts[1] = _hintTextsRU;
        _canvas = GameObject.Find("Canvas");
        _demon = GameObject.Find("Demon");
    }
    private void Start()
    {
        GameObject.Find("Chest").GetComponent<Chest>().ChangeLootCount(999u);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            StartCoroutine(PlayScene());
            Destroy(GetComponent<BoxCollider2D>());     
        }
    }
    private IEnumerator PlayScene()
    {
        ChangePlayerControlsActivity();
        ChangeOtherUIElementsActivity();
        Coroutine setIdleState = StartCoroutine(SetIdleState());
        Camera.main.GetComponent<CameraPositionInterpolation>().enabled = false;
        yield return StartCoroutine(TranslateCameraToTargetPosition());
        yield return StartCoroutine(TranslateDemon());
        yield return StartCoroutine(ShowDialogueWindows());
        _borders.SetActive(true);
        StopCoroutine(setIdleState);
        ChangeOtherUIElementsActivity();
        ChangePlayerControlsActivity();
        yield return new WaitForSeconds(4f);
        if (Demon.Instance != null)
            Demon.Instance.StartBattle();
        yield return new WaitUntil(() => _demon == null);
        TurnOffBorders();
        setIdleState = StartCoroutine(SetIdleState());
        ChangeOtherUIElementsActivity();
        ChangePlayerControlsActivity();
        yield return StartCoroutine(ShowDialogueWindow(_dialogueTexts[0].Length - 1, WindowType.Monk));
        ChangeOtherUIElementsActivity();
        ChangePlayerControlsActivity();
        StopCoroutine(setIdleState);
        yield return new WaitUntil(() => _scroll == null); 
        yield return StartCoroutine(FadeTransition());
        OpenMenu();
    }
    private void OpenMenu()
    {
        GameObject.Find("Canvas").transform.Find("Menu").gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    private IEnumerator FadeTransition()
    {
        GameObject fadeImage = _canvas.transform.Find("Fade").gameObject;
        fadeImage.SetActive(true);
        Image image = _canvas.transform.Find("Fade").gameObject.GetComponent<Image>();
        
        for (byte alpha = 0; alpha < 255; alpha++)
        {
            image.color = new Vector4(0f, 0f, 0f, alpha / 255f);
            yield return null;
        }
    }
    private void TurnOffBorders()
    {
        for (int i = 0; i < _borders.transform.childCount; i++)
        {
            StartCoroutine(_borders.transform.GetChild(i).gameObject.GetComponent<Fire>().GoOutFire());
        }
    }
    private IEnumerator TranslateDemon()
    {
        float speed = 3.7f;
        Rigidbody2D demonRigidbody = Demon.Instance.GetComponent<Rigidbody2D>();
        while (demonRigidbody.transform.position.x >= 70f)
        {
            demonRigidbody.velocity = new Vector2(-1f, 0f) * speed;
            yield return new WaitForFixedUpdate();
        }
        demonRigidbody.velocity = Vector2.zero;
    }
    private IEnumerator ShowDialogueWindows()
    {
        for (int i = 0; i < _dialogueTexts[0].Length - 2; i++)
        {
            yield return StartCoroutine(ShowDialogueWindow(i, WindowType.Demon));
        }
        yield return StartCoroutine(ShowDialogueWindow(_dialogueTexts[0].Length - 2, WindowType.Monk));
    }
    private IEnumerator ShowDialogueWindow(int messageIndex, WindowType windowType)
    {
        GameObject window;
        if (windowType == WindowType.Demon) 
            window = _canvas.transform.Find("Demon_dialogue_window").gameObject;
        else
            window = _canvas.transform.Find("Monk_dialogue_window").gameObject;
        GameObject skipButton = _canvas.transform.Find("Skip Button").gameObject;
        window.transform.GetChild(0).gameObject.GetComponent<Text>().text = _dialogueTexts[(int)GlobalSettings.language][messageIndex];
        window.SetActive(true);
        Hint.Instance.ShowHintWithText(_hintTexts[(int)GlobalSettings.language][0]);
        yield return new WaitForSeconds(0.5f);
        skipButton.SetActive(true);
        yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Space) || IsSkipButtonPressed));
        IsSkipButtonPressed = false;
        skipButton.SetActive(false);
        window.SetActive(false);
        Hint.Instance.Hide();
    }

    private void ChangeOtherUIElementsActivity()
    {
        GameObject healthBar = _canvas.transform.Find("Health Bar").gameObject;
        healthBar.SetActive(!healthBar.activeSelf);
        GameObject bladesCounter = _canvas.transform.Find("Blades Counter").gameObject;
        bladesCounter.SetActive(!bladesCounter.activeSelf);
    }
    private void ChangePlayerControlsActivity()
    {
        GameObject monk = GameObject.Find("Monk");
        monk.GetComponent<PlayerController>().enabled = !monk.GetComponent<PlayerController>().enabled;
        monk.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
    
    private IEnumerator SetIdleState()
    {
        while (true)
        {
            Player.Instance.GetComponent<Animator>().SetTrigger("idle");
            yield return null;
        }
    }

    private IEnumerator TranslateCameraToTargetPosition()
    {
        float lerpSpeed = 2.28f;
        while ((new Vector3(_cameraTargetPosition.x - Camera.main.transform.position.x, _cameraTargetPosition.y - Camera.main.transform.position.y, 0f)).magnitude > 0.001f)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main. transform.position, new Vector3(_cameraTargetPosition.x, _cameraTargetPosition.y, Camera.main.transform.position.z), lerpSpeed * Time.deltaTime);
            yield return null;
        }
        Camera.main.transform.position = new Vector3(_cameraTargetPosition.x, _cameraTargetPosition.y, Camera.main.transform.position.z);
    }
}
public enum WindowType
{
    Monk = 0,
    Demon = 1
}
