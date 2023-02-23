using System.Collections.Generic;
using UnityEngine;

public class BoatSprites : MonoBehaviour
{
    public static BoatSprites Instance { get; private set; }
    
    [Header("Player Sprites")]
    [SerializeField] private Sprite playerFine;
    [SerializeField] private Sprite playerDamaged;
    [SerializeField] private Sprite playerBreaking;
    [SerializeField] private Sprite playerDead;

    [Header("Shooter Sprites")]
    [SerializeField] private Sprite shooterFine;
    [SerializeField] private Sprite shooterDamaged;
    [SerializeField] private Sprite shooterBreaking;
    [SerializeField] private Sprite shooterDead;

    [Header("Chaser Sprites")]
    [SerializeField] private Sprite chaserFine;
    [SerializeField] private Sprite chaserDamaged;
    [SerializeField] private Sprite chaserBreaking;
    [SerializeField] private Sprite chaserDead;

    public Dictionary<BoatType, Dictionary<BoatStates, Sprite>> BoatSpriteList;

    private void Awake()
    {
        #region Singleton
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
        #endregion

        #region Sprite Dictionary
        BoatSpriteList = new Dictionary<BoatType, Dictionary<BoatStates, Sprite>>()
        {
            { BoatType.PLAYER , new Dictionary<BoatStates, Sprite>()
                {
                    {BoatStates.FINE, playerFine},
                    {BoatStates.DAMAGED , playerDamaged},
                    {BoatStates.BREAKING , playerBreaking},
                    {BoatStates.DEAD , playerDead}
                }
            },
            { BoatType.SHOOTER , new Dictionary<BoatStates, Sprite>()
                {
                    {BoatStates.FINE, shooterFine},
                    {BoatStates.DAMAGED , shooterDamaged},
                    {BoatStates.BREAKING , shooterBreaking},
                    {BoatStates.DEAD , shooterDead}
                }
            },
            { BoatType.CHASER , new Dictionary<BoatStates, Sprite>()
                {
                    {BoatStates.FINE, chaserFine},
                    {BoatStates.DAMAGED , chaserDamaged},
                    {BoatStates.BREAKING , chaserBreaking},
                    {BoatStates.DEAD , chaserDead}
                }
            }
        };
        #endregion
    }
    
}
