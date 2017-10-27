using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    // Use this for initialization
    void Start()
    {
        keywords.Add("Turn", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("RotateGraph");
        });

        keywords.Add("Stop", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("StopRotateGraph");
        });

        //keywords.Add("Labels", () =>
        //{
        //    // Call the OnReset method on every descendant object.
        //    this.BroadcastMessage("ShowHideLabels");
        //});

        keywords.Add("Hide", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("HideNodes");
        });

        keywords.Add("Show", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("ShowNodes");
        });

        keywords.Add("All", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("ShowNodes");
        });

        keywords.Add("Drop", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("DropContainer");
        });

        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}