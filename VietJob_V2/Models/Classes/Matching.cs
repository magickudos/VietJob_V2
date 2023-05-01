using System.Collections.Generic;
using System.Linq;
using VietJob_V2.Models.Abstracts;

namespace VietJob_V2.Models.Classes
{
    public class Matching : ChatGPTAbstract
    {
        public string cv;

        public int softskillScore;
        public int hardskillScore;
        public int otherskillScore;
        public int totalScore;

        public int softskillGood;
        public int hardskillGood;
        public int otherskillGood;

        public List<string> softskillKeywords = new List<string>();
        public List<string> hardskillKeywords = new List<string>();
        public List<string> otherskillKeywords = new List<string>();

        public string recommendation;

        public Matching(string curriculumVitae)
        {
            cv = curriculumVitae.ToLower().Trim();

            recommendation = string.Empty;

            softskillScore = 0;
            hardskillScore = 0;
            otherskillScore = 0;
            totalScore = 0;
            softskillGood = 0;
            hardskillGood = 0;
            otherskillGood = 0;

            softskillKeywords.Clear();
            hardskillKeywords.Clear();
            otherskillKeywords.Clear();
        }

        public void addNewSoftskillKeyword (string skill)
        {
            softskillKeywords.Add (skill.ToLower());
        }

        public void addNewHardskillKeyword (string skill)
        {
            hardskillKeywords.Add (skill.ToLower());
        }

        public void addNewOtherskillKeyword (string skill)
        {
            otherskillKeywords.Add (skill.ToLower());
        }

        public void caculateSoftskillScore ()
        {
            softskillGood = 0;

            foreach (string item in softskillKeywords)
            {
                if (cv.Contains (item))
                {
                    softskillGood += 1;
                }
            }

            softskillScore = softskillGood / softskillKeywords.Count * 100;
        }

        public void caculateHardskillScore()
        {
            hardskillGood = 0;

            foreach (string item in hardskillKeywords)
            {
                if (cv.Contains(item))
                {
                    hardskillGood += 1;
                }
            }

            hardskillScore = hardskillGood / hardskillKeywords.Count * 100;
        }

        public void caculateOtherskillScore()
        {
            otherskillGood = 0;

            foreach (string item in otherskillKeywords)
            {
                if (cv.Contains(item))
                {
                    otherskillGood += 1;
                }
            }

            otherskillScore = otherskillGood / otherskillKeywords.Count * 100;
        }

        public void caculateTotalScore()
        {
            totalScore = (softskillGood + hardskillGood + otherskillGood) / (softskillKeywords.Count() + hardskillKeywords.Count() + otherskillKeywords.Count()) * 100;
        }
    }
}
