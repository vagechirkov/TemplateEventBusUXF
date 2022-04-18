using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Evaluation : MonoBehaviour
{
    [SerializeField] GameObject evaluationPanel;
    [SerializeField] Slider slider;
    void OnEnable()
    {
        ExpEventBus.Subscribe(ExpEvents.EvaluationBegin, () => StartCoroutine(EvaluatePerformance()));
    }

    IEnumerator EvaluatePerformance()
    {
        evaluationPanel.SetActive(true);
        yield return new WaitUntil(() => !evaluationPanel.activeSelf);
        ExpEventBus.Publish(ExpEvents.EvaluationEnd);
        slider.value = 0.5f;
    }
}
