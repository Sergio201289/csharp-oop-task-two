using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace HomeWork12
{
    class Company
    {
        #region Поля
        //Название
        private string title;
        //Дата создания
        private DateTime dateofcreation;
        //Коллекция департаментов
        private List<Department> departments;

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор Компания
        /// </summary>
        /// <param name="Title">Название Компании</param>
        public Company(string Title)
        {
            this.title = Title;
            this.dateofcreation = DateTime.Now;
            this.departments = new List<Department>();
        }

        #endregion

        #region Методы

        /// <summary>
        /// Метод, находящий в компании родительский департамент от заданного
        /// </summary>
        /// <param name="id">id заданного департамента</param>
        /// <param name="company">компания</param>
        /// <returns>родительский департамент</returns>
        public Company FindParentDepartment(int id, Company company)
        {
            Company temp = null;
            foreach (var d in company.Departments)
            {
                if (d.DepartmentID == id) temp = company;
                else temp = FindParentDepartment(id, d);
                if (temp != null) break;
            }
            return temp;
        }

        /// <summary>
        /// Метод, удаляющий департамент из коллекции
        /// </summary>
        /// <param name="department">департамент</param>
        public void DeleteDepartment(Department department)
        {
            this.Departments.Remove(department);
        }

        /// <summary>
        /// Метод, добавляющий департамент в коллекцию
        /// </summary>
        /// <param name="department">Департамент</param>
        public void AddDepartment(Department department)
        {
            this.Departments.Add(department);
        }

        /// <summary>
        /// Метод, редактирующий департамент
        /// </summary>
        /// <param name="title">Новое название</param>
        /// <param name="id">id департамента</param>
        public void EditDepartment(string newtitle, int id)
        {
            foreach(var d in Departments)
            {
                if (d.DepartmentID == id) d.Title = newtitle;
            }
        }

        /// <summary>
        /// Метод, распаковывающий базу компании из файла
        /// </summary>
        public void OpenCompany()
        {
            try
            {
                Departments = JsonConvert.DeserializeObject<List<Department>>(File.ReadAllText($"CompanyBackup.json"));
            }
            catch { }
        }

        /// <summary>
        /// Метод, сохраняющий компанию в файл
        /// </summary>
        public void SaveCompany()
        {
            File.WriteAllText($"CompanyBackup.json", JsonConvert.SerializeObject(Departments));
        }

        #endregion

        #region Свойства

        [JsonProperty("Title")]
        public string Title { get { return this.title; } set { this.title = value; } }                                          //Свойство "Название"
        [JsonProperty("DateOfCreation")]
        public DateTime DateOfCreation { get { return this.dateofcreation; } private set { this.dateofcreation = value; } }     //Свойство "Дата создания"
        [JsonProperty("Departments")]
        public List<Department> Departments { get { return this.departments; } set { this.departments = value; } }              //Свойство "Департамент"

        #endregion
    }
}
