using System.Collections.Generic;

namespace Models
{
	public class CurriculumVitae
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

		private List<string> _prevExp;

		public List<string> PreviousExperience
		{
			get { return _prevExp; }
			set { _prevExp = value; }
		}


	}
}
