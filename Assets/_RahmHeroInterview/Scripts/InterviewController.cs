using UnityEngine;

namespace RahmHeroInterview
{
    public class InterviewController : MonoBehaviour
    {
        [SerializeField] private HeroSelection heroSelection;
        [SerializeField] private TopicSelection topicSelection;

        // The current selected hero data
        private RahmHeroData selectedHeroData;
        // The current selected topic data
        private TopicData selectedTopicData;

        public void SelectHero(RahmHeroData heroData)
        {
            this.selectedHeroData = heroData;
            
            ShowTopicSelection(heroData);
        }
        
        private void ShowTopicSelection(RahmHeroData heroData)
        {
            heroSelection.gameObject.SetActive(false);
            topicSelection.gameObject.SetActive(true);
            
            // TODO: Topic selection based on selected hero
        }
    }
}
