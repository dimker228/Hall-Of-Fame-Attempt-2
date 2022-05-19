using Hall_Of_Fame.Data.Models;

namespace Hall_Of_Fame.Services.Interfaces
{
    public interface ISkillService
    {

        /// <summary>
        /// Создание нового Skills 
        /// </summary>
        /// <param Name="skill"></param>
        /// <returns>Skills</returns>
        public Task<Skills> Create(Skills skill);

        /// <summary>
        /// Получение списка Skills
        /// </summary>
        /// <returns>Skills</returns>  
        public Task<List<Skills>> Get();

        /// <summary>
        /// Получение по Id
        /// </summary>
        /// <param Name="Id"></param>
        /// <returns>Skills</returns>
        public Task<Skills> Get(int id);

        /// <summary>
        ///Изменение Skills
        /// </summary>
        /// <param Name="skillmodel"></param>
        /// <returns>Skills</returns>
        public Task<Skills> Edit(Skills skillmodel);

        /// <summary>
        /// Удаление Skills
        /// </summary>
        /// <param Name="Id"></param>
        /// <returns>Skills</returns>
        public Task<int> Delete(int id);

        public List<Skills> ToListModel(List<Skills> skills);
        public List<Skills> ToListModelForGetPerson(List<Skills> skills);


    }
}