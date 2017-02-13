using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.Web.Mvc;
using AutoMapper;
using IgrejaOnlineWesleyana.Extensions;
using IgrejaOnlineWesleyana.Models;
using IgrejaOnlineWesleyana.Repositories;
using IgrejaOnlineWesleyana.ViewModels;
using Newtonsoft.Json;

namespace IgrejaOnlineWesleyana.Controllers
{
    public class MembrosController : Controller
    {
        private readonly IMWModel db = new IMWModel();
        private readonly FichaRepository _fichaRepository = new FichaRepository();
        private readonly IMapper _mapper;

        public MembrosController()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Membro, FichaCadastralViewModel>();
                cfg.CreateMap<Conjuge, FichaCadastralViewModel>();
                cfg.CreateMap<FichaCadastralViewModel, Membro>();
                cfg.CreateMap<FichaCadastralViewModel, Conjuge>();

            });
            _mapper = config.CreateMapper();
        }
        // GET: Membros
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var membro = db.Membro.Include(m => m.Cidade).Include(m => m.Cidade1).Include(m => m.Congregacao).Include(m => m.Distrito).Include(m => m.Estado).Include(m => m.GrauInstrucao).Include(m => m.Igreja).Include(m => m.Regiao);
            return View(await membro.OrderBy(e=>e.Nome).ToListAsync());
        }
        [NoDirectAccess]
        [Authorize]
        // GET: Membros/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var membro = await db.Membro.FindAsync(id);
            if (membro == null)
            {
                return HttpNotFound();
            }
            return View(membro);
        }

        [HttpGet]
        [Authorize]

        public async Task<ActionResult> NovaFicha()
        {
            var fichaCadastralViewModel = new FichaCadastralViewModel
            {
                Congregacoes = await _fichaRepository.GetCongregacoes(),
                Distritos = await _fichaRepository.GetDistritos(),
                Igrejas = await _fichaRepository.GetIgrejas(),
                Regioes = await _fichaRepository.GetRegioes(),
                GrausInstrucao = await _fichaRepository.GetGrausInstrucao(),
                Estados = await _fichaRepository.GetEstados(),
                Cidades = await _fichaRepository.GetCidades(),
                Naturalidades = await _fichaRepository.GetCidades(),
                TiposConjuge = await _fichaRepository.GetTipoConjugue()
            };
            return View("NovaFicha", fichaCadastralViewModel);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NovaFicha(FichaCadastralViewModel fichaCadastralViewModel)
        {
            var jsonResult = new JsonResult();
            try
            {
                var validator = new CPFValidatorViewModel();
                var result = validator.Validate(fichaCadastralViewModel);
                var firstOrDefault = result.Errors.FirstOrDefault();
                if (ModelState.IsValid && result.IsValid)
                {
                    fichaCadastralViewModel.Foto = JsonConvert.DeserializeObject<byte[]>(fichaCadastralViewModel.FotoEscolhida);
                    var membro = _mapper.Map<FichaCadastralViewModel, Membro>(fichaCadastralViewModel);
                    db.Membro.Add(membro);
                    db.SaveChanges();
                    if (fichaCadastralViewModel.NomeConjuge != null)
                    {
                        fichaCadastralViewModel.IDEsposa = membro.ID;
                        var conjuge = _mapper.Map<FichaCadastralViewModel, Conjuge>(fichaCadastralViewModel);
                        db.Conjuge.Add(conjuge);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }

                var errorModel =
                    from x in ModelState.Keys
                    where ModelState[x].Errors.Count > 0
                    select new
                    {
                        key = x,
                        errors = ModelState[x].Errors.
                            Select(y => y.ErrorMessage).
                            ToArray()
                    };

                if (firstOrDefault != null)
                {
                    var error="";
                    var x = errorModel.ToList();
                    foreach (var item in errorModel)
                    {
                        var a = 0;
                        error = error + " " + item.errors[a];
                        a++;
                    }
                    jsonResult = new JsonResult()
                    {
                        Data = firstOrDefault.ErrorMessage
                    };
                }
                else
                    jsonResult = new JsonResult()
                    {
                        Data = errorModel
                    };

                jsonResult.ContentEncoding=Encoding.UTF8;
                HttpContext.Response.StatusCode =(int)HttpStatusCode.BadRequest;

                fichaCadastralViewModel = new FichaCadastralViewModel
                {
                    Congregacoes = await _fichaRepository.GetCongregacoes(),
                    Distritos = await _fichaRepository.GetDistritos(),
                    Igrejas = await _fichaRepository.GetIgrejas(),
                    Regioes = await _fichaRepository.GetRegioes(),
                    GrausInstrucao = await _fichaRepository.GetGrausInstrucao(),
                    Estados = await _fichaRepository.GetEstados(),
                    Cidades = await _fichaRepository.GetCidades(),
                    Naturalidades = await _fichaRepository.GetCidades(),
                    TiposConjuge = await _fichaRepository.GetTipoConjugue()
                };
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);

            }
            return Json(new { data=jsonResult }, JsonRequestBehavior.AllowGet);
        }
        [NoDirectAccess]
        [HttpGet]
        public async Task<ActionResult> BuscarIDPorCPF(string cpf)
        {
            if (cpf == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var membro = await db.Membro.FirstOrDefaultAsync(e => e.CPF == cpf);
            if (membro == null)
            {
                return HttpNotFound();
            }
            return Json(new { data = membro.ID }, JsonRequestBehavior.AllowGet);

        }
        [NoDirectAccess]
        [HttpGet]
        public async Task<ActionResult> AlterarFicha(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var membro = await db.Membro.FindAsync(id);
            if (membro == null)
            {
                return HttpNotFound();
            }
            var conjuge = await _fichaRepository.GetConjugue(membro.ID);
            var filhos = await _fichaRepository.GetFilhos(membro.ID);
            var fichaCadastralViewModel = _mapper.Map<Membro,FichaCadastralViewModel>(membro);
            fichaCadastralViewModel.Congregacoes = await _fichaRepository.GetCongregacoes();
            fichaCadastralViewModel.Distritos = await _fichaRepository.GetDistritos();
            fichaCadastralViewModel.Igrejas = await _fichaRepository.GetIgrejas();
            fichaCadastralViewModel.Regioes = await _fichaRepository.GetRegioes();
            fichaCadastralViewModel.GrausInstrucao = await _fichaRepository.GetGrausInstrucao();
            fichaCadastralViewModel.Estados = await _fichaRepository.GetEstados();
            fichaCadastralViewModel.Cidades = await _fichaRepository.GetCidades();
            fichaCadastralViewModel.Naturalidades = await _fichaRepository.GetCidades();
            fichaCadastralViewModel.TiposConjuge = await _fichaRepository.GetTipoConjugue();
            if (conjuge != null)
            {
                fichaCadastralViewModel.IDConjuge = conjuge.IDConjuge;
                fichaCadastralViewModel.DataNascimentoConjuge = conjuge.DataNascimentoConjuge;
                fichaCadastralViewModel.EmailConjuge = conjuge.EmailConjuge;
                fichaCadastralViewModel.NomeConjuge = conjuge.NomeConjuge;
                fichaCadastralViewModel.TelefoneConjuge = conjuge.TelefoneConjuge;
                fichaCadastralViewModel.IDTipoConjuge = conjuge.IDTipoConjuge;
                fichaCadastralViewModel.IDEsposa = conjuge.IDEsposa;

            }
            if(filhos.Count>0)
            fichaCadastralViewModel.Filhos = filhos;
            return View("AlterarFicha", fichaCadastralViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AlterarFicha(FichaCadastralViewModel fichaCadastralViewModel)
        {
            var jsonResult = new JsonResult();
            try
            {
                fichaCadastralViewModel.Foto = JsonConvert.DeserializeObject<byte[]>(fichaCadastralViewModel.FotoEscolhida);
                if (ModelState.IsValid)
                {
                    var membro = _mapper.Map<FichaCadastralViewModel, Membro>(fichaCadastralViewModel);
                    db.Entry(membro).State = EntityState.Modified;
                    db.SaveChanges();
                    if (fichaCadastralViewModel.NomeConjuge == null) return RedirectToAction("Index");
                    fichaCadastralViewModel.IDEsposa = membro.ID;
                    var conjuge = _mapper.Map<FichaCadastralViewModel, Conjuge>(fichaCadastralViewModel);
                    fichaCadastralViewModel.IDEsposa = membro.ID;
                    db.Entry(conjuge).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                var errorModel =
                    from x in ModelState.Keys
                    where ModelState[x].Errors.Count > 0
                    select new
                    {
                        key = x,
                        errors = ModelState[x].Errors.
                            Select(y => y.ErrorMessage).
                            ToArray()
                    };

                jsonResult = new JsonResult
                {
                    Data = errorModel,
                    ContentEncoding = Encoding.UTF8
                };

                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                fichaCadastralViewModel = new FichaCadastralViewModel
                {
                    Congregacoes = await _fichaRepository.GetCongregacoes(),
                    Distritos = await _fichaRepository.GetDistritos(),
                    Igrejas = await _fichaRepository.GetIgrejas(),
                    Regioes = await _fichaRepository.GetRegioes(),
                    GrausInstrucao = await _fichaRepository.GetGrausInstrucao(),
                    Estados = await _fichaRepository.GetEstados(),
                    Cidades = await _fichaRepository.GetCidades(),
                    Naturalidades = await _fichaRepository.GetCidades(),
                    TiposConjuge = await _fichaRepository.GetTipoConjugue()
                };
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return Json(new { data = jsonResult }, JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Membro membro = await db.Membro.FindAsync(id);
            if (membro == null)
            {
                return HttpNotFound();
            }
            return View(membro);
        }
        // POST: Membros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Membro membro = await db.Membro.FindAsync(id);
            foreach (var filho in membro.Filhos.ToList())
            {
                db.Filho.Remove(filho);
            }
            var conjugue = await _fichaRepository.GetConjugue(membro.ID);
            if(conjugue != null)
            db.Conjuge.Remove(conjugue);
            db.Membro.Remove(membro);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
