using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class PlatformManage : MonoBehaviour
{
    public static PlatformManage Instance;
    [SerializeField] Transform startpoint;
    [SerializeField] Transform player;
    [SerializeField] Transform USP_Position;
    [SerializeField] MeshRenderer playerMash;
    [SerializeField] Texture oldCigrateTexture;
    [SerializeField] Texture newCigrateTexture;

    [SerializeField] List<GameObject> collectableItems = new List<GameObject>();
    [SerializeField] List<GameObject> collectUI = new List<GameObject>();

    [SerializeField] GameObject endGameUI;

    [SerializeField] CinemachineVirtualCamera fullView;
    [SerializeField] CinemachineVirtualCamera followView;
    [SerializeField] CinemachineVirtualCamera focussOnCigi;

    [SerializeField] TMP_Text startText;
    [SerializeField] TMP_Text endText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void OnEnable()
    {
        EventManager.StartGame += StartGame;
        EventManager.CollectItems += CollectItems;
        EventManager.GameOver += EndGame;

        CameraSwitcher.Register(fullView);
        CameraSwitcher.Register(followView);
        CameraSwitcher.Register(focussOnCigi);
        CameraSwitcher.SwitchCamera(fullView);

        //CollectItems(true);
    }
    private void OnDisable()
    {
        EventManager.StartGame -= StartGame;
        EventManager.CollectItems -= CollectItems;
        EventManager.GameOver -= EndGame;

        CameraSwitcher.Unregister(fullView);
        CameraSwitcher.Unregister(followView);
        CameraSwitcher.Unregister(focussOnCigi);
    }

    private void CollectItems(bool obj, int num)
    {
        num -= 1;
        Malboro.Cigarette.IsKinematic?.Invoke(obj);

        collectUI[num].SetActive(obj);
        collectUI[num].transform.position = USP_Position.position + new Vector3(0, 1, 0);
        collectUI[num].transform.GetChild(0).GetChild(0).DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutBounce)
                                        .OnComplete(()=> {
                                            collectUI[num].transform.GetChild(0).GetChild(1).DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutBounce);
                                            collectUI[num].transform.GetChild(0).GetComponent<Image>().DOFade(1, 0.5f).SetEase(Ease.InOutBounce);
                                        });

        if (obj)
        {
            Utility.SoundManager.Instance.Play("transition");
            DOTween.Sequence().AppendInterval(3.0f).OnComplete(() =>
            {
                DisableCollectUI(num);
            });
        }
    }

    void DisableCollectUI(int num)
    {
        collectUI[num].SetActive(false);

        if (num == 0)
        {
            playerMash.material.SetTexture("_MainTex", newCigrateTexture);

            CameraSwitcher.SwitchCamera(focussOnCigi);

            DOTween.Sequence().AppendInterval(3.0f).OnComplete(()=>{
                Malboro.Cigarette.IsKinematic?.Invoke(false);
                CameraSwitcher.SwitchCamera(followView);
            });
        }
        else
            Malboro.Cigarette.IsKinematic?.Invoke(false);
    }

    public void EndGame()
    {
        Malboro.Cigarette.IsKinematic?.Invoke(true);
        EventManager.Instance.isStartGame = false;
        EventManager.Instance.isGameOver = true;

        endGameUI.transform.GetChild(0).GetChild(0).DOScale(Vector3.one, 0.2f).SetEase(Ease.InOutBounce)
                                        .OnComplete(() => {
                                            endGameUI.transform.GetChild(0).GetChild(1).DOScale(Vector3.one, 0.2f).SetEase(Ease.InOutBounce).OnComplete(() => {
                                                endGameUI.transform.GetChild(0).GetChild(2).DOScale(Vector3.one, 0.2f).SetEase(Ease.InOutBounce);

                                                DOTween.Sequence().Append(endGameUI.transform.GetChild(0).GetChild(3).DOScale(Vector3.one, 0.4f).SetEase(Ease.InOutBounce))
                                                                    .AppendInterval(1.0f)
                                                                    .OnComplete(() => {
                                                                        UIManager.Instance.gameOver();

                                                                        endGameUI.transform.GetChild(0).GetChild(0).localScale = Vector3.zero;
                                                                        endGameUI.transform.GetChild(0).GetChild(1).localScale = Vector3.zero;
                                                                        endGameUI.transform.GetChild(0).GetChild(2).localScale = Vector3.zero;
                                                                        endGameUI.transform.GetChild(0).GetChild(3).localScale = Vector3.zero;

                                                                    });
                                                
                                            });
                                        });
    }

    public void StartGame()
    {
        Debug.Log("StartGame");
        startText.color = Color.white;
        endText.color = Color.white;
        playerMash.material.SetTexture("_MainTex", oldCigrateTexture);

        player.position = startpoint.position;
        player.gameObject.SetActive(true);
        foreach (GameObject g in collectableItems)
            g.SetActive(true);

        Malboro.Cigarette.IsKinematic?.Invoke(true);
        DOTween.Sequence().AppendInterval(1.0f).OnComplete(() => {
            CameraSwitcher.SwitchCamera(followView);
            DOTween.Sequence().AppendInterval(0.75f).OnComplete(() => {
                Malboro.Cigarette.IsKinematic?.Invoke(false);
                startText.color = new Color(103, 100, 89, 255);
                endText.color = new Color(103, 100, 89, 255);
            });
        });

    }
}
