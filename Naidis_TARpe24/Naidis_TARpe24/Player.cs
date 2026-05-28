using System;
using System.Collections.Generic;
using System.Text;

namespace Naidis_TARpe24;

public class Player
{
    public string Nimi { get; set; }
    public int Punktid { get; private set; }
    public int Katsed { get; private set; }
    public int OigedVastused { get; private set; }

    public Player(string nimi)
    {
        Nimi = nimi;
        Punktid = 0;
        Katsed = 0;
        OigedVastused = 0;
    }

    public void LisaPunktid(int kogus)
    {
        Punktid += kogus;
        OigedVastused++;
    }

    public void LisaKatse()
    {
        Katsed++;
    }

    public string GetStats()
    {
        return $"Punktid: {Punktid} | Õiged: {OigedVastused} | Katsed: {Katsed}";
    }

    public void Reset()
    {
        Punktid = 0;
        Katsed = 0;
        OigedVastused = 0;
    }
}