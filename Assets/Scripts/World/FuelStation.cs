using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelStation : MonoBehaviour, IInteractible
{
    private float FuelSupply = 2000;
    private float FuelToGive;
    [SerializeField]
    private Player pl;

    private void Start()
    {
        pl = Game.getPlayer();
    }


    public void AddFuel(int fuel)
    {
        pl.tankVolume += fuel;
        pl.UpdateTankStatus();
    }

    public void Interact()
    {
        if(pl.TankStatus())
        {
            AddFuel();
        }      
    }

    void AddFuel()
    {
        if(pl.tankVolume + FuelSupply <= pl.maxVolume)
        {
            FuelToGive = FuelSupply;
        }
        else
        {
            FuelToGive = pl.maxVolume - pl.tankVolume;
        }
        pl.tankVolume += FuelSupply;
        FuelSupply -= FuelToGive;

        Debug.Log("TAnkowanie");

        pl.UpdateTankStatus();
    }
}
