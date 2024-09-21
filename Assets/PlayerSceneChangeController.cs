using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSceneChangeController : MonoBehaviour
{
    public GameObject basementPoint1, basementPoint2;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("SceneChangeEast"))
        {
            Debug.Log("Change scene east");
        }
        else if (collision.collider.CompareTag("SceneChangeNorth"))
        {
            SceneManager.LoadScene("NorthScene");
        }
        else
        {
            if (basementPoint1.gameObject.GetComponent<PointCollider>().isTouched &&
                basementPoint2.gameObject.GetComponent<PointCollider>().isTouched)
            {
                Debug.Log("Change scene Basement");
            }
        }
    }
}
