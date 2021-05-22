using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;

/* Animacje podczas uruchamiania gry */
public class Load : MonoBehaviour
{
    public GameObject mainShip;
    public Image loadBackground;
    public Material mainShipParticleMat;

    void Start()
    {
        Game.HidePlayer();
        loadAnimations();
    }

    // Animacje UI podczas uruchamiania
    async void loadAnimations()
    {

        mainShip.transform.DOLocalMoveY(50, 14);
        Camera.main.gameObject.transform.DOLocalMoveY(0, 3f).OnComplete(() => { Camera.main.GetComponent<CameraScript>().enabled = true; });
        loadBackground.DOColor(Color.clear, 1f).SetEase(Ease.OutSine, 15, 4);

        await Task.Delay(System.TimeSpan.FromSeconds(3));
        UI.canvas.transform.Find("GalaxyText/Galaxy").GetComponent<TMP_Text>().DOColor(Color.clear, 1);
        UI.canvas.transform.Find("GalaxyText/GalaxyName").GetComponent<TMP_Text>().DOColor(Color.clear, 1);
        Destroy(loadBackground.gameObject);
        Game.ShowPlayer();
        complete();

        await Task.Delay(System.TimeSpan.FromSeconds(14));
        foreach (var i in mainShip.GetComponentsInChildren<ParticleSystemRenderer>())
        {
            i.material = mainShipParticleMat;
        }
        mainShip.transform.DOMoveY(500, 2);
        GameObject.Destroy(mainShip, 4);

        // Czasowo

        UI.createNotification("Test notyfikacji1", 1);
        UI.createNotification("Test notyfikacji2" , 5);
        UI.createNotification("Test notyfikacji3", 9);
        UI.createNotification("Test notyfikacji4", 2);
        UI.createNotification("Test notyfikacji5", 5);
        UI.createNotification("Test notyfikacji6");
        UI.createNotification("Test notyfikacji7");
        Invoke("testowe", 3);
    }

    void testowe()
    {
        UI.createNotification("Test notyfikacji5");
        Game.getPlayerInventory().addItem(ItemsDatabase.ItemById(0));
        Game.getPlayerInventory().addItem(ItemsDatabase.ItemById(1));


    }

    async void complete()
    {
        await Task.Delay(System.TimeSpan.FromSeconds(1));
        UI.createNotification("TARGET: EXPLORE THE WORLD");
        UI.createNotification("GO TO THE MAIN SHIP");

        GameObject.Find("Safe").transform.DOLocalMoveY(450, 1);
        GameObject.Find("Safe").GetComponent<RectTransform>().DOSizeDelta(new Vector2(260, 30), 1).SetLoops(3);

        await Task.Delay(System.TimeSpan.FromSeconds(3));
        GameObject.Find("Safe").transform.DOLocalMoveY(1000, 2);
    }
}
