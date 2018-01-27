
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NotesEditor : MonoBehaviour
{
    #region enum

    #endregion

    #region const

    #endregion

    #region public property

    #endregion

    #region private property

    /// <summary>
    /// The audio source.
    /// </summary>
    AudioSource audioSource;

    /// <summary>
    /// The start button.
    /// </summary>
    [SerializeField]
    GameObject startButton;

    /// <summary>
    /// The is playing.
    /// </summary>
    bool isPlaying = false;

    /// <summary>
    /// The start time.
    /// </summary>
    float startTime = 0;

    #endregion

    #region public method

    /// <summary>
    /// Starts the music.
    /// </summary>
    public void StartMusic()
    {
        startButton.SetActive(false);
        audioSource.Play();
        startTime = Time.time;
        isPlaying = true;
    }

    /// <summary>
    /// Writes the CSV.
    /// </summary>
    /// <param name="txt">Text.</param>
    public void WriteCSV(string txt)
    {
        StreamWriter streamWriter;
        FileInfo fileInfo;
        fileInfo = new FileInfo(Application.dataPath + "/Resources/" + Config.TextFileName);
        streamWriter = fileInfo.AppendText();
        streamWriter.WriteLine(txt);
        streamWriter.Flush();
        streamWriter.Close();
    }

    #endregion

    #region private method

    /// <summary>
    /// Gets the timing.
    /// </summary>
    /// <returns>The timing.</returns>
    float GetTiming()
    {
        return Time.time - startTime;
    }

    #endregion

    #region event

    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag(Tag.GameMusic).GetComponent<AudioSource>();
    }

    void Update()
    {
        // TODO:use UniRx
        if (Input.GetKeyDown(KeyCode.Space))
        {
            WriteCSV(GetTiming().ToString());                    
        }
    }

    #endregion
}