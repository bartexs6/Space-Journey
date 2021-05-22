using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

/* ?  */
public class Tween
{
    public async static void notificationAnimation(GameObject notification, int seconds, int id)
    {
        notification.transform.DOLocalMoveX(-700, 1);
        await Task.Delay(System.TimeSpan.FromSeconds(seconds));
        notification.transform.DOLocalMoveX(700, 1);

        await Task.Delay(System.TimeSpan.FromSeconds(1));

        UI.notifications.Insert(id, null);

        GameObject.Destroy(notification);
    }
}
