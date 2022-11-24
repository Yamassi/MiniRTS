using System.Collections;
using UnityEngine;

public class WoodController : MonoBehaviour, IInteractable
{
    [SerializeField] private int _wood;
    [SerializeField] GameObject _popUpTextPrefab;

    public int WoodCount { get => _wood; }

    private void Update()
    {
        if (_wood <= 0)
        {
            StartCoroutine(DestroyWood());
        }
    }
    public void ExtractWood(int wood)
    {
        _wood -= wood;
        //TODO
        // if (_popUpTextPrefab != null)
        // {
        //     ShowPopUpText(wood);
        // }
    }

    private void ShowPopUpText(int wood)
    {
        var text = Instantiate(_popUpTextPrefab, transform.position, Quaternion.Euler(55, 0, 0), transform);
        text.GetComponent<TextMesh>().text = "+" + wood.ToString();
    }
    IEnumerator DestroyWood()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
