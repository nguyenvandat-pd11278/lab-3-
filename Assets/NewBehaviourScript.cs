using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] gameObjects; // M?ng ch?a c�c ??i t??ng c?n spawn
    private List<GameObject> spawnedObjects = new List<GameObject>(); // Danh s�ch l?u c�c ??i t??ng ?� spawn
    public float fadeDuration = 5f; // Th?i gian l�m m?

    void Start()
    {
        StartCoroutine(SpawnAndMoveObjects());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Work");
            foreach (GameObject obj in spawnedObjects)
            {
                StartCoroutine(FadeCube(obj));
            }
        }
    }

    IEnumerator SpawnAndMoveObjects()
    {
        // Spawn 3 ??i t??ng c�ng l�c
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(2);
            float randomPosX = Random.Range(-5f, 5f);
            float randomPosY = Random.Range(-5f, 5f);
            float randomPosZ = Random.Range(-5f, 5f);
            Vector3 randomPos = new Vector3(randomPosX, randomPosY, randomPosZ);

            int randomIndex = Random.Range(0, gameObjects.Length);
            GameObject newObject = Instantiate(gameObjects[randomIndex], randomPos, Quaternion.identity);
            spawnedObjects.Add(newObject);
        }

        // Di chuy?n t?ng ??i t??ng l?n l??t, ch? 1 gi�y gi?a m?i l?n di chuy?n
        foreach (GameObject obj in spawnedObjects)
        {
            StartCoroutine(MoveObject(obj)); // B?t ??u di chuy?n ??i t??ng
            yield return new WaitForSeconds(1); // Ch? 1 gi�y tr??c khi di chuy?n ??i t??ng ti?p theo
        }
    }

    IEnumerator MoveObject(GameObject obj)
    {
        float elapsedTime = 0f;
        float randomPosX = Random.Range(-5f, 5f);
        float randomPosY = Random.Range(-5f, 5f);
        float randomPosZ = Random.Range(-5f, 5f);
        Vector3 randomPos = new Vector3(randomPosX, randomPosY, randomPosZ);

        while (elapsedTime < 5)
        {
            obj.transform.position = Vector3.Lerp(obj.transform.position, randomPos, elapsedTime / 5);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FadeCube(GameObject cubeClone)
    {
        Renderer renderer = cubeClone.GetComponent<Renderer>();
        if (renderer == null || renderer.material == null)
        {
            Debug.LogWarning("??i t??ng kh�ng c� Renderer ho?c Material!");
            yield break;
        }

        Color color = renderer.material.color;
        float startAlpha = color.a; // Alpha ban ??u
        float endAlpha = 0f; // Alpha sau khi l�m m?

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            // T�nh alpha hi?n t?i d?a tr�n th?i gian ?� tr�i qua
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);

            // C?p nh?t gi� tr? alpha
            color.a = newAlpha;
            renderer.material.color = color;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ??m b?o alpha ??t gi� tr? cu?i c�ng
        color.a = endAlpha;
        renderer.material.color = color;
    }
}