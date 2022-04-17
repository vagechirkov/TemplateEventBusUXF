using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InstructionUI : MonoBehaviour
{
    [SerializeField] bool isResponseNeeded = true;

    Button _button;
    bool _isResponded;

    void Awake()
    {
        _button = GetComponentInChildren<Button>();
    }

    void OnEnable()
    {
        StartCoroutine(ShowInstructionAndWaitButtonPress());
        if (_button != null)
        {
            _button.onClick.AddListener(() => _isResponded = true);
        }
    }
        
    IEnumerator ShowInstructionAndWaitButtonPress()
    {
        if (isResponseNeeded)
        {
            yield return new WaitForSeconds(0.2f);
            yield return new WaitUntil(() => _isResponded || Input.GetKeyDown(KeyCode.Space));
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
        }

        _isResponded = false;
        gameObject.SetActive(false);
    }
}