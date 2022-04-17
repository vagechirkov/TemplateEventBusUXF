using System.Collections;
using System.Linq;
using UnityEngine;
using UXF;

public class ExperimentManager : MonoBehaviour
{
    bool _isInstStartFinished, _isInstFinishFinished;

    void OnEnable()
    {
        ExpEventBus.Subscribe(ExpEvents.InstStartEnd, () => _isInstStartFinished = true);
        ExpEventBus.Subscribe(ExpEvents.InstFinishEnd, () => _isInstFinishFinished = true);
            
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
            // ExpEventBus.Publish(ExpEvents.InstStartBegin);
                    
            // wait for instruction to finish
            // yield return new WaitUntil(() => _isInstStartFinished);
                    
            // start action event
            ExpEventBus.Publish(ExpEvents.ActionBegin);
                    
            // wait for action to finish
            yield return new WaitForSeconds(5f);
            ExpEventBus.Publish(ExpEvents.ActionEnd);
            
            yield return new WaitForSeconds(1f);
            
            // start instruction at the end of the trial
            // ExpEventBus.Publish(ExpEvents.InstFinishBegin);
                
            // wait for instruction to finish
            // yield return new WaitUntil(() => _isInstFinishFinished);

            _isInstStartFinished = _isInstFinishFinished = false;
                    
            // end UXF trial
            trial.End();
            yield return null;
        }
        uxfSession.End();
    }
        
}