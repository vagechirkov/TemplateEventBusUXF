using System.Collections;
using System.Linq;
using UnityEngine;
using UXF;

public class ExperimentManager : MonoBehaviour
{
    bool _isInstStartFinished, _isEvaluationFinished;

    void OnEnable()
    {
        ExpEventBus.Subscribe(ExpEvents.InstStartEnd, () => _isInstStartFinished = true);
        ExpEventBus.Subscribe(ExpEvents.EvaluationEnd, () => _isEvaluationFinished = true);
    }

    public void RunExperiment(Session uxfSession)
    {
        StartCoroutine(ExperimentLoop(uxfSession));
    }

    IEnumerator ExperimentLoop(Session uxfSession)
    {
        foreach (var trial in uxfSession.blocks.SelectMany(block => block.trials))
        {
            // Start UXF trial
            trial.Begin();

            // start instruction at the beginning of the trial
            ExpEventBus.Publish(ExpEvents.InstStartBegin);

            // wait for instruction to finish
            yield return new WaitUntil(() => _isInstStartFinished);

            // start action event
            ExpEventBus.Publish(ExpEvents.ActionBegin);

            // wait for action to finish
            yield return new WaitForSeconds(5f);
            ExpEventBus.Publish(ExpEvents.ActionEnd);

            // start evaluation at the end of the trial
            ExpEventBus.Publish(ExpEvents.EvaluationBegin);

            // wait for evaluation to finish
            yield return new WaitUntil(() => _isEvaluationFinished);

            _isInstStartFinished = _isEvaluationFinished = false;

            // end UXF trial
            trial.End();
            yield return null;
        }

        uxfSession.End();
    }
}