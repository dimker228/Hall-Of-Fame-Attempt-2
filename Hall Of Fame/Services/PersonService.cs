using Hall_Of_Fame.Data.Models;
using Hall_Of_Fame.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Hall_Of_Fame.Services
{
    public class PersonService : IPersonService
    {

        private WorkerContext _context;
        private ISkillService _skillService;
        private readonly ILogger<PersonService> _logger;
        public PersonService(WorkerContext context, ISkillService skillService, ILogger<PersonService> logger)
        {
            _context = context;
            _skillService = skillService;
            _logger = logger;
        }

        /// <summary>
        /// Получение Person
        /// </summary>
        /// <returns>persons</returns>
        public async Task<List<Person>> Get()
        {
            return await _context.Persons.Include(s => s.Skills).ToListAsync();
        }

        /// <summary>
        /// Создание нового Person и Skills + проверка Level 
        /// </summary>
        /// <param Name="person"></param>
        /// <returns>personService || null</returns>
        public async Task<Person> Create(Person person)
        {

            var personToAdd = ToModelForGet(person);
            await _context.Persons.AddAsync(personToAdd);
            if (personToAdd.Skills.All(l => l.Level >= 1 && l.Level <= 10))
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Created new Person object");

                return person;
            }
            else
                return null;

        }
        /// <summary>
        /// Нужна для проверки рэнжа Level
        /// </summary>
        /// <param Name="skill"></param>
        /// <returns>Skills</returns>
        private Skills ToModelForGetSkills(Skills skill)
        {
            return new Skills
            {
                Id = skill.Id,
                Name = skill.Name,
                Level = skill.Level,
                PersonId = skill.PersonId
            };
        }
        /// <summary>
        /// Изменение Person
        /// </summary>
        /// <returns>Person</returns>
        public async Task<Person> Edit(Person person)
        {
            var existingPerson = await _context.Persons.FirstOrDefaultAsync(p => p.Id == person.Id);
            if (existingPerson is null)
            {
                return null;
            }
            existingPerson.Name = person.Name;
            existingPerson.DisplayName = person.DisplayName;

            foreach (Skills skill in person.Skills)
            {
                await _skillService.Edit(skill);
            }

            _context.Persons.Update(existingPerson);
            await _context.SaveChangesAsync();
            return person;
        }

        /// <summary>
        /// Получение Person по Id
        /// </summary>
        /// <returns></returns>
        public async Task<Person> Get(int id)
        {

            var person = await _context.Persons.Include(s => s.Skills).FirstOrDefaultAsync(p => p.Id == id);
            if (person != null) return ToModelForGet(person);
            else
                return null;
        }

        /// <summary>
        /// Удаление Person по Id
        /// </summary>
        /// <returns>Id</returns>
        public async Task<int> Delete(int id)
        {
            var personToDelete = await _context.Persons.FirstOrDefaultAsync(p => p.Id == id);

            if (personToDelete == null) return 0;

            _context.Persons.Remove(personToDelete);
            await _context.SaveChangesAsync();

            return id;
        }

        /// <summary>
        /// Для отображения с айдишниками
        /// </summary>
        /// <param Name="person"></param>
        /// <returns>Person</returns>
        private Person ToModelForGet(Person person)
        {
            return new Person
            {
                Id = person.Id,
                Name = person.Name,
                DisplayName = person.DisplayName,
                Skills = _skillService.ToListModelForGetPerson(person.Skills)
            };
        }
    }
}
