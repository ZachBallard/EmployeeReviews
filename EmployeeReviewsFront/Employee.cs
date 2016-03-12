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

        public bool IsSatisfactory { get; set; }
        public bool HasReview { get; set; }
        public string Review { get; set; }



        private string NegWordKey1 { get; set; } = @"(takes longer)|(not useful)|(negative)|(sad)|(bad)|(slower)|(unsatisfactory)|(unhappy)
                                                          |(upset)|(messy)|(clumsy)|(interrupt)|(talk over)|(dwell)|(fewer)|(accomidate)|(off topic)|(is rarely)|(not done well)
                                                          |(needs to work)|(needs work)|(difficult)|(could do a better job)|(misusing)|(is not)|(could do more)|(unacceptable)
                                                          |(misses)|(poor)|(inability)|(however)|(However)|(takes longer)";
        private string NegWordKey2 { get; set; } = @"(negative)|(angry)|(apath)|(callous)|(moan)|(whine)|(confus)|(depressed)|(ill)|(dirty)|(fault)|(greed)|(fail)
                                                          |(questionable)|(pain)|(shoddy)|(stupid)|(substandard)|(smelly)|(hate)|(loud)|(complain)|(potential issues)|(late)";
        private string NegWordKey3 { get; set; } = @"(abismal)|(terrifying)|(disgusting)|(gross)|(jealous)|(revolting)|(belligerent)|(foul)|(vindictive)|(lack)
                                                          |(worthless)|(detrimental)|(crazy)|(gross)|(severe)|(scary)|(obnoxious)|(inadequate)|(rude)|(absent)|(alarming)";

        private string PosWordKey1 { get; set; } = @"(good)|(acceptable)|(fine)|(happy)|(nice)|(ok)|(OK)|(friendly)|(quick)|(agreeable)|(charming)|(compet)
                                                          |(kind)|(modest)|(polite)|(smiling)|(teachable)|(enjoy)|(rarely needs)|(consistent)|(has done well)";
        private string PosWordKey2 { get; set; } = @"(positive)|(great)|(friendly)|(pleas)|(honest)|(respectful)|(alert)|(balanced)|(considerate)|(cheerful)
                                                          |(careful)|(willing)|(love)|(optimist)|(youthful)|(responsive)|(hope to)|(valu)|(always willing)
                                                          |(success)|(punctual)";
        private string PosWordKey3 { get; set; } = @"(efficient)|(effective)|(wonderful)|(amazing)|(awesome)|(fantastic)|(delight)|(excellent)|(asset)|(flawless)
                                                          |(flourish)|(innovat)|(ingenuity)|(ideal)|(trustworthy)|(dependable)|(motivated)|(obedient)|(perfect)
                                                          |(incredible)|(devoted)|(incredib)|(impressed)|(devoted)(responsible)|(dependable)|(valuable)|(knowledgeable)";

        public Employee(string n, decimal s, string e, string p)
        {
            Name = n;
            Salary = decimal.Round(s, 2, MidpointRounding.AwayFromZero);
            Email = e;
            PhoneNum = p;

            IsSatisfactory = true;
            HasReview = false;
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
            empScore -= neg2All.Count * 2;
            empScore -= neg3All.Count * 3;

            empScore += pos1All.Count;
            empScore += pos2All.Count * 2;
            empScore += pos3All.Count * 3;

            return empScore >= 0;
        }
    }
}
