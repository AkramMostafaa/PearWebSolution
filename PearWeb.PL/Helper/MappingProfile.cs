using AutoMapper;
using Pear.DAL.Data;
using PearWeb.PL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PearWeb.PL.Helper
{
    class MappingProfile:Profile
    {

        public MappingProfile()
        {
            CreateMap<Products, ProductsVM>()
                .ReverseMap();

            CreateMap<Suppliers, SupplierVM>()
                .ReverseMap();
        }
    }

}