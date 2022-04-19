using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InstructionUI : MonoBehaviour
{
    Button _button;
    bool _isResponded;

    void Awake()
    {
        _button = GetComponentInChildren<Button>();
    }

    void OnEnable()
    {
        StartCoroutine(ShowInstructionAndWaitButtonPress());
        _button.onClick.AddListener(() => _isResponded = true);
    }

    IEnumerator ShowInstructionAndWaitButtonPress()
    {
        yield return new WaitForSeconds(0.2f);
        yield return new WaitUntil(() => _isResponded);
        yield return new WaitForSeconds(0.5f);
        _isResponded = false;
        gameObject.SetActive(false);
    }
}