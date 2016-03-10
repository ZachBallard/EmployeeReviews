using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmployeeReviewsFront
{
    public class Employee
    {
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public string Email { get; set; }
        public string PhoneNum { get; set; }

        public string ReviewLocation { get; set; }
        public string Review { get; set; }

        public bool IsSatisfactory { get; set; }

        public string NegWordKey1 { get; private set; } = @"(takes longer)|(not useful)|(lack)|(negative)|(sad)|(bad)|(slower)|(unsatisfactory)|(unhappy)
                                                          |(upset)|(messy)|(clumsy)|(interrupt)|(talk over)|(dwell)|(fewer)|(accomidate)|(off topic)|(is rarely)
                                                          |(not done well)";
        public string NegWordKey2 { get; private set; } = @"(negative)|(angry)|(apath)|(callous)|(moan)|(whine)|(confus)|(depressed)|(ill)|(dirty)|(fault)|(greed)
                                                          |(questionable)|(pain)|(shoddy)|(stupid)|(substandard)|(smelly)|(hate)|(loud)|(complain)|(potential issues)";
        public string NegWordKey3 { get; private set; } = @"(abismal)|(terrifying)|(disgusting)|(gross)|(jealous)|(revolting)|(belligerent)|(foul)|(vindictive)
                                                          |(worthless)|(detrimental)|(crazy)|(gross)|(severe)|(scary)|(obnoxious)|(inadequate)|(rude)";

        public string PosWordKey1 { get; private set; } = @"(good)|(acceptable)|(fine)|(happy)|(nice)|(ok)|(OK)|(friendly)|(quick)|(agreeable)|(charming)|(compet)
                                                          |(kind)|(modest)|(polite)|(smiling)|(teachable)|(enjoy)|(rarely needs)|(consistent)|(has done well)";
        public string PosWordKey2 { get; private set; } = @"(positive)|(great)|(friendly)|(pleas)|(honest)|(respectful)|(alert)|(balanced)|(considerate)|(cheerful)
                                                          |(careful)|(willing)|(love)|(optimist)|(valuable)|(youthful)|(responsive)|(hope to)|(valuable)
                                                          |(always willing)|(success)";
        public string PosWordKey3 { get; private set; } = @"(efficient)|(effective)|(wonderful)|(amazing)|(awesome)|(fantastic)|(delight)|(excellent)|(asset)|(flawless)
                                                          |(flourish)|(innovat)|(ingenuity)|(ideal)|(trustworthy)|(dependable)|(motivated)|(obedient)|(perfect)
                                                          |(incredible)|(devoted)|(incredib)|(impressed)|(devoted)";

        public Employee(string n, decimal s, string e, string p)
        {
            Name = n;
            Salary = s;
            Email = e;
            PhoneNum = p;
        }

        public void AddReview(string rl)
        {
            ReviewLocation = rl;
        }

        public void GetReview()
        {
            Review = System.IO.File.ReadAllText(ReviewLocation);
        }

        public bool Evaluate()
        {
            int empScore = 0;

            var neg1 = new Regex(NegWordKey1);
            var neg2 = new Regex(NegWordKey2);
            var neg3 = new Regex(NegWordKey3);

            var pos1 = new Regex(PosWordKey1);
            var pos2 = new Regex(PosWordKey2);
            var pos3 = new Regex(PosWordKey3);

            MatchCollection neg1All = neg1.Matches(Review);
            MatchCollection neg2All = neg2.Matches(Review);
            MatchCollection neg3All = neg3.Matches(Review);

            MatchCollection pos1All = pos1.Matches(Review);
            MatchCollection pos2All = pos2.Matches(Review);
            MatchCollection pos3All = pos3.Matches(Review);

            empScore -= neg1All.Count;
            empScore -= neg1All.Count * 2;
            empScore -= neg1All.Count * 3;

            empScore += neg1All.Count;
            empScore += neg1All.Count * 2;
            empScore += neg1All.Count * 3;

            return empScore >= 0;
        }
    }
}
