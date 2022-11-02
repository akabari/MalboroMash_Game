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

    [SerializeField] GameObject warningScreen;
    [SerializeField] GameObject startScreen;
    [SerializeField] GameObject congratulationScreen;
    [SerializeField] GameObject endScreen;
    [SerializeField] GameObject bottomPanel;

    [SerializeField] GameObject exitPopup;

    [SerializeField] GameObject exitBtn;

    [SerializeField] Transform starsParent;

    [SerializeField] VideoPlayer completeCigiVideo;
    [SerializeField] Animator endScreenAnimator;
    [SerializeField] Animator congratsScreenAnimator;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

    }

    private void Start()
    {
        bottomPanel.SetActive(false);
        warningScreen.SetActive(true);
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
        warningScreen.SetActive(false);
        bottomPanel.SetActive(true);
        startScreen.SetActive(true);
    }

    public void gameOver()
    {
        //EventManager.Instance.isStartGame = false;
        congratulationScreen.SetActive(false);
        //EventManager.Instance.isGameOver = true;

        CameraManager.Instance.transform.DOMove(new Vector3(0, 12.59f, -5.5f), 0.5f);
        CameraManager.Instance.transform.DORotate(new Vector3(70.939f, 0, 0), 0.5f);

        Invoke("EndGame", 0.5f);
    }

    void EndGame()
    {
        Utility.SoundManager.Instance.Play("complete");
        congratulationScreen.SetActive(true);
        //congratsScreenAnimator.enabled = true;
        //completeCigiVideo.Stop();
        //endScreenAnimator.enabled = false;

        DOTween.Sequence().Append(starsParent.GetChild(0).DOScale(Vector3.one, 0.3f).SetEase(Ease.InOutBounce))
                            .Append(starsParent.GetChild(1).DOScale(Vector3.one, 0.3f).SetEase(Ease.InOutBounce))
                            .Append(starsParent.GetChild(2).DOScale(Vector3.one, 0.3f).SetEase(Ease.InOutBounce))
                            .AppendInterval(3f)
                            .OnComplete(() => {
                                //congratsScreenAnimator.enabled = false;
                                congratulationScreen.SetActive(false);
                                endScreen.SetActive(true);
                                //completeCigiVideo.Play();
                                //endScreenAnimator.enabled = true;
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
        startScreen.SetActive(false);
        endScreen.SetActive(false);

        //CameraManager.Instance.cameraAnim.SetTrigger("Play");
        Invoke("DoneAnim", 1.5f);
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
        exitPopup.SetActive(true);
    }
    public void Close_ExitPopup()
    {
        exitPopup.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    #endregion

}
