using LALC_UWP.Models;
using LALCXamarin.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LALCXamarin.ViewModels.Categorias
{
   
    public class EditarSubcategoriaViewModel: Subcategoria
    {

        public LalcAPI lalcAPI { get; set; }
        public String Title { get; set; }

        public EditarSubcategoriaViewModel()
        {
            Title = "Editar categoría";
            lalcAPI = new LalcAPI();
        }

        public async Task<Subcategoria> OnAppearing(int id)
        {
            return await lalcAPI.GetSubcategoria(id);
        }

        public async Task<Boolean> EditarSubcategoria(int id, Subcategoria editSubcategoria)
        {
            return await lalcAPI.EditarSubcategoria(id, editSubcategoria);
        }

    }
}
