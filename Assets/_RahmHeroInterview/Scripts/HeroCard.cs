using TMPro;
using UnityEngine;

namespace RahmHeroInterview
{
    public class HeroCard : MonoBehaviour
    {
        // Id of the hero (beginning with 1)
        [SerializeField] public int heroId;

        // Text fields of hero card
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI keywordText;
        [SerializeField] private TextMeshProUGUI descriptionText;

        private HeroSelection heroSelection;


        void Start()
        {

        }

        public void SetHeroID(int heroId)
        {
            this.heroId = heroId;
        }

        public void SetNameText(string name)
        {
            this.nameText.text = name;
        }

        public void SetKeywordText(string keyword)
        {
            this.keywordText.text = keyword;
        }

        public void SetDescriptionText(string description)
        {
            this.descriptionText.text = description;
        }

        public void SetHeroSelection(HeroSelection heroSelection)
        {
            this.heroSelection = heroSelection;
        }
    }
}
