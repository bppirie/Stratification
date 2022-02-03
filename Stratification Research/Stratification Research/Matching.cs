using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stratification_Research
{
    public class Matching
    {
        public static bool VerifyInitialMatching(MatchingResearcher[] researchers)
        {
            if (researchers != null && researchers.Length > 0)
            {
                bool result = true;

                foreach(MatchingResearcher r in researchers)
                {
                    if (r.hold == null && r.preferences != null && r.preferences.Count != 0)
                    {
                        result = false;
                        break;
                    }
                }

                return result;
            }

            return false;
        }
        
        public static void MatchPreferences(MatchingResearcher[] researchers)
        {
            if (researchers != null && researchers.Length > 0)
            {
                foreach (MatchingResearcher r in researchers)
                {
                    r.BuildPreferences();
                }

                int index = 0;

                while (!VerifyInitialMatching(researchers))
                {
                    MatchingResearcher r = researchers[index];
                    if (r.preferences != null)
                    {
                        MatchingResearcher fav = r.preferences[0];
                        int favIndex = 0;
                        for (int i = 0; i < researchers.Length; i++)
                        {
                            if (researchers[i].skillScore == fav.skillScore)
                            {
                                fav = researchers[i];
                                favIndex = i;
                                break;
                            }
                        }
                        
                        if (r.ProposeTo(fav))
                        {
                            fav.hold = r;
                            MatchingResearcher[] deletes = fav.RejectLowerScores();

                            if (deletes != null && deletes.Length > 0)
                            {
                                for (int i = 0; i < deletes.Length; i++)
                                {
                                    for (int j = 0; j < researchers.Length; j++)
                                    {
                                        if (deletes[i].skillScore == researchers[j].skillScore)
                                        {
                                            researchers[j].Remove(fav);
                                            break;
                                        }
                                    }
                                }
                            }

                            researchers[favIndex] = fav;
                        }

                        researchers[index] = r;
                    }

                    index = (index == researchers.Length - 1 ? 0 : index + 1);
                }

                if (IsMaximumStable(researchers))
                {
                    return;
                }
            }
        }

        // Just gotta check
        public static bool IsMaximumStable(MatchingResearcher[] researchers)
        {
            bool result = true;

            if (researchers != null && researchers.Length > 0)
            {
                foreach(MatchingResearcher r in researchers)
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
    }
}
