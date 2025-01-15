using UnityEngine;
using System.Collections;

public class AndroidTextToSpeech : MonoBehaviour
{
    private AndroidJavaObject textToSpeech;

    void Start()
    {
        // Initialize TextToSpeech after Unity is initialized.
        InitializeTextToSpeech();
    }

    private void InitializeTextToSpeech()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            using (AndroidJavaClass textToSpeechClass = new AndroidJavaClass("android.speech.tts.TextToSpeech"))
            {
                // Get the Android context (Application Context)
                using (AndroidJavaObject context = new AndroidJavaObject("android.content.ContextWrapper", GetUnityActivity()))
                {
                    // Create the TextToSpeech object
                    textToSpeech = new AndroidJavaObject("android.speech.tts.TextToSpeech", context, new AndroidJavaRunnable(OnInitCompleted));
                }
            }
        }
    }

    // Callback when TextToSpeech initialization is complete
    private void OnInitCompleted()
    {
        if (textToSpeech != null)
        {
            Debug.Log("TextToSpeech Initialized!");
        }
        else
        {
            Debug.LogError("TextToSpeech Initialization Failed");
        }
    }

    // Method to speak a text
    public void SpeakText(string text)
    {
        if (textToSpeech != null)
        {
            // Set language to English (or change as needed)
            using (AndroidJavaObject locale = new AndroidJavaObject("java.util.Locale", "en", "US"))
            {
                textToSpeech.Call<int>("setLanguage", locale);
            }

            // Speak the text
            textToSpeech.Call("speak", text, 0, null, null);
        }
    }

    // Get the Unity Activity context (important for Android integration)
    private static AndroidJavaObject GetUnityActivity()
    {
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            return unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        }
    }

    void OnApplicationQuit()
    {
        if (textToSpeech != null)
        {
            textToSpeech.Call("stop");
            textToSpeech.Call("shutdown");
        }
    }
}
