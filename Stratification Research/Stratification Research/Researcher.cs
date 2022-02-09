using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stratification_Research
{
    public class Researcher : EqualityComparer<Researcher>
    {
        #region Public Properties
        public int id;
        public int skillScore;
        public HashSet<Researcher>? collaborators;
        #endregion

        #region Constructors
        public Researcher()           
        {
            skillScore = 0;
            collaborators = null;
        }
        public Researcher(int score)
        {
            skillScore = score;
        }
        public Researcher(int id, int score)
        {
            this.id = id;
            this.skillScore = score;
        }
        #endregion

        #region Public Methods
        public override bool Equals(Researcher r1, Researcher r2)
        {
            return r1.skillScore == r2.skillScore;
        }

        public override int GetHashCode(Researcher r1)
        {
            return r1.skillScore.GetHashCode();
        }
        #endregion
    }

    public class MatchingResearcher : Researcher
    {
        #region Public Properties
        public List<MatchingResearcher>? preferences;
        public MatchingResearcher? hold;
        #endregion

        #region Constructors
        public MatchingResearcher(Researcher r)
        {
            this.collaborators = r.collaborators;
            this.skillScore = r.skillScore;
        }
        #endregion


        #region Public Methods
        public static int MatchingResearcherCompare(MatchingResearcher r1, MatchingResearcher r2)
        {
            return r2.skillScore.CompareTo(r1.skillScore);
        }

        public void BuildPreferences()
        {
            if (collaborators != null)
            {
                preferences = collaborators.ToList().ConvertAll<MatchingResearcher>(r => new MatchingResearcher(r));
                preferences.Sort(MatchingResearcherCompare);
            }
        }

        public bool ProposeTo(MatchingResearcher r)
        {
            Console.WriteLine(string.Format("{0} proposed to {1}", this.skillScore, r.skillScore));
            if (r == null) return false;
            else if (r.preferences != null && r.preferences.Exists(x => x.skillScore == this.skillScore) 
                && (r.hold == null || this.skillScore >= r.hold.skillScore))
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
        
        public void Remove(MatchingResearcher r)
        {
            if(r != null && preferences != null && preferences.Count > 0)
            {
                for(int i = 0; i < preferences.Count; i++)
                {
                    if (preferences[i].skillScore == r.skillScore)
                    {
                        preferences.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        public MatchingResearcher[] RejectLowerScores()
        {
            List<MatchingResearcher> deletes = new List<MatchingResearcher>();

            if (this.preferences != null)
            {
                foreach (MatchingResearcher r in this.preferences)
                {
                    if (this.hold != null && r.skillScore < this.hold.skillScore)
                    {
                        deletes.Add(r);
                    }
                }

                foreach(MatchingResearcher delete in deletes)
                {
                    this.preferences.Remove(delete);
                }
            }
            return deletes.ToArray();
        }
        
        #endregion
    }
}
