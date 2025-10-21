using System;
using UnityEngine.Video;

namespace RahmHeroInterview
{
    [Serializable]
    public struct Topic
    {
        public int Id;
        public string Name;
        public VideoClip Clip;
        
        public Topic(int id, string name, VideoClip clip)
        {
            this.Id = id;    
            this.Name = name;
            this.Clip = clip;
        }
    }
    
    [Serializable]
    public struct TopicData
    {
        public int Id;
        public string Name;
    }

    [Serializable]
    public struct RahmHero
    {
        public int Id;
        public string Name;
        public string Keyword;
        public string Description;

        public VideoClip FaceIconClip;
        public VideoClip IdleClip;
        public Topic[] Topics;


        public RahmHero(int id, string name, string keyword, string description, VideoClip faceIconClip,
            VideoClip idleClip, Topic[] topics)
        {
            this.Id = id;
            this.Name = name;
            this.Keyword = keyword;
            this.Description = description;
            this.FaceIconClip = faceIconClip;
            this.IdleClip = idleClip;
            this.Topics = topics;
        }
    }

    [Serializable]
    public struct RahmHeroData
    {
        public int Id;
        public string Name;
        public string Keyword;
        public string Description;
        
        public TopicData[] TopicData;
    }
}