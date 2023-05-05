using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using VietJob_V2.Models.Abstracts;

namespace VietJob_V2.Models.Classes
{
    public class Matching : ChatGPTAbstract
    {
        private string cv;
        private string jd;

        public int totalScore;

        public ListOfSkill softskillKeywords = new ListOfSkill();
        public ListOfSkill hardskillKeywords = new ListOfSkill();
        public ListOfSkill otherskillKeywords = new ListOfSkill();

        public string recommendation;

        private readonly OpenAi _openAi = new OpenAi();
        
        public Matching(string _cv, string _jd)
        {
            cv = _cv.ToLower().Trim();
            jd = _jd.ToLower().Trim();
            recommendation = string.Empty;
            totalScore = 0;
            
            AnalyzeCv();
            AnalyzeJd();
            CalculateScore();
        }

        private void AnalyzeJd()
        {
            try
            {
                string request = "list all very short keywords of skills, the number of their occurrences, their required level " +
                                 "(basic, medium, high), requirement group (hard skills,soft skills, other skills) and " +
                                 "short description from only the requirements section of this job description and returns in json format, each object has the following keys (keywords, occurrences, level, group, description): " + jd;
                var result = _openAi.MakeConversation(request).Result;

                var serializer = new JavaScriptSerializer();
                var answer = serializer.Deserialize<List<ChatGPTResponse>>( result);

                foreach (ChatGPTResponse item in answer)
                {
                    AddNewSkillKeyword(item.keywords.ToLower().Trim(), 
                        item.level.ToLower().Trim(),
                        item.description.ToLower().Trim(), 
                        Convert.ToInt16(item.occurrences), 
                        item.group.ToLower().Trim());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void AnalyzeCv()
        {
            
        }

        private void AddNewSkillKeyword (string skill, string qualification, string description, int occurrences, string type)
        {
            switch (type)
            {
                case "soft skills":
                    softskillKeywords.AddSkill(skill, qualification, description, occurrences);
                    break;
                case "hard skills":
                    hardskillKeywords.AddSkill(skill, qualification, description, occurrences);
                    break;
                default:
                    otherskillKeywords.AddSkill(skill, qualification, description, occurrences);
                    break;
            }
        }

        private void CalculateScore ()
        {
            softskillKeywords.CheckSkill(cv);
            otherskillKeywords.CheckSkill(cv);
            hardskillKeywords.CheckSkill(cv);
            
            if ((softskillKeywords.skills.Count + hardskillKeywords.skills.Count + otherskillKeywords.skills.Count) == 0) totalScore = 10;
            else totalScore = (int)((double)(softskillKeywords.skillGood + hardskillKeywords.skillGood + otherskillKeywords.skillGood) 
                / (softskillKeywords.skills.Count + hardskillKeywords.skills.Count + otherskillKeywords.skills.Count) * 100);
        }
    }

    public class Keywords
    {
        public string keyword;
        public string qualification;
        public string description;
        public int occurrences;
        public bool existed;

        public Keywords()
        {
            keyword = string.Empty;
            qualification = "Cơ bản";
            existed = false;
        }
    }

    public class ListOfSkill
    {
        public List<Keywords> skills = new List<Keywords>();
        public int skillGood;
        public int priSkillGood;
        public int score;

        public void AddSkill(string skill, string qualification, string description, int occurrences)
        {
            skills.Add(new Keywords
            {
                keyword = skill,
                qualification = qualification,
                description = description,
                occurrences = occurrences,
                existed = false
            });
        }

        public void CheckSkill(string cv)
        {
            foreach (var skill in skills.Where(skill => cv.Contains(skill.keyword)))
            {
                skill.existed = true;
                skillGood += 1;
            }

            if (skills.Count == 0) score = 0;
            else score = (int)((double)skillGood / skills.Count * 100);
        }
    }

    public class ChatGPTResponse
    {
        public string keywords;
        public string occurrences;
        public string level;
        public string group;
        public string description;
    }
}
