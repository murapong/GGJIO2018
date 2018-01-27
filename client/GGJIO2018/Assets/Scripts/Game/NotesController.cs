using UnityEngine;

public class NotesController : MonoBehaviour
{
    #region enum

    #endregion

    #region const

    #endregion

    #region public property

    #endregion

    #region private property

    /// <summary>
    /// The game manager.
    /// </summary>
    GameManager gameManager;

    #endregion

    #region public method

    #endregion

    #region private method

    /// <summary>
    /// Checks the input.
    /// </summary>
    /// <param name="key">Key.</param>
    void CheckInput(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            gameManager.AddScore();
            Destroy(gameObject);
        }
    }

    #endregion

    #region event

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag(Tag.GameManager).GetComponent<GameManager>();
    }

    void Update()
    {
        transform.position += Vector3.down * 10 * Time.deltaTime;
        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        CheckInput(KeyCode.Space);
    }

    #endregion
}
