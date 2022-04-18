using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] Canvas canvas;

    void OnEnable()
    {
        ExpEventBus.Subscribe(ExpEvents.ActionBegin, () => canvas.enabled = false);
        ExpEventBus.Subscribe(ExpEvents.PracticeBegin, () => canvas.enabled = false);
        ExpEventBus.Subscribe(ExpEvents.EvaluationBegin, () => canvas.enabled = true);
    }
}
