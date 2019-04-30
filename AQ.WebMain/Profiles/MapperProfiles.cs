using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AQ.Models;
using AQ.ViewModels;
using AutoMapper;

namespace AQ.WebMain.Profiles
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<ArticleCategoryViewModel, ArticleCategory>();
        }
    }
}
