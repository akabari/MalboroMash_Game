using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public static Action GameOver;

    [SerializeField] CanvasGroup warningScreen;
    [SerializeField] CanvasGroup startScreen;
    [SerializeField] CanvasGroup congratulationScreen;
    [SerializeField] CanvasGroup endScreen;

    [SerializeField] GameObject bottomPanel;

    [SerializeField] CanvasGroup exitPopup;

    [SerializeField] GameObject exitBtn;

    [SerializeField] Transform starsParent;

    //[SerializeField] VideoPlayer completeCigiVideo;
    [Header("Animators")]
    [SerializeField] Animator endScreenAnimator;
    [SerializeField] Animator congratsScreenAnimator;
    [SerializeField] Animator confitiAnimator;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

    }

    private void Start()
    {
        bottomPanel.SetActive(false);
        //warningScreen.SetActive(true);
        //OpenNextScreen(null, warningScreen);
        exitBtn.SetActive(false);
        Invoke("StartGame", 2.0f);
    }



    //private void OnEnable()
    //{
    //    EventManager.GameOver += gameOver;
    //}
    //private void OnDisable()
    //{
    //    EventManager.GameOver -= gameOver;
    //}

    void StartGame()
    {
        exitBtn.SetActive(true);
        bottomPanel.SetActive(true);
        //warningScreen.SetActive(false);
        //startScreen.SetActive(true);
        OpenNextScreen(warningScreen, startScreen);
    }

    public void gameOver()
    {
        //EventManager.Instance.isStartGame = false;
        //congratulationScreen.SetActive(false);
        OpenNextScreen(congratulationScreen, null);
        //EventManager.Instance.isGameOver = true;

        CameraManager.Instance.transform.DOMove(new Vector3(0, 12.59f, -5.5f), 0.5f);
        CameraManager.Instance.transform.DORotate(new Vector3(70.939f, 0, 0), 0.5f);

        Invoke("EndGame", 0.5f);
    }

    void EndGame()
    {
        starsParent.GetChild(0).localScale = Vector3.zero;
        starsParent.GetChild(1).localScale = Vector3.zero;
        starsParent.GetChild(2).localScale = Vector3.zero;

        Utility.SoundManager.Instance.Play("complete");
        //congratulationScreen.SetActive(true);
        OpenNextScreen(null, congratulationScreen);
        //congratsScreenAnimator.enabled = false;
        //congratsScreenAnimator.enabled = true;
        congratsScreenAnimator.Play(0);

        //confitiAnimator.enabled = false;
        //confitiAnimator.enabled = true;
        confitiAnimator.Play(0);
        //completeCigiVideo.Stop();
        //endScreenAnimator.enabled = false;

        DOTween.Sequence().Append(starsParent.GetChild(0).DOScale(Vector3.one, 0.3f).SetEase(Ease.InOutBounce))
                            .Append(starsParent.GetChild(1).DOScale(Vector3.one, 0.3f).SetEase(Ease.InOutBounce))
                            .Append(starsParent.GetChild(2).DOScale(Vector3.one, 0.3f).SetEase(Ease.InOutBounce))
                            .AppendInterval(6.6f)
                            .OnComplete(() => {
                                //congratsScreenAnimator.enabled = false;
                                //confitiAnimator.enabled = false;
                                //congratulationScreen.SetActive(false);
                                //endScreen.SetActive(true);
                                OpenNextScreen(congratulationScreen, endScreen);
                                //completeCigiVideo.Play();
                                //endScreenAnimator.enabled = false;
                                //endScreenAnimator.enabled = true;
                                endScreenAnimator.Play(0);
                            });

        //endScreen.SetActive(true);
        //CameraManager.Instance.cameraAnim.enabled = true;
        //CameraManager.Instance.cameraAnim.SetTrigger("Stop");
    }

    public void OnClick_Start()
    {
        Debug.Log("OnClick_Start");
        Utility.SoundManager.Instance.Play("btn_click");
        EventManager.Instance.isGameOver = false;
        EventManager.StartGame?.Invoke();
        //startScreen.SetActive(false);
        //endScreen.SetActive(false);
        OpenNextScreen(startScreen, null);
        OpenNextScreen(endScreen, null);

        //CameraManager.Instance.cameraAnim.SetTrigger("Play");
        Invoke("DoneAnim", 1.5f);

        //congratsScreenAnimator.enabled = false;
        //confitiAnimator.enabled = false;
        //endScreenAnimator.enabled = false;
    }
    void DoneAnim()
    {
        EventManager.Instance.isStartGame = true;
        //CameraManager.Instance.cameraAnim.enabled = false;
        //CameraManager.Instance.cameraAnim.ResetTrigger("Play");
    }

    #region Sound
    bool isSound = true;
    public void OnClick_Sound(Image img)
    {
        isSound = !isSound;
        if (isSound)
        {
            img.sprite = Utility.SoundManager.Instance.soundOn;
        }
        else
        {
            img.sprite = Utility.SoundManager.Instance.soundOff;
        }
        Utility.SoundManager.Instance.SetSoundTypeOnOff(Utility.SoundManager.SoundType.SoundEffect, isSound);
        Utility.SoundManager.Instance.SetSoundTypeOnOff(Utility.SoundManager.SoundType.Music, isSound);
    }

    #endregion

    #region Exit

    public void Open_ExitPopup()
    {
        OpenNextScreen(null, exitPopup);
        //exitPopup.SetActive(true);
    }
    public void Close_ExitPopup()
    {
        OpenNextScreen(exitPopup, null);
        //exitPopup.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    #endregion

    void OpenNextScreen(CanvasGroup hideScreen, CanvasGroup showScreen)
    {
        if (hideScreen)
        {
            hideScreen.DOFade(0, 0.5f);
            hideScreen.interactable = false;
            hideScreen.blocksRaycasts = false;
            hideScreen.ignoreParentGroups = false;
        }
        if (showScreen)
        {
            showScreen.DOFade(1, 0.5f);
            showScreen.interactable = true;
            showScreen.blocksRaycasts = true;
            showScreen.ignoreParentGroups = true;
        }
    }
}
