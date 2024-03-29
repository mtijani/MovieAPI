﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using movieAPI.DTOs;
using movieAPI.Entities;
using movieAPI.Filters;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace movieAPI.Controllers
{
    [Route("api/genres")]
    [ApiController]
 //   [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    public class GenresController : ControllerBase
    {
        private readonly ILogger<GenresController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public GenresController( ILogger<GenresController> logger ,
            ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet] // api/genres
       
        public async Task<ActionResult<List<GenreDTO>>> Get()
        {
            logger.LogInformation("getting all the genres");
            var genres =  await context.Genres.OrderBy(x => x.Name).ToListAsync();
            return mapper.Map<List<GenreDTO>>(genres);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GenreCreationDTO genreCreationDTO)
        {
            var genre = mapper.Map<Genre>(genreCreationDTO);
            context.Add(genre);
            await context.SaveChangesAsync();
            return NoContent();

        }
        [HttpPut("{id:int}")]   
        public async Task<ActionResult> Put(int id,[FromBody] GenreCreationDTO genreCreationDTO)
        {
            var genre = mapper.Map<Genre>(genreCreationDTO);
            genre.Id = id;
            context.Entry(genre).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var genre = await context.Genres.FirstOrDefaultAsync(x => x.Id == Id);
            if (genre == null)
            {
                return NotFound();
            }
            context.Remove(genre);
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("{id:int}")] // api/genres/example
        public async Task<ActionResult<GenreDTO>> Get(int Id)
        {

            var genre = await context.Genres.FirstOrDefaultAsync(x => x.Id==Id);
            if(genre== null)
            {
                return NotFound();
            }
            return mapper.Map<GenreDTO>(genre);
        }
    }
}
