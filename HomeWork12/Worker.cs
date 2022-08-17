using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace HomeWork12
{
    /// <summary>
    /// Критерии сортировки
    /// </summary>
    enum SortedCriterion
    {
        Name,
        Surname,
        Salary
    }

    /// <summary>
    /// Структура сотрудников организации
    /// </summary>
    class Worker
    {
        #region Поля

        static private int count = 0;   //Статическое поле счетчика сотрудников

        private string name;            //Имя

        private string surname;         //Фамилия

        private int age;                //Возраст

        private int salary;             //Зарплата

        private int numberofprojects;   //Количество проектов

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Name">Имя</param>
        /// <param name="Surname">Фамилия</param>
        /// <param name="Age">Возраст</param>
        /// <param name="Salary">Зарплата</param>
        /// <param name="NumberOfProjects">Количество проектов</param>
        public Worker(string Name, string Surname, int Age, int Salary, int NumberOfProjects)
        {
            count++;
            this.name = Name;
            this.surname = Surname;
            this.age = Age;
            this.salary = Salary;
            this.numberofprojects = NumberOfProjects;
        }

        #endregion

        #region Метод

        /// <summary>
        /// Метод, печатающий поля структуры
        /// </summary>
        /// <returns>Поля</returns>
        public override string ToString()
        {
            return String.Format("{0,10}{1,10}{2,10}{3,10}{4,10}",
                name,
                surname,
                age,
                salary,
                numberofprojects);
        }

        #endregion

        #region Свойства

        [JsonProperty("Name")]
        public string Name { get { return this.name; } set { this.name = value; } }                                 //Свойство Имя
        [JsonProperty("Surname")]
        public string Surname { get { return this.surname; } set { this.surname = value; } }                        //Свойство Фамилия
        [JsonProperty("Age")]
        public int Age { get { return this.age; } set { this.age = value; } }                                       //Свойство Возраст
        [JsonProperty("Salary")]
        public int Salary { get { return this.salary; } set { this.salary = value; } }                              //Свойство Зарплата
        [JsonProperty("NumberOfProjects")]
        public int NumberOfProjects { get{ return this.numberofprojects; } set { this.numberofprojects = value; } } //Свойство Количество проектов
        [JsonProperty("Count")]
        static public int Count { get { return count; } }                                                           //Свойство Счетчик
        #endregion

        #region Сортировка
        
        private class SortByName : IComparer<Worker>
        {
            public int Compare(Worker x, Worker y)
            {
                Worker X = (Worker)x;
                Worker Y = (Worker)y;

                return String.Compare(X.Name, Y.Name);
            }
        }
        private class SortBySurname : IComparer<Worker>
        {
            public int Compare(Worker x, Worker y)
            {
                Worker X = (Worker)x;
                Worker Y = (Worker)y;

                return String.Compare(X.Surname, Y.Surname);
            }
        }
        private class SortBySalary : IComparer<Worker>
        {
            public int Compare(Worker x, Worker y)
            {
                Worker X = (Worker)x;
                Worker Y = (Worker)y;

                if (X.Salary == Y.Salary) return 0;
                else if (X.Salary > Y.Salary) return 1;
                else return -1;
            }
        }
        public static IComparer<Worker> SortedBy(SortedCriterion criterion)
        {
            if (criterion == SortedCriterion.Name) return new SortByName();
            else if (criterion == SortedCriterion.Surname) return new SortBySurname();
            else return new SortBySalary();
        }
        #endregion
    }
}