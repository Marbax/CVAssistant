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

        private IEnumerable<string> _skills;
        public IEnumerable<string> Skills
        {
            get { return _skills; }
            set { _skills = value; }
        }

        private IEnumerable<Experience> _prevExp;
        public IEnumerable<Experience> PreviousExperience
        {
            get { return _prevExp; }
            set { _prevExp = value; }
        }

        public CurriculumVitae(Person person, IEnumerable<string> skills, IEnumerable<Experience> previousExperience)
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
