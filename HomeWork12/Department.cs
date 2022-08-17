using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml.Serialization;

namespace HomeWork12
{
    /// <summary>
    /// Сруктура департамента
    /// </summary>
    class Department : Company
    {
        #region Поля

        static private int count = 0;           //Счетчик департаментов

        private int departmentID;               //ID департамента

        private List<Worker> workers;           //Коллекция работников

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор Департамента
        /// </summary>
        /// <param name="Title">Название департамента</param>
        public Department(string Title) : base (Title)
        {
            this.departmentID = count;
            count++;
            this.workers = new List<Worker>();
        }

        #endregion

        #region Методы

        /// <summary>
        /// Метод, добавляющий работника в коллекцию
        /// </summary>
        /// <param name="worker">Работник</param>
        public void AddWorker(Worker worker)
        {
            this.workers.Add(worker);
        }

        /// <summary>
        /// Метод, удаляющий работника из коллекции
        /// </summary>
        /// <param name="index">Индекс работника</param>
        public void DeleteWorker(int index)
        {
            this.workers.RemoveAt(index - 1);
        }

        /// <summary>
        /// Метод, редактирующий работника в коллекции
        /// </summary>
        /// <param name="name">Новое имя</param>
        /// <param name="surname">Новая фамилия</param>
        /// <param name="age">Новый возраст</param>
        /// <param name="salary">Новая зарплата</param>
        /// <param name="numberofprojects">Новое количество проектов</param>
        /// <param name="index">Индекс</param>
        public void EditWorker(string name, string surname, int age, int salary, int numberofprojects, int index)
        {
            this.workers[index - 1].Name = name;
            this.workers[index - 1].Surname = surname;
            this.workers[index - 1].Age = age;
            this.workers[index - 1].Salary = salary;
            this.workers[index - 1].NumberOfProjects = numberofprojects;
        }

        #endregion

        #region Свойства

        [JsonProperty("Workers")]
        public List<Worker> Workers { get { return this.workers; } set { this.workers = value; } }                  //Свойство "Работники"
        [JsonProperty("DepartmentID")]
        public int DepartmentID { get { return this.departmentID; } set { this.departmentID = value; } }            //Свойство "ID департамента"
        [JsonProperty("Count")]
        public int Count { get { return count; } }                                                                  //Свойство "Счетчик"
        #endregion
    }
}