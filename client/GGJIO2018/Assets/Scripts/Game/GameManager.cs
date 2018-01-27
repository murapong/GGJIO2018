using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
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
    /// The notes.
    /// </summary>
    GameObject notes;

    /// <summary>
    /// The start button.
    /// </summary>
    [SerializeField]
    GameObject startButton;

    /// <summary>
    /// The good effect.
    /// </summary>
    GameObject goodEffect;

    /// <summary>
    /// The bad effect.
    /// </summary>
    GameObject badEffect;

    /// <summary>
    /// The score text.
    /// </summary>
    [SerializeField]
    Text scoreText;

    /// <summary>
    /// Where game is playing or not.
    /// </summary>
    bool isPlaying = false;

    /// <summary>
    /// The array of timing.
    /// </summary>
    float[] timing;

    /// <summary>
    /// The notes count.
    /// </summary>
    int notesCount = 0;

    /// <summary>
    /// The start time.
    /// </summary>
    float startTime = 0;

    /// <summary>
    /// The score.
    /// </summary>
    int score = 0;

    /// <summary>
    /// The time offset.
    /// </summary>
    float timeOffset = -1f;

    #endregion

    #region public method

    /// <summary>
    /// Starts the game.
    /// </summary>
    public void StartGame()
    {
        startButton.SetActive(false);
        startTime = Time.time;
        audioSource.Play();
        isPlaying = true;

        // Preload object
        goodEffect = Resources.Load("Game/GoodEffect") as GameObject;
        badEffect = Resources.Load("Game/BadEffect") as GameObject;
        notes = Resources.Load("Game/Notes") as GameObject;
    }

    #endregion

    #region private method

    /// <summary>
    /// Checks the next notes.
    /// </summary>
    void CheckNextNotes()
    {
        while (timing[notesCount] + timeOffset < GetMusicTime() && timing[notesCount] != 0)
        {
            SpawnNotes();
            notesCount++;
        }
    }

    /// <summary>
    /// Spawns the notes.
    /// </summary>
    void SpawnNotes()
    {
        Instantiate(notes, new Vector3(0, 10.0f, 0), Quaternion.identity);
    }

    /// <summary>
    /// Load the CSV.
    /// </summary>
    void LoadCSV()
    {
        int i = 0;

        //TODO:move fine name to config
        TextAsset text = Resources.Load("NotesData") as TextAsset;
        StringReader reader = new StringReader(text.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            //            Debug.Log(line);
            timing[i] = float.Parse(line);
            i++;
        }
    }

    /// <summary>
    /// Gets the music time.
    /// </summary>
    /// <returns>The music time.</returns>
    float GetMusicTime()
    {
        return Time.time - startTime;
    }

    #endregion

    #region event

    void Start()
    {
        audioSource = GameObject.Find("GameMusic").GetComponent<AudioSource>();
        timing = new float[1024];
        LoadCSV();
    }

    void Update()
    {
        if (!isPlaying)
        {
            return;
        }

        CheckNextNotes();
        scoreText.text = "Score : " + score;
    }

    /// <summary>
    /// Adds the score.
    /// </summary>
    public void AddScore()
    {
        var go = Instantiate(goodEffect);
        Destroy(go, 0.5f);
        score++;
    }

    #endregion
}