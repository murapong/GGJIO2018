using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class TitleSceneController : MonoBehaviour
{
    #region enum

    #endregion

    #region const

    #endregion

    #region public property

    #endregion

    #region private property

    /// <summary>
    /// The start button.
    /// </summary>
    [SerializeField]
    Button startButton;

    #endregion

    #region public method

    #endregion

    #region private method

    #endregion

    #region event

    void Start()
    {
        startButton.onClick.AsObservable().First()
            .Subscribe(_ => Debug.Log("Go to Game."));
    }

    void Update()
    {

    }

    #endregion
}