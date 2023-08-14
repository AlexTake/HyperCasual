using System.Collections;
using TMPro;
using UnityEngine;


public class Cutter : MonoBehaviour
{
    [SerializeField] private AudioClip fruitSfx;
    private int _score=0;
    public TextMeshProUGUI scoreText;
    [SerializeField] private GameObject[] fruitPrefab;

    void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<SphereCollider>().enabled = false;
        _score++;
        scoreText.text = _score.ToString();
        other.gameObject.GetComponent<AudioSource>().PlayOneShot(fruitSfx, 0.3f);
        other.gameObject.GetComponent<ParticleSystem>().Play();
        foreach (Transform child in other.transform)
        {
            child.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }

        StartCoroutine(UpdateFruit(other.gameObject));
    }

    IEnumerator UpdateFruit(GameObject fruit)
    {
        Vector3 pos = fruit.transform.position;
        yield return new WaitForSeconds(2f);
        Destroy(fruit);
        yield return new WaitForSeconds(3f);
        GameObject tempFruit = Instantiate(fruitPrefab[Random.Range(0, fruitPrefab.Length)]);
        tempFruit.transform.position = pos;
    }
}