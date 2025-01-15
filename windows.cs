using UnityEngine;
using System.Speech.Synthesis;

public class TextToSpeech : MonoBehaviour
{
    private SpeechSynthesizer synthesizer;

    // Initialize the SpeechSynthesizer
    void Start()
    {
        synthesizer = new SpeechSynthesizer();
        
        // Set the desired voice (optional)
        SetVoice("Microsoft David Desktop");  // Change this to any voice you prefer

        // Example: Speak a text
        SpeakText("Hello, welcome to the game!");
    }

    // Method to set the voice
    void SetVoice(string voiceName)
    {
        foreach (var voice in synthesizer.GetInstalledVoices())
        {
            if (voice.VoiceInfo.Name == voiceName)
            {
                synthesizer.SelectVoice(voice.VoiceInfo.Name);
                Debug.Log("Voice set to: " + voiceName);
                return;
            }
        }
        Debug.LogError("Voice not found: " + voiceName);
    }

    // Method to speak a text
    public void SpeakText(string text)
    {
        synthesizer.SpeakAsync(text);
    }

    // Cleanup
    void OnApplicationQuit()
    {
        if (synthesizer != null)
        {
            synthesizer.Dispose();
        }
    }
}
