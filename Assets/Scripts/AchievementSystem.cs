using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementSystem : MonoBehaviour
{
    public static AchievementSystem instance;

    [SerializeField] private int totalInimigos; // Total de inimigos no jogo
    [SerializeField] private int totalCristais; // Total de cristais no jogo

    [SerializeField] private int inimigosDerrotados = 0; // Contador de inimigos derrotados
    [SerializeField] private int cristaisColetados = 0; // Contador de cristais coletados

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void SetTotals(int inimigos, int cristais)
    {
        totalInimigos = inimigos;
        totalCristais = cristais;
    }

    public void EnemyDefeated()
    {
        inimigosDerrotados++;
        if (inimigosDerrotados >= totalInimigos)
        {
            NotifyAchievement("Conquista: Todos os inimigos derrotados!");
        }
    }

    public void CristalCollected()
    {
        cristaisColetados++;
        if (cristaisColetados >= totalCristais)
        {
            NotifyAchievement("Conquista: Todos os cristais coletados!");
        }
    }
    /*
    private void CheckAchievements()
    {
        if (inimigosDerrotados >= totalInimigos)
        {
            NotifyAchievement("Conquista: Todos os inimigos derrotados!");
        }

        if (cristaisColetados >= totalCristais)
        {
            NotifyAchievement("Conquista: Todos os cristais coletados!");
        }
    }
    */

    private void NotifyAchievement(string message)
    {
        Debug.Log(message); 
        UIController.instance.ShowAchievementMessage(message); 
    }
}
