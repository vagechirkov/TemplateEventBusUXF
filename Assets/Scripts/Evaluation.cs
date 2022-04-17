using System.Collections;
using UnityEngine;

public class Evaluation : MonoBehaviour
{
    [SerializeField] GameObject evaluationPanel;
    void OnEnable()
    {
        ExpEventBus.Subscribe(ExpEvents.EvaluationBegin, () => StartCoroutine(EvaluatePerformance()));
    }

    IEnumerator EvaluatePerformance()
    {
        evaluationPanel.SetActive(true);
        yield return new WaitUntil(() => !evaluationPanel.activeSelf);
        ExpEventBus.Publish(ExpEvents.EvaluationEnd);
    }
}
