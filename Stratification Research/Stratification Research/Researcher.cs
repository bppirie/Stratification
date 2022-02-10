using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stratification_Research
{
    public class Researcher
    {
        #region Public Properties
        public int id;
        public int skillScore;
        public HashSet<int>? collaborators;
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

        //public static explicit operator int(Researcher r) => r.id;
        public static implicit operator int(Researcher r) => r.id;

        #endregion
    }

    public class MatchingResearcher : Researcher
    {
        #region Public Properties
        public List<int>? preferences;
        public MatchingResearcher? hold;
        #endregion

        #region Constructors
        public MatchingResearcher(Researcher r)
        {
            this.collaborators = r.collaborators;
            this.skillScore = r.skillScore;
            this.id = r.id;
        }
        #endregion


        #region Public Methods
        public bool ProposeTo(MatchingResearcher r)
        {
            Console.WriteLine(string.Format("{0} proposed to {1}", this.skillScore, r.skillScore));
            if (r == null) return false;
            else if (r.preferences != null && r.preferences.Exists(x => x == this.id) 
                && (r.hold == null || this.skillScore >= r.hold.skillScore))
            {
                return true;
            }
            else 
            {
                return false;
            }
        }       
        #endregion
    }
}
