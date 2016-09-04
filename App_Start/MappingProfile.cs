using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Dtos;
using Vidly.Models;
using AutoMapper;

namespace Vidly.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //This will map the properties of these 2 classes together by property Name.
            Mapper.CreateMap<Customer, CustomerDto>();
            //The ForMember method here tells the API to ignore whatever the client sends for ID so that it does not generate
            //A 'property id is park of the key info and cant be modified' exception.
            Mapper.CreateMap<CustomerDto, Customer>().ForMember(c => c.Id, opt => opt.Ignore());
            Mapper.CreateMap<Movie, MoviesDto>();
            //When mapping MoviesDto to Movie, ignore (don't try to map) ID
            Mapper.CreateMap<MoviesDto, Movie>().ForMember(m => m.Id, opt => opt.Ignore());
            Mapper.CreateMap<MembershipType, MembershipTypeDto>(); //To access MembershipType in JSON with Ajax/jQuery
            Mapper.CreateMap<Genre, GenreDto>(); //To access Genre in JSON with Ajax/jQuery


        }
    }
}