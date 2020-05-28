using Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace ViewModels
{
    public class ExperienceViewModel : AViewModel
    {
        private Experience _experience;
        public Experience Experience
        {
            get { return _experience; }
            set { SetProperty(ref _experience, value); }
        }


        public string Company
        {
            get { return Experience.Company; }
            set
            {
                if (value != null && !Experience.Company.Equals(value))
                {
                    Experience.Company = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Company)));
                }
            }
        }

        public string Position
        {
            get { return Experience.Position; }
            set
            {
                if (value != null && !Experience.Position.Equals(value))
                {
                    Experience.Position = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Position)));
                }
            }
        }

        public System.DateTime StartDate
        {
            get { return Experience.StartDate; }
            set
            {
                if (!Experience.StartDate.Equals(value))
                {
                    Experience.StartDate = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(StartDate)));
                }
            }
        }

        public System.DateTime EndDate
        {
            get { return Experience.EndDate; }
            set
            {
                if (!Experience.EndDate.Equals(value))
                {
                    Experience.EndDate = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(EndDate)));
                }
            }
        }

        private ObservableCollection<Responsobility> _r14s = new ObservableCollection<Responsobility>();
        public ObservableCollection<Responsobility> Responsibilities
        {
            get { return _r14s; }
            set { SetProperty(ref _r14s, value); }
        }


        public ExperienceViewModel(Experience experience)
        {
            Experience = experience;
            experience.Responsibilities.ToList().ForEach(i => Responsibilities.Add(new Responsobility(i)));
        }


        public void UpdateSource()
        {
            List<string> tmpResponsobilities = new List<string>();
            Responsibilities.ToList().ForEach(i => tmpResponsobilities.Add(i.Name));
            Experience.Responsibilities = tmpResponsobilities;
        }


    }

}
