using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stratification_Research
{
    public class Program
    {
        public static void Main()
        {
            Researcher r1 = new Researcher(0, 1);
            Researcher r2 = new Researcher(1, 2);
            Researcher r3 = new Researcher(2, 3);
            Researcher r4 = new Researcher(3, 4);
            Researcher r5 = new Researcher(4, 5);
            Researcher r6 = new Researcher(5, 6);

            List<Researcher> researchers = new List<Researcher>(){
                r1, r2, r3, r4, r5, r6
            };

            r1.collaborators = new HashSet<int>() { r3, r4, r5, r6 };
            r2.collaborators = new HashSet<int>() { r1, r4, r5 };
            r3.collaborators = new HashSet<int>() { r2, r4, r5, r6 };
            r4.collaborators = new HashSet<int>() { r1, r2, r3, r5, r6 };
            r5.collaborators = new HashSet<int>() { r1, r2, r3, r6 };
            r6.collaborators = new HashSet<int>() { r1, r3, r4, r5 };

            Dictionary<int, MatchingResearcher> matchingResearchers = researchers.ToDictionary(
                r => r.id,
                r => new MatchingResearcher(r));

            Matching match = new Matching(matchingResearchers);

            matchingResearchers = match.MatchPreferences();

            foreach((int id, MatchingResearcher r) in matchingResearchers)
            {
                Console.WriteLine(String.Format("{0} matched {1}",
                    r.skillScore, 
                    r.preferences?.Count > 0 ? matchingResearchers[r.preferences[0]].skillScore : "Nobody"));
            }
        }
    }
}