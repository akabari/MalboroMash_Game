using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        switch (this.transform.tag)
        {
            case "EndGame":
                EventManager.GameOver?.Invoke();
                break;
            case "Collectable":
                gameObject.SetActive(false);
                EventManager.CollectItems?.Invoke(true, int.Parse(this.name));
                break;
            case "Path":
                gameObject.SetActive(false);
                break;
        }
    }
}
