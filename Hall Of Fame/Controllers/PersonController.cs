using Hall_Of_Fame.Data.Models;
using Hall_Of_Fame.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hall_Of_Fame.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PersonsController : ControllerBase
    {
        private IPersonService _personService;
        
        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        /// <summary>
        ///  Получить список сотрнудников
        /// </summary>
        /// <returns>Person</returns>
        [HttpGet]
        public async Task<ActionResult<List<Person>>> Get()
        {
            var getPerson = await _personService.Get();
            return Ok(getPerson);
        }


        /// <summary>
        ///Получить сотрудника по Id
        /// </summary>
        /// <param Name="Id">Для получения по Id</param>
        /// <returns>Id</returns>
        [HttpGet("{Id}")]
        public async Task<ActionResult<Person>> Get(int id)
        {
            var getPerson = await _personService.Get(id);
            if (getPerson is null)
            {
                return NotFound();

            }
            return Ok(getPerson);
        }

        /// <summary>
        /// Добавить нового сотрудника
        /// </summary>
        /// <param Name="person"></param>
        /// <returns>Person</returns>
        [HttpPost]
        public async Task<ActionResult<Person>> Post(Person person)
        {
            var postPerson = await _personService.Create(person);
            if (postPerson is null)
            {
                return NotFound();
            }
            else
                return Ok(person);

        }

        /// <summary>
        /// Изменить сотрудника
        /// </summary>
        /// <param Name="person"></param>
        /// <returns>Person</returns>
        [HttpPut]
        public async Task<ActionResult<Person>> PutAsync(Person person)
        {
            var changedPerson = await _personService.Edit(person);
            if (changedPerson is null)
            {
                return NotFound();
            }
            return Ok(changedPerson);
        }

        /// <summary>
        /// Удалить сотрудника по Id
        /// </summary>
        /// <param Name="Id"></param>
        /// <returns>Person</returns> 
        [HttpDelete("{Id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            var delPerson = await _personService.Delete(id);
            if (delPerson == 0)
            {
                return NotFound();
            }
            else
                return Ok(delPerson);
        }
      
    }

}
