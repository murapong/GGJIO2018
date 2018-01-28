using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    /// The title panel.
    /// </summary>
    [SerializeField]
    GameObject titlePanel;

    /// <summary>
    /// The game panel.
    /// </summary>
    [SerializeField]
    GameObject gamePanel;

    /// <summary>
    /// The result panel.
    /// </summary>
    [SerializeField]
    GameObject resultPanel;

    /// <summary>
    /// The line object.
    /// </summary>
    [SerializeField]
    GameObject lineObject;

    /// <summary>
    /// The game score text.
    /// </summary>
    [SerializeField]
    Text gameScoreText;

    /// <summary>
    /// The result score text.
    /// </summary>
    [SerializeField]
    Text resultScoreText;

    /// <summary>
    /// Where game is playing or not.
    /// </summary>
    bool isPlaying = false;

    /// <summary>
    /// The array of timing.
    /// </summary>
    float[] timing;

    /// <summary>
    /// The length of the audio.
    /// </summary>
    float audioLength;

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
    /// The time offset (for small adjestment).
    /// </summary>
    float timeOffset = -1f;

    #endregion

    #region public method

    /// <summary>
    /// Starts the game.
    /// </summary>
    public void StartGame()
    {
        titlePanel.SetActive(false);
        gamePanel.SetActive(true);
        resultPanel.SetActive(false);
        lineObject.SetActive(true);

        startTime = Time.time;
        audioSource.Play();
        isPlaying = true;

        // Preload object
        notes = Resources.Load("Game/Notes") as GameObject;
    }

    /// <summary>
    /// Retries the game.
    /// </summary>
    public void RetryGame()
    {
        SceneManager.LoadScene(Scene.Game);   
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

        TextAsset text = Resources.Load(Path.GetFileNameWithoutExtension(Config.TextFileName)) as TextAsset;
        StringReader reader = new StringReader(text.text);
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
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
        audioSource = GameObject.FindGameObjectWithTag(Tag.GameMusic).GetComponent<AudioSource>();
        timing = new float[1024];
        LoadCSV();

        titlePanel.SetActive(true);
        gamePanel.SetActive(false);
        resultPanel.SetActive(false);
        lineObject.SetActive(false);
        audioLength = audioSource.clip.length;
    }

    void Update()
    {
        if (!isPlaying)
        {
            return;
        }

        CheckNextNotes();
        gameScoreText.text = score.ToString();

        // after end of audio + 1.0f
        if (GetMusicTime() - 1.0f >= audioLength)
        {
            isPlaying = false;
            gamePanel.SetActive(false);
            resultPanel.SetActive(true);
            lineObject.SetActive(false);
            resultScoreText.text = score.ToString();
        }
    }

    /// <summary>
    /// Adds the score.
    /// </summary>
    public void AddScore()
    {
        score++;
    }

    /// <summary>
    /// Reduces the score.
    /// </summary>
    public void ReduceScore()
    {
        score--;
    }

    #endregion
}
