using Commands;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ViewModels
{
    public class CurriculumVitaeViewModel : AViewModel
    {
        private CurriculumVitae _curriculumVitae;
        public CurriculumVitae CurriculumVitae
        {
            get => _curriculumVitae;
            private set => SetProperty(ref _curriculumVitae, value);
        }

        #region Commands
        private readonly RelayCommand _addSkill;
        public ICommand AddSkill => _addSkill;

        private readonly RelayCommand _removeSkill;
        public ICommand RemoveSkill => _removeSkill;

        private readonly RelayCommand _addExperience;
        public ICommand AddExperience => _addExperience;

        private readonly RelayCommand _removeExperience;
        public ICommand RemoveExperience => _removeExperience;

        private readonly RelayCommand _addResponsobility;
        public ICommand AddResponsobility => _addResponsobility;

        private readonly RelayCommand _removeResponsobility;
        public ICommand RemoveResponsobility => _removeResponsobility;

        #endregion


        // that works to and were bindable , wtf
        //public Person Person
        //{
        //    get { return _curriculumVitae.Person; }
        //    set
        //    {
        //        if (_curriculumVitae.Person == null || !_curriculumVitae.Person.Equals(value))
        //        {
        //            _curriculumVitae.Person = value;
        //            OnPropertyChanged(new PropertyChangedEventArgs(nameof(_curriculumVitae.Person)));
        //        }
        //    }
        //}

        private PersonViewModel _person;
        public PersonViewModel Person
        {
            get { return _person; }
            set { SetProperty(ref _person, value); }
        }

        private ObservableCollection<SkillViewModel> _skills = new ObservableCollection<SkillViewModel>();
        public ObservableCollection<SkillViewModel> Skills
        {
            get { return _skills; }
            set { SetProperty(ref _skills, value); }
        }

        private ObservableCollection<ExperienceViewModel> _preveousExpertience = new ObservableCollection<ExperienceViewModel>();
        public ObservableCollection<ExperienceViewModel> PreviousExperience
        {
            get { return _preveousExpertience; }
            set { SetProperty(ref _preveousExpertience, value); }
        }

        public CurriculumVitaeViewModel(CurriculumVitae curriculumVitae)
        {
            _curriculumVitae = curriculumVitae;
            Person = new PersonViewModel(curriculumVitae.Person);
            _curriculumVitae.Skills.ToList().ForEach(i => Skills.Add(new SkillViewModel(i)));
            _curriculumVitae.PreviousExperience.ToList().ForEach(i => PreviousExperience.Add(new ExperienceViewModel(i)));

            #region Commands Init
            _addSkill = new RelayCommand(AddSkillToSkills, CanAddSkillToSkills);
            _removeSkill = new RelayCommand(RemoveSkillFromSkills, CanRemoveSkillFromSkills);
            _addExperience = new RelayCommand(AddExpToPreviousExperience);
            _removeExperience = new RelayCommand(RemoveExpFromPreviousExperience, CanRemoveExpFromPreviousExperience);
            _addResponsobility = new RelayCommand(AddResponsobilityToCerteinExperience, CanAddResponsobilityToCerteinExperience);
            _removeResponsobility = new RelayCommand(RemoveResponsobilityFromCerteinExperience, CanRemoveResponsobilityToCerteinExperience);
            #endregion

        }

        /// <summary>
        /// Temporary methods for update collections ('coz have problems with ObservableCollections)
        /// </summary>
        public void UpdateSource()
        {
            List<string> tmpSkills = new List<string>();
            Skills.ToList().ForEach(i => tmpSkills.Add(i.Name));
            CurriculumVitae.Skills = tmpSkills;

            List<Experience> tmpExp = new List<Experience>();

            // BE WARE
            PreviousExperience.ToList().ForEach(i => { i.UpdateSource(); tmpExp.Add(i.Experience); });
            CurriculumVitae.PreviousExperience = tmpExp;
        }

        #region Remove Skill
        private void RemoveSkillFromSkills(object obj)
        {
            int skillId = (int)obj;

            _curriculumVitae.Skills.ToList().RemoveAt(skillId);
            Skills.RemoveAt(skillId);
        }
        private bool CanRemoveSkillFromSkills(object arg)
        {
            int skillId = (int)arg;

            return skillId >= 0 && Skills.Count > skillId;
        }
        #endregion

        #region Add Skill
        private void AddSkillToSkills(object obj)
        {
            SkillViewModel skill = new SkillViewModel((obj as string).Trim(' '));
            Skills.Add(skill);
            _curriculumVitae.Skills.ToList().Add(skill.Name);
            Console.WriteLine($"Skill \"{skill.Name}\" added to skills");
        }
        private bool CanAddSkillToSkills(object arg)
        {
            string skillName = (arg as string);
            SkillViewModel skill = new SkillViewModel(skillName.Trim(' '));
            return !string.IsNullOrEmpty(skill.Name) && !Skills.Contains(skill);
        }
        #endregion

        #region Remove Experience
        private bool CanRemoveExpFromPreviousExperience(object arg)
        {
            int expId = (int)arg;

            return expId >= 0 && expId < PreviousExperience.Count;
        }
        private void RemoveExpFromPreviousExperience(object obj)
        {
            int expId = (int)obj;

            PreviousExperience.RemoveAt(expId);
        }
        #endregion

        #region Add Experience
        private void AddExpToPreviousExperience(object obj)
        {
            PreviousExperience.Add(new ExperienceViewModel(new Experience("Company", "Position", default, default, new string[] { "Responsobility0", "Responsobility1", "Responsobility2" })));
        }
        #endregion

        #region Remove Responsobility
        private bool CanRemoveResponsobilityToCerteinExperience(object arg)
        {
            var values = (object[])arg;
            if (values == null || values.Length < 2 || values[1] == DependencyProperty.UnsetValue)
                return false;
            int expId = (int)values[0];
            int respId = (int)values[1];

            return expId >= 0 && respId >= 0 && expId < PreviousExperience.Count && respId < PreviousExperience[expId].Responsibilities.Count;
        }
        private void RemoveResponsobilityFromCerteinExperience(object obj)
        {
            var values = (object[])obj;
            int expId = (int)values[0];
            int respId = (int)values[1];

            PreviousExperience[expId].Responsibilities.RemoveAt(respId);
        }
        #endregion

        #region Add Responsobility
        private bool CanAddResponsobilityToCerteinExperience(object arg)
        {
            int expId = (int)arg;

            return expId >= 0 && expId < PreviousExperience.Count;
        }

        private void AddResponsobilityToCerteinExperience(object obj)
        {
            int expId = (int)obj;

            PreviousExperience.ElementAt(expId).Responsibilities.Add(new ResponsobilityViewModel("Responsobility"));
        }
        #endregion

    }

}
