using Hall_Of_Fame.Data.Models;

namespace Hall_Of_Fame.Services.Interfaces
{
    public interface IPersonService
    {

        /// <summary>
        ///  Получение списка Person
        /// </summary>
        /// <returns></returns>
        public Task<List<Person>> Get();

        /// <summary>
        /// Получение Person по Id
        /// </summary>
        /// <param Name="Id"></param>
        /// <returns>Id</returns>      
        public Task<Person> Get(int id);

        /// <summary>
        /// Создание нового Person
        /// </summary>
        /// <param Name="person"></param>
        /// <returns></returns>  
        public Task<Person> Create(Person person);

        /// <summary>
        /// Изменение Person
        /// </summary>
        /// <param Name="person"></param>
        /// <returns></returns>
        public Task<Person> Edit(Person person);

        /// <summary>
        /// Удаление Person по Id
        /// </summary>
        /// <param Name="Id"></param>
        /// <returns></returns>
        public Task<int> Delete(int id);

    }
}