using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using UniRx;

public class GameSceneController : MonoBehaviour
{
    #region enum

    #endregion

    #region const

    #endregion

    #region public property

    #endregion

    #region private property

    /// <summary>
    /// The debug result button.
    /// </summary>
    [SerializeField]
    Button debugResultButton;

    #endregion

    #region public method

    #endregion

    #region private method

    #endregion

    #region event

    void Start()
    {
        debugResultButton.onClick.AsObservable().First()
            .Subscribe(_ => SceneManager.LoadScene(Scene.Result));
    }

    void Update()
    {

    }

    #endregion
}