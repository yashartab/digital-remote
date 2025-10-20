using System;
using System.Collections.Generic;
using UnityEngine;

namespace RahmHeroInterview
{
    public class HeroSelection : MonoBehaviour
    {
        [SerializeField] private MsgHandler msgHandler;
            
        // private void Awake()
        // {
        //     msgHandler.OnInitHeroSelection();
        // }
        
        void Update()
        {

        }

        public void InitHeroSelection(List<RahmHeroData> heroData)
        {
            foreach (RahmHeroData hero in heroData)
            {
                Debug.Log(hero.Id);
            }
        }
    }
}
