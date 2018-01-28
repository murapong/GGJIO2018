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

    /// <summary>
    /// The good effect.
    /// </summary>
    GameObject goodEffect;

    /// <summary>
    /// The bad effect.
    /// </summary>
    GameObject badEffect;

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
            SpawnEffect(goodEffect);
        }
    }

    /// <summary>
    /// Spawns the effect.
    /// </summary>
    void SpawnEffect(GameObject effect)
    {
        var go = Instantiate(effect, gameObject.transform.position, Quaternion.identity);
        Destroy(go, 0.5f);
    }

    #endregion

    #region event

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag(Tag.GameManager).GetComponent<GameManager>();

        // Preload
        goodEffect = Resources.Load("Game/GoodEffect") as GameObject;
        badEffect = Resources.Load("Game/BadEffect") as GameObject;
    }

    void Update()
    {
        transform.position += Vector3.down * 10 * Time.deltaTime;
        if (transform.position.y < -5.0f)
        {
            gameManager.ReduceScore();
            Destroy(gameObject);

            SpawnEffect(badEffect);
        }
    }

    void OnTriggerStay(Collider other)
    {
        CheckInput(KeyCode.Space);
    }

    #endregion
}
