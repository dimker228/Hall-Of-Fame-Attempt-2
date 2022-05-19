using Hall_Of_Fame.Data.Models;
using Hall_Of_Fame.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hall_Of_Fame.Services
{
    public class SkillService : ISkillService
    {
        private WorkerContext _context;
        private readonly ILogger<SkillService> _logger;
        public SkillService(WorkerContext context, ILogger<SkillService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        ///  Создание Skills
        /// </summary>
        /// <param Name="skill"></param>
        /// <returns>skill</returns>
        public async Task<Skills> Create(Skills skill)
        {
            var skillToAdd = ToModel(skill);
            await _context.Skilss.AddAsync(skillToAdd);

            await _context.SaveChangesAsync();
            _logger.LogInformation("Created new Skill object");

            return skill;
        }

        /// <summary>
        /// Получение списка Skills
        /// </summary>
        /// <returns>result</returns>
        public async Task<List<Skills>> Get()
        {
            var skills = await _context.Skilss.ToListAsync();
            var result = new List<Skills>();

            foreach (Skills skill in skills)
            {
                result.Add(ToModelForGetSkills(skill));
            }

            return result;
        }

        /// <summary>
        /// Получение списка по Id
        /// </summary>
        /// <param Name="Id"></param>
        /// <returns></returns>
        public async Task<Skills> Get(int id)
        {
            var skill = await _context.Skilss.FirstOrDefaultAsync(s => s.Id == id);

            if (skill != null) return ToModelForGetSkills(skill);

            return null;
        }

        /// <summary>
        /// Изменение
        /// </summary>
        /// <param Name="skillModel"></param>
        /// <returns>skillModel</returns>
        public async Task<Skills> Edit(Skills skillModel)
        {
            var existingSkill = await _context.Skilss.FirstOrDefaultAsync(s => s.Id == skillModel.Id);
            if (existingSkill != null)
            {
                existingSkill.Name = skillModel.Name;
                existingSkill.Level = skillModel.Level;
                existingSkill.PersonId = skillModel.PersonId;
                _context.Skilss.Update(existingSkill);

                await _context.SaveChangesAsync();
            }

            return skillModel;
        }

        /// <summary>
        /// Удаление Skills по Id
        /// </summary>
        /// <param Name="Id"></param>
        /// <returns>Id</returns>
        public async Task<int> Delete(int id)
        {
            var skillToDelete = await _context.Skilss.FirstOrDefaultAsync(s => s.Id == id);

            if (skillToDelete == null) return 0;

            _context.Skilss.Remove(skillToDelete);
            await _context.SaveChangesAsync();

            return id;
        }


        /// <summary>
        /// include
        /// </summary>
        /// <param Name="skills"></param>
        /// <returns>result</returns>
        public List<Skills> ToListModel(List<Skills> skills)
        {
            var result = new List<Skills>();
            foreach (Skills skill in skills)
            {
                result.Add(ToModel(skill));
            }

            return result;
        }

        /// <summary>
        /// include
        /// </summary>
        /// <param Name="skill"></param>
        /// <returns>Skills</returns> 
        private Skills ToModel(Skills skill)
        {
            return new Skills
            {

                Name = skill.Name,
                Level = skill.Level,
                PersonId = skill.PersonId
            };
        }
        /// <summary>
        /// Для отображения с айдишниками
        /// </summary>
        /// <param Name="skill"></param>
        /// <returns></returns>
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
        /// Для того, чтобы отображение было с Id
        /// </summary>
        /// <param Name="skills"></param>
        /// <returns>result</returns>
        public List<Skills> ToListModelForGetPerson(List<Skills> skills)
        {
            var result = new List<Skills>();
            foreach (Skills skill in skills)
            {
                result.Add(ToModelForGetSkills(skill));
            }

            return result;
        }
    }
}
