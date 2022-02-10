using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stratification_Research
{
    public class Matching
    { 
        #region Public Properties

        Dictionary<int, MatchingResearcher>? researchers;

        #endregion

        #region Constructors

        public Matching()
        {
            researchers = null;
        }

        public Matching(Dictionary<int, MatchingResearcher> x)
        {
            researchers = x;
        }

        #endregion

        #region Phase 1 Methods
        public int MatchingResearcherCompare(int r1, int r2)
        {
            if (researchers == null) return -1;

            MatchingResearcher first = researchers[r1];
            MatchingResearcher second = researchers[r2];

            int x1 = first.skillScore, x2 = second.skillScore;
            return x2.CompareTo(x1);
        }

        public void BuildPreferences(int id)
        {
            if (researchers == null || !researchers.ContainsKey(id)) return;

            MatchingResearcher r = researchers[id];
            if (r.collaborators != null)
            {
                r.preferences = r.collaborators.ToList();
                r.preferences.Sort(MatchingResearcherCompare);
            }
            researchers[id] = r;
        }

        public bool VerifyInitialMatching()
        {
            if (researchers == null || researchers.Count <= 0) return false;

            bool result = true;

            foreach ((int researcherID, MatchingResearcher r) in researchers)
            {
                if (r.hold == null && r.preferences != null && r.preferences.Count != 0)
                {
                    result = false;
                    break;
                }
            }

                return result;
        }

        public MatchingResearcher RejectLowerScores(MatchingResearcher fav)
        {
            if (researchers == null || fav.preferences == null) return fav;

            fav.preferences.RemoveAll(id =>
            {
                MatchingResearcher r = researchers[id];
                if (r.skillScore < fav.hold?.skillScore)
                {
                    r.preferences?.Remove(fav.id);
                    researchers[id] = r;
                    return true;
                }
                else
                {
                    return false;
                }
            });

            return fav;
        }

        public bool IsMaximumStable()
        {
            bool result = true;

            if (researchers != null && researchers.Count > 0)
            {
                foreach (MatchingResearcher r in researchers.Values)
                {
                    if (r.preferences != null && r.preferences.Count > 1)
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        #endregion

        #region Phase 2 Methods



        #endregion

        public Dictionary<int, MatchingResearcher> MatchPreferences()
        {
            if (researchers == null || researchers.Count <= 0) return researchers;

            #region Phase 1

            foreach ((int researcherID, MatchingResearcher r) in researchers)
            {
                BuildPreferences(researcherID);
            }

            List<int> ids = researchers.Keys.ToList();
            int index = 0;

            while (!VerifyInitialMatching())
            {
                int id = ids[index];
                MatchingResearcher r = researchers[id];

                if (r.preferences == null || r.preferences.Count <= 0) return researchers;
                    
                int favID = r.preferences[0];
                MatchingResearcher fav = researchers[favID];

                if (r.ProposeTo(fav))
                {
                    fav.hold = r;
                    fav = RejectLowerScores(fav);
                    researchers[favID] = fav;
                }
                else
                {
                    Console.WriteLine("Reject");
                    r.preferences.Remove(fav.id);
                }

                researchers[index] = r;
                index = (index == researchers.Count - 1 ? 0 : index + 1);
            }

            #endregion

            if (IsMaximumStable())
            {
                return researchers;
            }

            #region Phase 2

            // Should only get to this phase w relative preferences
            while (true)
            {
                Console.WriteLine("True");
            }

            #endregion

            return researchers;
        }
    }
}
