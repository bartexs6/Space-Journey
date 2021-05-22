using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShipType { Fighter, Healing, Transporter }

/* Statki */
abstract class SpaceShip : MonoBehaviour
{
    /* Abstrakcyjna klasa
     * statków kosmicznych.
     * Pozwala na stworzenie
     * klasy na podstawie tej
     * abstrakcyjnej klasy */

    protected abstract int hp { get; set; }
    public abstract int maxHp { get; set; }
    protected abstract float speed { get; set; }
    public abstract int maxSpeed { get; set; }

    public abstract int getHp();
    public abstract float getSpeed();

}
