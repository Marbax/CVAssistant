using System.Collections.Generic;

namespace ViewModels
{
    public class Responsobility : AViewModel
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public Responsobility(string name)
        {
            Name = name;
        }

        public override string ToString() => Name.ToString();

        public override bool Equals(object obj) => obj is Responsobility && (obj as Responsobility).Name.Equals(this.Name);

        public override int GetHashCode() => 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
    }

}
