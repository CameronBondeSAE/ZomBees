using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CivViewModel : MonoBehaviour
{
    public CivGPT civGpt;
    public AudioClip clip;

    public TextMeshPro textMeshProUGUI;

    [SerializeField]
    private ParticleSystem bloodParticles;

    // bloodParticles.Emit(150);

    
    // Start is called before the first frame update
    void Start()
    {
        civGpt.GPTOutputDialogueEvent += CivGptOnGPTOutputDialogueEvent;
        civGpt.GPTPerformingActionEvent += CivGptOnGPTPerformingActionEvent;
    }


    private void CivGptOnGPTPerformingActionEvent(object sender, CivGPT.CivAction civAction)
    {
        Debug.Log("Probably remove the text now?");
        textMeshProUGUI.text = "";
    }

    private void CivGptOnGPTOutputDialogueEvent(object sender, string input)
    {
        textMeshProUGUI.text = input; // HACK
        // GetComponent<AudioSource>().clip = clip;
        // GetComponent<AudioSource>().Play();

    
    }

    
}
