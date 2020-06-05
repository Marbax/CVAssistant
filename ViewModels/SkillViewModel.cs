using System.Collections.Generic;

namespace ViewModels
{
    public class SkillViewModel : AViewModel
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public SkillViewModel(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            return obj is SkillViewModel && (obj as SkillViewModel).Name.Equals(this.Name);
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
