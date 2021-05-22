using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    public string shipName = "F4U1";
    public ShipType shipType = ShipType.Fighter;

    public enum PlayerShipPart { Armor, Fueltank, Boosters, Storage, Engine, Ammunition }
    private static Dictionary<PlayerShipPart, byte> playerParts = new Dictionary<PlayerShipPart, byte>();

    public static void Initialize()
    {
        playerParts.Add(PlayerShipPart.Armor, 0);
        playerParts.Add(PlayerShipPart.Fueltank, 0);
        playerParts.Add(PlayerShipPart.Boosters, 0);
        playerParts.Add(PlayerShipPart.Storage, 0);
        playerParts.Add(PlayerShipPart.Engine, 0);
        playerParts.Add(PlayerShipPart.Ammunition, 0);
    }

    public void UpgradePart(int upgradePart)
    {
        switch ((PlayerShipPart)upgradePart)
        {
            case PlayerShipPart.Armor:
                playerParts[PlayerShipPart.Armor] += 1;
                Game.getPlayer().maxHp += 1;
                break;
            case PlayerShipPart.Fueltank:
                playerParts[PlayerShipPart.Fueltank] += 1;
                break;
            case PlayerShipPart.Boosters:
                playerParts[PlayerShipPart.Boosters] += 1;
                Game.getPlayer().maxSpeed += 1;
                break;
            case PlayerShipPart.Storage:
                playerParts[PlayerShipPart.Storage] += 1;
                break;
            case PlayerShipPart.Engine:
                playerParts[PlayerShipPart.Engine] += 1;
                Game.getPlayer().maxSpeed += 1;
                break;
            case PlayerShipPart.Ammunition:
                playerParts[PlayerShipPart.Ammunition] += 1;
                Game.getPlayer().attackDmg += 1;
                break;
            default:
                break;
        }
    }
}
