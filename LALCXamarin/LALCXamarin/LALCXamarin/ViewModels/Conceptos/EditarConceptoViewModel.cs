using LALC_UWP.Models;
using LALCXamarin.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LALCXamarin.ViewModels.Categorias
{
   
    public class EditarConceptoViewModel: Concepto
    {

        public LalcAPI lalcAPI { get; set; }
        public String Title { get; set; }

        public EditarConceptoViewModel()
        {
            Title = "Editar concepto";
            lalcAPI = new LalcAPI();
        }

        public async Task<Concepto> OnAppearing(int id)
        {
            return await lalcAPI.GetConcepto(id);
        }

        public async Task<Boolean> EditarConcepto(int id, Concepto editConcepto)
        {
            return await lalcAPI.EditarConcepto(id, editConcepto);
        }

    }
}
