using System.Collections.Generic;

namespace ViewModels
{
    public class Skill : AViewModel
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public Skill(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            return obj is Skill && (obj as Skill).Name.Equals(this.Name);
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

        public override string ToString()
        {
            return Name.ToString();
        }
    }

}
