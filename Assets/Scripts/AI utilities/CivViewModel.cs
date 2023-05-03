using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lloyd;
using TMPro;
using UnityEngine;

public class CivViewModel : MonoBehaviour
{
    public CivGPT civGpt;
    public AudioClip clip;

    public TextMeshPro textMeshProUGUI;

    public Renderer mainMesh;

    [SerializeField]
    private ParticleSystem bloodParticles;

    public ElevenLabsVoiceAPITest ElevenLabsVoiceAPITest;
    
    // bloodParticles.Emit(150);

    
    // Start is called before the first frame update
    void Start()
    {
        civGpt.GPTOutputDialogueEvent += CivGptOnGPTOutputDialogueEvent;
        civGpt.GPTPerformingActionEvent += CivGptOnGPTPerformingActionEvent;

        mainMesh.material.color = new Color(Random.value, Random.value, Random.value);
    }


    private void CivGptOnGPTPerformingActionEvent(object sender, CivGPT.GPTResponseData gptResponseData)
    {
        textMeshProUGUI.DOFade(0, 2f);
    }

    private void CivGptOnGPTOutputDialogueEvent(object sender, string input)
    {
        textMeshProUGUI.DOFade(1, 2f);
        textMeshProUGUI.text = input; // HACK
        // GetComponent<AudioSource>().clip = clip;
        // GetComponent<AudioSource>().Play();

        if (ElevenLabsManager.Instance.ElevenLabsVoice)
        {
            ElevenLabsVoiceAPITest.SynthesizeSpeech(input);
        }
    
    }

    
}
