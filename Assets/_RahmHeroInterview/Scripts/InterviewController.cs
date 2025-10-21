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

        
        void Start()
        {
            // Start with hero selection
            ShowHeroSelection();
        }
        
        public void SelectHero(RahmHeroData heroData)
        {
            this.selectedHeroData = heroData;
            
            ShowTopicSelection(heroData);
        }
        
        public void ShowHeroSelection()
        {
            // Show hero selection
            heroSelection.gameObject.SetActive(true);
            topicSelection.gameObject.SetActive(false);
            // TODO: Subtopic selection
            
            // TODO: Start heroCard rotation
        }
        
        private void ShowTopicSelection(RahmHeroData heroData)
        {
            // Generate topic buttons based on selected hero
            topicSelection.SetupTopicButtons(heroData);
            
            heroSelection.gameObject.SetActive(false);
            topicSelection.gameObject.SetActive(true);
        }
    }
}
