﻿using MC.ApplicationServices.DTOs;
using MC.Data.Entities;
using MC.Repositories.Implementations;
using System.Collections.Generic;

namespace MC.ApplicationServices.Implementations
{
    public class GenreManagementService
    {
        public List<GenreDto> Get()
        {
            List<GenreDto> genresDto = new List<GenreDto>();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                foreach (var item in unitOfWork.GenreReposiotry.Get())
                {
                    genresDto.Add(new GenreDto
                    {
                        Id = item.Id,
                        Name = item.Name
                    });
                }
            }

            return genresDto;
        }

        public GenreDto GetById(int id)
        {
            GenreDto genreDto = new GenreDto();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Genre genre = unitOfWork.GenreReposiotry.GetByID(id);

                genreDto = new GenreDto
                {
                    Id = genre.Id,
                    Name = genre.Name
                };
            }

            return genreDto;
        }

        public bool Save(GenreDto genreDto)
        {
            Genre genre = new Genre
            {
                Id = genreDto.Id,
                Name = genreDto.Name
            };

            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    if (genreDto.Id == 0)
                        unitOfWork.GenreReposiotry.Insert(genre);
                    else
                        unitOfWork.GenreReposiotry.Update(genre);
                    unitOfWork.Save();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            if (id == 0) return false;

            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    Genre genre = unitOfWork.GenreReposiotry.GetByID(id);
                    unitOfWork.GenreReposiotry.Delete(genre);
                    unitOfWork.Save();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
