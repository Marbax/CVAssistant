using DotLiquid;
using System.Collections.Generic;

namespace Models
{
    public class CurriculumVitae : ILiquidizable
    {
        private Person _person;

        public Person Person
        {
            get { return _person; }
            set { _person = value; }
        }

        private List<string> _skills;

        public List<string> Skills
        {
            get { return _skills; }
            set { _skills = value; }
        }

        private List<Experience> _prevExp;


        public List<Experience> PreviousExperience
        {
            get { return _prevExp; }
            set { _prevExp = value; }
        }

        public CurriculumVitae(Person person, List<string> skills, List<Experience> previousExperience)
        {
            Person = person;
            Skills = skills;
            PreviousExperience = previousExperience;
        }

        public object ToLiquid()
        {
            return new
            {
                Person,
                Skills,
                PreviousExperience
            };
        }
    }
}
