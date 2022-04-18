using System.Collections;
using UnityEngine;
using UXF;

public class Instructions : MonoBehaviour
{
    [SerializeField] GameObject instructionWelcome;
    [SerializeField] GameObject instructionPractice;
    [SerializeField] GameObject instructionTrial1;
    [SerializeField] Session uxfSession;

    GameObject inst;

    void OnEnable()
    {
        ExpEventBus.Subscribe(ExpEvents.InstStartBegin, () => StartCoroutine(ShowInstruction()));
    }

    IEnumerator ShowInstruction()
    {
        var trial = uxfSession.currentTrialNum;
        if (trial == 1)
        {
            instructionWelcome.SetActive(true);
            yield return new WaitUntil(() => !instructionWelcome.activeSelf);
            instructionPractice.SetActive(true);
            yield return new WaitUntil(() => !instructionPractice.activeSelf);
        }
        else if (trial == 2)
        {
            instructionTrial1.SetActive(true);
            yield return new WaitUntil(() => !instructionTrial1.activeSelf);
        }
        ExpEventBus.Publish(ExpEvents.InstStartEnd);
    }
}