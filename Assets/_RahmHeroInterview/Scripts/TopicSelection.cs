using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RahmHeroInterview
{
    public class TopicSelection : MonoBehaviour
    {
        [SerializeField] private MsgHandler msgHandler;
        
        [SerializeField] private InterviewController interviewController;
        [SerializeField] private HeroSelection heroSelection;

        // Parent transform of topic buttons
        [SerializeField] private Transform topicButtonsParent;
        // Prefab for topic button
        [SerializeField] private GameObject topicButtonPrefab;
        
        // List of all topic buttons
        private List<Button> topicButtons = new List<Button>();
        
        
        public void SetupTopicButtons(RahmHeroData heroData)
        {
            // Destroy previous topic buttons
            foreach (Transform child in topicButtonsParent)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < heroData.TopicData.Length; i++)
            {
                // Generate a new button for the topic
                GameObject newButton = Instantiate(topicButtonPrefab, topicButtonsParent);
                newButton.GetComponentInChildren<TextMeshProUGUI>().text = heroData.TopicData[i].Name;
                topicButtons.Add(newButton.GetComponent<Button>());

                // Add onClick event to the button which selects the topic
                TopicData topicData = heroData.TopicData[i];
                newButton.GetComponent<Button>().onClick.AddListener(() => { this.OnSelectTopic(topicData); });
            }
        }
        
        public void OnSelectTopic(TopicData topicData)
        {
            // TODO: Send message to server to select the topic
            // TODO: On reply 
            
            msgHandler.OnSelectTopic(topicData.Id);
        }
    }
}
