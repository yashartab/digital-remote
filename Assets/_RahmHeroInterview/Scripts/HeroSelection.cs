using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RahmHeroInterview
{
    public class HeroSelection : MonoBehaviour
    {
        [SerializeField] private MsgHandler msgHandler;
        
        // Parent transform of hero cards
        [SerializeField] private Transform heroCardsParent;
        // Prefab for hero card
        [SerializeField] private GameObject heroCardPrefab;
        
        // List of all rahm heroes
        [SerializeField] private List<RahmHeroData> rahmHeroData = new List<RahmHeroData>();
        
        // List of all hero cards
        private List<HeroCard> heroCards = new List<HeroCard>();
        
        
        void Update()
        {
            // TODO: Automatic hero rotation on message from server (when video changes)
        }

        public void InitHeroSelection(List<RahmHeroData> heroData)
        {
            // Save current rahm hero data
            rahmHeroData = heroData;
            
            // Generate the hero cards
            foreach (RahmHeroData hero in heroData)
            {
                // Generate a new button for the hero
                GameObject newButton = Instantiate(heroCardPrefab, heroCardsParent);
                HeroCard heroCard = newButton.GetComponent<HeroCard>();
                heroCard.SetHeroSelection(this);

                // Add hero card to list
                heroCards.Add(heroCard);
                
                // Set hero card content
                heroCard.SetHeroID(hero.Id);
                heroCard.SetNameText(hero.Name);
                heroCard.SetKeywordText(hero.Keyword);
                heroCard.SetDescriptionText(hero.Description);

                // Add onClick event to the button which selects the hero
                heroCard.GetComponent<Button>().onClick.AddListener(() => { this.OnSelectHero(hero); });

            }
        }
        
        public void OnSelectHero(RahmHeroData hero)
        {
            // TODO: Send message to server to select the hero
            // TODO: On reply change UI to topic selection
            
            msgHandler.OnSelectHero(hero.Id);
        }

        public List<RahmHeroData> GetHeroData()
        {
            return rahmHeroData;
        }

        public RahmHeroData GetHeroDataByID(int heroID)
        {
            if (heroID > 0 && heroID <= rahmHeroData.Count)
            {
                return rahmHeroData[heroID - 1];
            }
            
            Debug.LogError("HeroID " + heroID + " is out of range.");
            return rahmHeroData[0];
        }
    }
}
