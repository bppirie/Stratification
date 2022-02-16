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
            Researcher r7 = new Researcher(6, 7);
            Researcher r8 = new Researcher(7, 8);
            Researcher r9 = new Researcher(8, 9);
            Researcher r10 = new Researcher(9, 10);
            Researcher r11 = new Researcher(10, 11);
            Researcher r12 = new Researcher(11, 12);
            Researcher r13 = new Researcher(12, 13);
            Researcher r14 = new Researcher(13, 14);
            Researcher r15 = new Researcher(14, 15);
            Researcher r16 = new Researcher(15, 16);
            Researcher r17 = new Researcher(16, 17);


            List<Researcher> researchers = new List<Researcher>(){
                r1, r2, r3, r4, r5, r6, r7, r8, r9, r10, r11, r12, r13, r14, r15, r16, r17
            };

            /*r1.collaborators = new HashSet<int>() { r3, r4, r5, r6 };
            r2.collaborators = new HashSet<int>() { r1, r4, r5 };
            r3.collaborators = new HashSet<int>() { r2, r4, r5, r6 };
            r4.collaborators = new HashSet<int>() { r1, r2, r3, r5, r6 };
            r5.collaborators = new HashSet<int>() { r1, r2, r3, r6 };
            r6.collaborators = new HashSet<int>() { r1, r3, r4, r5 };*/

            r1.collaborators = new HashSet<int> { r2, r3, r4, r6, r7, r8, r10, r11, r13, r16, r17 };
            r2.collaborators = new HashSet<int> { r1, r4, r6, r7, r8, r13, r14, r17 };
            r3.collaborators = new HashSet<int> { r1, r4, r5, r6, r7, r8, r9, r10, r9, r12, r13, r15, r17 };
            r4.collaborators = new HashSet<int> { r1, r2, r3, r5, r6, r7, r8, r10, r11, r12, r13, r14, r15, r16, r17 };
            r5.collaborators = new HashSet<int> { r3, r4, r6, r8, r9, r10, r12, r13, r15, r17 };
            r6.collaborators = new HashSet<int> { r1, r2, r3, r4, r5, r7, r8, r10, r11, r13, r14, r15, r16, r17 };
            r7.collaborators = new HashSet<int> { r1, r2, r3, r4, r6, r8, r10, r11, r13, r14, r16, r17 };
            r8.collaborators = new HashSet<int> { r1, r2, r3, r4, r5, r6, r7, r9, r10, r11, r12, r13, r15, r16, r17 };
            r9.collaborators = new HashSet<int> { r3, r5, r8, r12, r15, r17 };
            r10.collaborators = new HashSet<int> { r1, r3, r4, r5, r6, r7, r8, r9, r12, r13, r15, r17 };
            r11.collaborators = new HashSet<int> { r1, r4, r6, r7, r8, r13, r16, r17 };
            r12.collaborators = new HashSet<int> { r3, r4, r5, r8, r9, r10, r15, r17 };
            r13.collaborators = new HashSet<int> { r1, r2, r3, r4, r5, r6, r7, r8, r10, r11, r14, r15, r16, r17 };
            r14.collaborators = new HashSet<int> { r2, r4, r6, r7, r13, r17 };
            r15.collaborators = new HashSet<int> { r3, r4, r5, r6, r8, r9, r10, r12, r13, r17 };
            r16.collaborators = new HashSet<int> { r1, r4, r6, r7, r8, r11, r13, r17 };
            r17.collaborators = new HashSet<int> { r1, r2, r3, r4, r5, r6, r7, r8, r9, r10, r11, r12, r13, r14, r15, r16 };

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