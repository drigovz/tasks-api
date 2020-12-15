using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TasksApi.DTOs;
using TasksApi.Models;
using TasksApi.Pagination;
using TasksApi.Repository;

namespace TasksApi.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/[Controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class TasksController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public TasksController(IUnitOfWork uof, ILogger<TasksController> logger, IMapper mapper)
        {
            _uof = uof;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TasksDTO>>> GetAllAsync([FromQuery] QueryStringParameters taskParameters)
        {
            try
            {
                _logger.LogInformation(" ########## GET api/tasks ########## ");
                var tasks = await _uof.TasksRepository.GetTasksPaginationAsync(taskParameters);

                var jsonMetadata = new
                {
                    tasks.TotalCount,
                    tasks.PageSize,
                    tasks.CurrentPage,
                    tasks.TotalPages,
                    tasks.HasNext,
                    tasks.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(jsonMetadata));

                var tasksDTO = _mapper.Map<List<TasksDTO>>(tasks);
                return tasksDTO;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar acessar o servidor");
            }
        }

        [HttpGet("listforname")]
        public async Task<ActionResult<IEnumerable<TasksDTO>>> GetTasksForNameAsync()
        {
            var task = await _uof.TasksRepository.GetTasksForName();
            var tasksDTO = _mapper.Map<List<TasksDTO>>(task);
            return tasksDTO;
        }

        /// <summary>
        /// Obtém uma tarefa pelo ID
        /// </summary>
        /// <param name="id">ID da tarefa</param>
        ///<returns>Objeto tarefa</returns>
        [HttpGet("{id}", Name = "GetTask")]
        public async Task<IActionResult> GetTaskAsync([BindRequired] int id)
        {
            try
            {
                var task = await _uof.TasksRepository.GetById(t => t.Id == id);
                if (task == null)
                    return NotFound($"A categoria de código {id} não foi encontrada!");
                else
                {
                    var taskDTO = _mapper.Map<Tasks>(task);
                    return new ObjectResult(taskDTO);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar acessar o servidor");
            }
        }

        ///<summary>
        /// Inclui uma nova tarefa
        ///</summary>
        ///<remarks>
        /// Exemplo de requisição:
        /// POST api/tasks
        /// {
        ///     "name": "Comprar playstation 5",
        ///     "isCompleted": true
        /// }
        ///</remarks>
        /// <param name="taskDTO">Data Transfer Object da tarefa</param>
        ///<returns>Objeto tarefa incluído</returns>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TasksDTO taskDTO)
        {
            try
            {
                if (taskDTO == null)
                    return BadRequest();

                var task = _mapper.Map<Tasks>(taskDTO);
                _uof.TasksRepository.Add(task);
                await _uof.Commit();

                var resultTaskDTO = _mapper.Map<TasksDTO>(task);

                return CreatedAtRoute("GetTask", new { id = resultTaskDTO.Id }, resultTaskDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar criar uma nova tarefa!");
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([BindRequired] int id, [FromBody] TasksDTO objDTO)
        {
            try
            {
                if (objDTO == null || objDTO.Id != id)
                    return BadRequest($"Não foi possível atualizar a tarefa com código {id}");

                var task = _mapper.Map<Tasks>(objDTO);
                _uof.TasksRepository.Update(task);
                await _uof.Commit();

                return new OkResult();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar atualizar uma tarefa!");
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([BindRequired] int id)
        {
            try
            {
                Tasks task = await _uof.TasksRepository.GetById(t => t.Id == id);
                if (task == null)
                    return NotFound($"Não foi possível localizar a tarefa com código {id}");

                _uof.TasksRepository.Delete(task);
                await _uof.Commit();

                return new NoContentResult();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar excluir a tarefa com código {id}!");
            }
        }
    }
}