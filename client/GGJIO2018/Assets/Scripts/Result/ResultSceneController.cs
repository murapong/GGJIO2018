using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using UniRx;

public class ResultSceneController : MonoBehaviour
{
    #region enum

    #endregion

    #region const

    #endregion

    #region public property

    /// <summary>
    /// The retry button.
    /// </summary>
    [SerializeField]
    Button retryButton;

    #endregion

    #region private property

    #endregion

    #region public method

    #endregion

    #region private method

    #endregion

    #region event

    void Start()
    {
        retryButton.onClick.AsObservable().First()
            .Subscribe(_ => SceneManager.LoadScene(Scene.Game));
    }

    void Update()
    {

    }

    #endregion
}