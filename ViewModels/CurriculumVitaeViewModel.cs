using Commands;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace ViewModels
{
    public class CurriculumVitaeViewModel : AViewModel
    {
        private CurriculumVitae _curriculumVitae;
        public CurriculumVitae CurriculumVitae
        {
            get => _curriculumVitae;
            //set => SetProperty(ref _curriculumVitae, value);
        }


        private readonly RelayCommand _addSkill;
        public ICommand AddSkill => _addSkill;

        private readonly RelayCommand _removeSkill;
        public ICommand RemoveSkill => _removeSkill;


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

        private ObservableCollection<Skill> _skills = new ObservableCollection<Skill>();
        public ObservableCollection<Skill> Skills
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
            curriculumVitae.Skills.ToList().ForEach(i => Skills.Add(new Skill(i)));
            _curriculumVitae.PreviousExperience.ToList().ForEach(i => PreviousExperience.Add(new ExperienceViewModel(i)));


            _addSkill = new RelayCommand(AddSkillToSkills, CanAddSkillToSkills);
            _removeSkill = new RelayCommand(RemoveSkillFromSkills, CanRemoveSkillFromSkills);

        }



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

        private void AddSkillToSkills(object obj)
        {
            Skill skill = new Skill((obj as string).Trim(' '));
            Skills.Add(skill);
            _curriculumVitae.Skills.ToList().Add(skill.Name);
            Console.WriteLine($"Skill \"{skill.Name}\" added to skills");
        }
        private bool CanAddSkillToSkills(object arg)
        {
            string skillName = (arg as string);
            Skill skill = new Skill(skillName.Trim(' '));
            return !string.IsNullOrEmpty(skill.Name) && !Skills.Contains(skill);
        }


    }

}
