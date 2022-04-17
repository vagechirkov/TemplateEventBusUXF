using System.Collections;
using UnityEngine;

public class InstructionUI : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(ShowInstructionAndWaitButtonPress());
    }

    IEnumerator ShowInstructionAndWaitButtonPress()
    {
        yield return new WaitForSeconds(0.2f);
        yield return new WaitUntil(() => Input.GetButton("Jump"));
        yield return new WaitForSeconds(0.5f);


        gameObject.SetActive(false);
    }
}